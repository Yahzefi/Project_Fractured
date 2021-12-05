using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    DialogueManager dManager;

    Scene currentLevel;
    Cutscene scene;
    Data playerData;

    Camera sceneCam;

    GameObject HUD;

    GameObject player;
    GameObject enemy;
    SpriteRenderer playerSprite;
    SpriteRenderer enemySprite;

    Animator c_Animator; // camera
    Animator p_Animator; // player
    Animator e_Animator; // enemy

    Vector3 playerPos;

    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        dManager = this.GetComponent<DialogueManager>();

        currentLevel = SceneManager.GetActiveScene();
        playerData = DataManager.playerData;
        scene = playerData.cutscene;

        sceneCam = GameObject.Find("CutsceneCam").GetComponent<Camera>();

        HUD = GameObject.Find("HUD");
        HUD.SetActive(false);
        
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Antagonist");
        playerSprite = player.GetComponent<SpriteRenderer>();
        enemySprite = enemy.GetComponent<SpriteRenderer>();

        c_Animator = GameObject.Find("CutsceneCam").GetComponent<Animator>();
        p_Animator = player.GetComponent<Animator>();
        e_Animator = enemy.GetComponent<Animator>();

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
            case Cutscene.S01_01:

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
                NextStep(c_Animator); // c-1
                playerSprite.flipX = true;
                isMoving = true;
                p_Animator.SetBool("isRunning", true);
                yield return new WaitWhile(() => playerPos.x > 0.25f);
                p_Animator.SetBool("isRunning", false);
                isMoving = false;

                sceneCam.enabled = true;
                c_Animator.enabled = true;

                StartCoroutine(dManager.initScript(1, 1));
                yield return new WaitWhile(() => dManager.sceneIsPlaying);

                yield return new WaitForSeconds(0.750f);
                NextStep(c_Animator); // c-2
                yield return new WaitForSeconds(1.0f);
                NextStep(c_Animator); // c-3
                yield return new WaitForSeconds(0.750f);
                NextStep(c_Animator); // c-4
                yield return new WaitForSeconds(0.750f);

                StartCoroutine(dManager.initScript(1, 2));
                yield return new WaitWhile(() => dManager.sceneIsPlaying);

                yield return new WaitForSeconds(0.750f);
                NextStep(c_Animator); // c-5
                yield return new WaitForSeconds(0.750f);

                NextStep(e_Animator); // e-1
                yield return new WaitForSeconds(2.017f);
                NextStep(e_Animator); // e-2
                NextStep(c_Animator); // c-6
                playerSprite.flipX = false;
                yield return new WaitForSeconds(1.5f);

                StartCoroutine(dManager.initScript(1, 3));
                yield return new WaitWhile(() => dManager.sceneIsPlaying);

                NextStep(e_Animator); // e-3
                yield return new WaitForSeconds(0.517f);

                StartCoroutine(dManager.initScript(1, 4));
                yield return new WaitWhile(() => dManager.sceneIsPlaying);


                playerData.cutscene = Cutscene.S01_02;
                DataManager.Save(playerData);

                break;

            default:
                break;

        }

        yield return null;
    }

    void NextStep (Animator animator)
    {
        animator.SetInteger("SceneStep", animator.GetInteger("SceneStep") + 1);
    }

}
