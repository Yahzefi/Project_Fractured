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
    [HideInInspector] public bool isRunning = false;
    [HideInInspector] public bool isJumping = false;
    [HideInInspector] public bool isCrouching = false;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool isHit = false;

    // > player velocity
        // >> current velocity : max velocity 
    [HideInInspector] public float velocityX = 0.0f; // [-1.0f <--> 1.0f] 
    //[HideInInspector] public float velocityY = 0.0f; // [0.0f <--> 1.0f]
        // <<
    [HideInInspector] public float maxVelocity = 7.5f;
/*    [HideInInspector] public float maxVelocityX = 7.5f;d
    [HideInInspector] public float minVelocityY = 7.5f;*/

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
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpPressed = false;
            isJumping = false;
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            isRunning = false;
        }

    }

    public void Move(Rigidbody2D playerBody, Direction direction, float speed = 12.5f, float jumpHeight = 75.0f)
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
                    playerBody.velocity += new Vector2(speed * Time.deltaTime, 0);
                }

                velocityX = 1.0f; // TEST

                break;
            case Direction.Left:
                if (playerBody.velocity.x > -0.5f)
                {
                    p_Sprite.flipX = true;
                }
                if (playerBody.velocity.x > -7.5f)
                {
                    playerBody.velocity -= new Vector2(speed * Time.deltaTime, 0);
                }

                velocityX = -1.0f; // TEST

                break;
            case Direction.Up:
                if (playerBody.velocity.y < maxVelocity)
                {
                    playerBody.velocity += new Vector2(0, jumpHeight * Time.deltaTime);
                }
                else
                {
                    jumpPressed = false;
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

    public void Stop (Rigidbody2D playerBody, Movement movementType)
    {
        if (movementType == Movement.Run)
        {
            playerBody.velocity = new Vector2(0, playerBody.velocity.y);
            velocityX = 0.0f;
        }
        else if (movementType == Movement.Jump)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, 0);
        }
    }
    public IEnumerator Animate (Movement movementType)
    {

        p_Animator.SetBool("isRunning", true);
        yield return new WaitWhile(() => isRunning);
        p_Animator.SetBool("isRunning", false);

        yield return null;
    }

}
