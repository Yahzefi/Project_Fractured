using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject player;
    Vector3 playerPos;
    Vector3 playerScreenPos;

    Vector3 cameraPos;
    Vector3 targetPos;

    float scrollSpeed = 15.0f;

    public static bool screenIsScrolling_X = false;
    public static bool screenIsScrolling_Y = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        cameraPos = this.transform.position;
        targetPos = cameraPos;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        playerScreenPos = Camera.main.WorldToViewportPoint(player.transform.position);
        cameraPos = this.transform.position;

        this.transform.position = new Vector3
                (
                Mathf.MoveTowards(cameraPos.x, targetPos.x, scrollSpeed * Time.deltaTime), // x
                Mathf.MoveTowards(cameraPos.y, targetPos.y, scrollSpeed * Time.deltaTime), // y
                cameraPos.z                                                                // z
                );

        if (playerScreenPos.x > 1 || playerScreenPos.x < 0)
        {
            screenIsScrolling_X = true;


            if (playerScreenPos.x > 1)
            {
                targetPos = new Vector3(playerPos.x + 7.25f, cameraPos.y, cameraPos.z);
            }
            else if (playerScreenPos.x < 0)
            {
                targetPos = new Vector3(playerPos.x - 7.25f, cameraPos.y, cameraPos.z);
            }

            

        }

        if (playerScreenPos.y > 1 || playerScreenPos.y < 0)
        {
            screenIsScrolling_Y = true;


            if (playerScreenPos.y > 1)
            {
                targetPos = new Vector3(cameraPos.x, playerPos.y, cameraPos.z);
            }
            else if (playerScreenPos.y < 0)
            {
                targetPos = new Vector3(cameraPos.x, playerPos.y, cameraPos.z);
            }


        }

        if (screenIsScrolling_X && this.transform.position.x == targetPos.x)
        {
            screenIsScrolling_X = false;
        }

        if (screenIsScrolling_Y && this.transform.position.y == targetPos.y)
        {
            screenIsScrolling_Y = false;
        }

    }

}
