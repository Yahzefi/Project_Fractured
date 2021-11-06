using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject player;
    Vector3 playerPos;

    Vector3 cameraPos;
    Vector3 camOffset;
    Vector3 currPos;

    float scrollSpeed = 6.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        camOffset = new Vector3(0.15f, 0.1f, -10);
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        cameraPos = this.transform.position;

        if (playerPos.x > cameraPos.x + 8.65f)
        {
            this.transform.position = new Vector3(Mathf.Lerp(cameraPos.x, playerPos.x, scrollSpeed * Time.deltaTime), cameraPos.y, cameraPos.z);
        }
    }
}
