using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCheckpoint : MonoBehaviour
{
    Data playerData;

    int[] cPoint;
    int chapterNum;
    int cPointNum;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        playerData = DataManager.playerData;

        chapterNum = playerData.cPoint[0];
        cPointNum = playerData.cPoint[1];

        if (collider.tag == "Player")
        {
            if (chapterNum == 1)
            {
                if (cPointNum < 3)
                {
                    cPointNum++;
                }
                else
                {
                    cPointNum = -1;
                }
            }
            else if (chapterNum == 2)
            {

            }

            playerData.cPoint = new int[] { chapterNum, cPointNum };

            DataManager.Save(CharType.Player, playerData);

        }
        else
        {
            Debug.LogWarning("Invalid Collider Detected.");
        }
    }

}
