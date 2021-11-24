using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{

    Scene currentLevel;
    Cutscene scene;

    Camera sceneCam;

    Animator c_Animator;
    Animator p_Animator;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene();
        scene = DataManager.cutscene;

        Debug.Log(scene);

        sceneCam = GameObject.Find("CutsceneCam").GetComponent<Camera>();

        c_Animator = GameObject.Find("CutsceneCam").GetComponent<Animator>();
        p_Animator = GameObject.Find("Player").GetComponent<Animator>();

        c_Animator.enabled = false;
        sceneCam.enabled = false;

        StartCoroutine(PlayScene(scene));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayScene (Cutscene scene)
    {

        switch (scene)
        {
            case Cutscene.S00_01:

                Debug.Log("ye");

                break;

            default:
                break;

        }

        yield return null;
    }

}
