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

    float scrollSpeed = 11.5f;

    public static bool screenIsScrolling = false;

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

        this.transform.position = new Vector3(Mathf.MoveTowards(cameraPos.x, targetPos.x, scrollSpeed * Time.deltaTime), cameraPos.y, cameraPos.z);

        if (playerScreenPos.x > 1 || playerScreenPos.x < 0)
        {
            screenIsScrolling = true;
            targetPos = new Vector3(playerPos.x, cameraPos.y, cameraPos.z);
        }

        if (screenIsScrolling && this.transform.position.x == targetPos.x)
        {
            screenIsScrolling = false;
        }
    }

}
