using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // > player components
    PlayerController player;
    Rigidbody2D playerBody;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {

        if (player.isHit)
        {
            if (player.runPhase != PlayerRun.Idle)
            {
                player.Stop(playerBody, player.runPhase == PlayerRun.Left ? Direction.Left : Direction.Right);
            }

            StartCoroutine(player.Animate(Movement.Hit));

        }

        if (player.rightPressed)
        {
            // if player is running to the left and right is pressed
            if (player.runPhase == PlayerRun.Left)
            {
                player.Stop(playerBody, Direction.Right);
            }

            player.Move(playerBody, Direction.Right);
            StartCoroutine(player.Animate(Movement.Run));
        }
        
        if (player.leftPressed)
        {
            // if player is running to the right and left is pressed
            if (player.runPhase == PlayerRun.Right)
            {
                player.Stop(playerBody, Direction.Left);
            }

            player.Move(playerBody, Direction.Left);
            StartCoroutine(player.Animate(Movement.Run));
        }

        if (!player.leftPressed && !player.rightPressed)
        {
            player.Stop(playerBody, Direction.Idle);
        }

        if (player.jumpPressed)
        {

            switch (player.jumpPhase)
            {
                case PlayerJump.Ascend:
                    player.Move(playerBody, Direction.Up);
                    StartCoroutine(player.Animate(Movement.Jump));
                    break;

                case PlayerJump.Landing:
                    player.Stop(playerBody, Direction.Up);
                    StartCoroutine(player.Animate(Movement.Landing));
                    break;

                case PlayerJump.MaxHeight:
                    player.Stop(playerBody, Direction.Up);
                    StartCoroutine(player.Animate(Movement.Falling));
                    break;

                case PlayerJump.Suspended:
                    break;

                default:
                    Debug.LogWarning("Case Not Found");
                    break;
            }  
        }

        if (player.mouseClickLeft && !player.isAttacking)
        {
            if (player.runPhase != PlayerRun.Idle)
            {
                player.Stop(playerBody, player.runPhase == PlayerRun.Left ? Direction.Left : Direction.Right);
            }

            StartCoroutine(player.Animate(Movement.Attack));

        }

        if (player.lungeActive)
        {
            PlayerStatus.DeductMP(player.Lunge.requiredMP);
            player.UseSkill(player.Lunge.name);
        }

        if (player.quakeActive)
        {
            PlayerStatus.DeductMP(player.Quake.requiredMP);
            player.UseSkill(player.Quake.name);
        }

    }
}
