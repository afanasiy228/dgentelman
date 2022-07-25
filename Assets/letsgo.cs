using System;
using UnityEngine;

public class letsgo : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 4;
    [SerializeField] private float jumpPower = 10;
    [SerializeField] private float jumpGravity = 2;
    [SerializeField] private int maxJumps = 2;

    private Rigidbody2D rbody;
    private Animator animator;
    private bool jumpButtonDown = false;
    private bool jumpButtonUp = false;
    private bool rising = false;
    private ground ground = null;
    private float baseGravityScale;
    private int jumps = 0;

    private const int noJump = 0, jumpRise = 1, jumpFall = 2;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ground = GetComponentInChildren<ground>();
        baseGravityScale = rbody.gravityScale;
    }

    private void Update()
    {
        jumpButtonDown |= Input.GetButtonDown("Jump") && jumps < maxJumps;
        if (rising)
        {
            jumpButtonUp |= Input.GetButtonUp("Jump");
        }
        else
        {
            jumpButtonUp = false;
        }
    }
    void FixedUpdate()
    {
        Vector2 motion;

        float input = Input.GetAxis("Horizontal");
        if (input == 0)
        {
            animator.SetBool("New Bool", false);
            motion.x = rbody.velocity.x;
        }
        else
        {
            animator.SetBool("New Bool", true);

            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Math.Sign(input);
            transform.localScale = scale;

            motion.x = input * maxSpeed;
            if (motion.x > 0)
            {
                if (rbody.velocity.x < maxSpeed)
                {
                    motion.x = Mathf.Min(maxSpeed, rbody.velocity.x + motion.x);
                }
            }
            else
            {
                if (rbody.velocity.x > -maxSpeed)
                {
                    motion.x = Mathf.Max(-maxSpeed, rbody.velocity.x + motion.x);
                }
            }
        }

        if (jumpButtonDown)
        {
            motion.y = jumpPower;
            jumpButtonDown = false;
            rbody.gravityScale = jumpGravity;
            rising = true;

            animator.SetInteger("jumpState", jumpRise);
            jumps++;
        }
        else
        {
            motion.y = rbody.velocity.y;
            if (rising) // если летим
            {
                if (motion.y <= 0 || jumpButtonUp) // а вдруг уже падаем?
                {
                    rbody.gravityScale = baseGravityScale;
                    rising = false;
                }
            }
            else // а если не летим или уже падаем
            {
                if (ground.IsGrounded())
                {
                    animator.SetInteger("jumpState", noJump);
                    jumps = 0;
                }
                else
                {
                    animator.SetInteger("jumpState", jumpFall);
                }
            }
        }

        rbody.velocity = motion;
    }
}