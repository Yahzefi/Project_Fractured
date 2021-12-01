using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    DialogueManager dManager;

    Scene currentLevel;
    Cutscene scene;

    Camera sceneCam;

    GameObject player;
    SpriteRenderer playerSprite;

    Animator c_Animator;
    Animator p_Animator;

    Vector3 playerPos;

    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        dManager = this.GetComponent<DialogueManager>();

        currentLevel = SceneManager.GetActiveScene();
        scene = DataManager.cutscene;

        Debug.Log(scene);

        sceneCam = GameObject.Find("CutsceneCam").GetComponent<Camera>();
        
        player = GameObject.Find("Player");
        playerSprite = player.GetComponent<SpriteRenderer>();

        c_Animator = GameObject.Find("CutsceneCam").GetComponent<Animator>();
        p_Animator = player.GetComponent<Animator>();

        c_Animator.enabled = false;
        sceneCam.enabled = false;

        playerPos = player.transform.position;

        StartCoroutine(PlayScene(scene));

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (dManager.sceneIsPlaying) return;

        if (isMoving)
        {
            if (playerPos.x <= 2 && c_Animator.GetInteger("SceneStep") == 0)
            {
                player.transform.position = new Vector3(Mathf.MoveTowards(playerPos.x, 2.5f, 2.5f * Time.deltaTime), playerPos.y, playerPos.z);
                playerPos = player.transform.position;
            }
            if (playerPos.x > 0.25f && c_Animator.GetInteger("SceneStep") == 1)
            {
                player.transform.position = new Vector3(Mathf.MoveTowards(playerPos.x, 0, 2.5f * Time.deltaTime), playerPos.y, playerPos.z);
                playerPos = player.transform.position;
            }
        }
    }

    IEnumerator PlayScene (Cutscene scene)
    {

        switch (scene)
        {
            case Cutscene.S00_01:

                yield return new WaitForSeconds(1.0f);
                playerSprite.flipX = true;
                yield return new WaitForSeconds(0.5f);
                playerSprite.flipX = false;
                yield return new WaitForSeconds(0.5f);
                isMoving = true;
                p_Animator.SetBool("isRunning", true);
                yield return new WaitWhile(() => playerPos.x < 2);
                p_Animator.SetBool("isRunning", false);
                isMoving = false;
                yield return new WaitForSeconds(0.5f);
                NextStep(); // 1
                playerSprite.flipX = true;
                isMoving = true;
                p_Animator.SetBool("isRunning", true);
                yield return new WaitWhile(() => playerPos.x > 0.25f);
                p_Animator.SetBool("isRunning", false);
                isMoving = false;

                sceneCam.enabled = true;
                c_Animator.enabled = true;

                StartCoroutine(dManager.initScript(Script.Intro00_01));
                yield return new WaitWhile(() => dManager.sceneIsPlaying);

                yield return new WaitForSeconds(0.750f);
                NextStep(); // 2
                yield return new WaitForSeconds(1.0f);

                NextStep(); // 3
                yield return new WaitForSeconds(0.750f);
                NextStep(); // 4
                yield return new WaitForSeconds(0.750f);

                StartCoroutine(dManager.initScript(Script.Intro00_02));

                yield return new WaitWhile(() => dManager.sceneIsPlaying);

                yield return new WaitForSeconds(0.750f);
                NextStep(); // 5

                // ant jumps off (or maybe teleports) pillar and lands in front of player
                
                // zoom in on both of them

                // dialogue

                // player loses abilities

                break;

            default:
                break;

        }

        yield return null;
    }

    void NextStep ()
    {
        c_Animator.SetInteger("SceneStep", c_Animator.GetInteger("SceneStep") + 1);
    }

}
