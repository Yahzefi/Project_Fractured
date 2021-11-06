using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // > key input/press
    [HideInInspector] public bool rightPressed = false;
    [HideInInspector] public bool leftPressed = false;
    [HideInInspector] public bool jumpPressed = false;

    // > animation parameters
    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public bool isRunning = false;
    [HideInInspector] public bool isJumping = false;
    [HideInInspector] public bool isFalling = false;
    [HideInInspector] public bool isLanding = false;
    [HideInInspector] public bool isCrouching = false;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool isHit = false;

    // > player velocity
    public float runSpeed = 12.5f;
    public float jumpSpeed = 115.0f;

    [HideInInspector] public float maxVelocity = 7.5f;

    [HideInInspector] public PlayerJump jumpPhase;
    [HideInInspector] public PlayerRun runPhase;

    // <

    // > player components
    GameObject player;
    SpriteRenderer p_Sprite;
    Animator p_Animator;

    // > 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        p_Sprite = player.GetComponent<SpriteRenderer>();
        p_Animator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rightPressed = true;
            isRunning = true;
        }
        else
        {
            rightPressed = false;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            leftPressed = true;
            isRunning = true;
        }
        else
        {
            leftPressed = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
            isJumping = true;
            jumpPhase = jumpPhase == PlayerJump.Grounded ? PlayerJump.Ascend : jumpPhase;

        }

        if (isGrounded)
        {
            p_Animator.SetBool("isGrounded", isGrounded);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Platform")
        {
            Debug.Log("Entered");
            isGrounded = true;
            jumpPhase = jumpPhase != PlayerJump.Grounded ? PlayerJump.Landing : jumpPhase;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Platform")
        {
            Debug.Log("Exited");
            isGrounded = false;
        }

    }

    public void Move(Rigidbody2D playerBody, Direction direction)
    {
        switch (direction)
        {
            case Direction.Right:

                // switch case where at 1/4 of the way to max velocity, velocityX is 0.25 || 1/2 -> 0.5, etc.. \\

                if (playerBody.velocity.x < 0.5f)
                {
                    p_Sprite.flipX = false;
                }
                if (playerBody.velocity.x < maxVelocity)
                {
                    playerBody.velocity += new Vector2(runSpeed * Time.deltaTime, 0);
                }

                runPhase = PlayerRun.Right;

                break;
            case Direction.Left:
                if (playerBody.velocity.x > -0.5f)
                {
                    p_Sprite.flipX = true;
                }
                if (playerBody.velocity.x > -7.5f)
                {
                    playerBody.velocity -= new Vector2(runSpeed * Time.deltaTime, 0);
                }

                runPhase = PlayerRun.Left;

                break;
            case Direction.Up:
                if (playerBody.velocity.y < maxVelocity)
                {
                    playerBody.velocity += new Vector2(0, jumpSpeed * Time.deltaTime);
                    jumpPhase = PlayerJump.Ascend;
                }
                else
                {
                    jumpPhase = PlayerJump.MaxHeight;
                    isJumping = false;
                    isFalling = true;
                }
                break;
            case Direction.Down:
                // crouch
                break;
            default:
                Debug.LogWarning("Uh-oh.");
                break;
        }
    }

    public void Stop (Rigidbody2D playerBody, Direction direction)
    {
        if (direction == Direction.Left || direction == Direction.Right || direction == Direction.Idle)
        {
            playerBody.velocity = new Vector2(0, playerBody.velocity.y);
            isRunning = false;
        }
        else if (direction == Direction.Up)
        {
            
            if (jumpPhase == PlayerJump.Ascend)
            {
                isJumping = false;
            }
            else if (jumpPhase == PlayerJump.Landing)
            {
                isFalling = false;
            }

            if (jumpPhase != PlayerJump.MaxHeight)
            {
                playerBody.velocity = new Vector2(playerBody.velocity.x, 0);
                isLanding = jumpPhase == PlayerJump.Landing;
            }

            jumpPhase = !isGrounded ? PlayerJump.Suspended : PlayerJump.Landing;

        }
    }
    public IEnumerator Animate (Movement movementType)
    {
        switch (movementType) // **SET ALL BOOLS TO REF VAR?** \\
        {

            case Movement.Run:
                p_Animator.SetBool("isRunning", true);
                yield return new WaitWhile(() => isRunning);
                p_Animator.SetBool("isRunning", false);

                break;

            case Movement.Jump:

                p_Animator.SetBool("isGrounded", isGrounded);
                p_Animator.SetBool("isJumping", isJumping);
                yield return new WaitWhile(() => isJumping);
                p_Animator.SetBool("isGrounded", isGrounded);
                p_Animator.SetBool("isJumping", isJumping);

                break;

            case Movement.Falling:

                p_Animator.SetBool("isGrounded", isGrounded);
                p_Animator.SetBool("isFalling", isFalling);
                yield return new WaitWhile(() => isFalling);
                p_Animator.SetBool("isGrounded", isGrounded);
                p_Animator.SetBool("isFalling", isFalling);

                break;

            case Movement.Landing:

                p_Animator.SetBool("isLanding", isLanding);
                yield return new WaitForSeconds(0.350f);
                isLanding = false;
                p_Animator.SetBool("isLanding", isLanding);

                jumpPressed = false;
                jumpPhase = PlayerJump.Grounded;

                break;

            default:
                break;
        }

        yield return null;
    }
}
