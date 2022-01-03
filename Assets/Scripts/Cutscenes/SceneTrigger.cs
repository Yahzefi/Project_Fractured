using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    CutsceneManager csManager;
    Cutscene scene;

    private void Start()
    {
        csManager = GameObject.Find("SceneManager").GetComponent<CutsceneManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            scene = DataManager.cutscene;
            csManager.PlayScene(scene);
        }
        else
        {
            Debug.LogWarning("Something is mistakenly colliding with scene trigger.");
        }
    }

}
