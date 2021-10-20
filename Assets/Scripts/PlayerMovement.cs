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

        if (player.rightPressed)
        {
            if (player.velocityX < 0)
            {
                player.Stop(playerBody, Movement.Run);
            }

            player.Move(playerBody, Direction.Right);
            StartCoroutine(player.Animate(Movement.Run));
        }
        
        if (player.leftPressed)
        {
            if (player.velocityX > 0)
            {
                player.Stop(playerBody, Movement.Run);
            }

            player.Move(playerBody, Direction.Left);
            StartCoroutine(player.Animate(Movement.Run));
        }

        if (!player.leftPressed && !player.rightPressed)
        {
            player.Stop(playerBody, Movement.Run);
        }

        if (player.jumpPressed)
        {
            player.Move(playerBody, Direction.Up);
        }

    }
}
