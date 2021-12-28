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
    GameObject teleportMask;
    SpriteRenderer playerSprite;
    SpriteRenderer enemySprite;

    Animator c_Animator; // camera
    Animator p_Animator; // player
    Animator x_Animator; // exclamation mark (child to player)
    Animator e_Animator; // enemy

    Vector3 playerPos;

    string characterName;

    int levelNum = 0;
    int sceneNum = 0;

    int movementStep = 0; // tied in with method update calls
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
        teleportMask = GameObject.Find("Teleport"); // child of enemy to create vanish effect
        teleportMask.SetActive(false);
        playerSprite = player.GetComponent<SpriteRenderer>();
        enemySprite = enemy.GetComponent<SpriteRenderer>();

        c_Animator = GameObject.Find("CutsceneCam").GetComponent<Animator>();
        p_Animator = player.GetComponent<Animator>();
        x_Animator = GameObject.Find("ExclamationPoint").GetComponent<Animator>();
        e_Animator = enemy.GetComponent<Animator>();

        c_Animator.enabled = false;
        sceneCam.enabled = false;

        x_Animator.enabled = false;

        playerPos = player.transform.position;

        StartCoroutine(PlayScene(scene));

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (dManager.sceneIsPlaying) return;

        if (isMoving)
        {
            MoveCharacter(characterName, levelNum, sceneNum);
        }

    }

    IEnumerator PlayScene (Cutscene scene)
    {

        switch (scene)
        {
// !L1S1
            case Cutscene.S01_01:

                levelNum = 1;
                sceneNum = 1;

                yield return new WaitForSeconds(1.0f);
                playerSprite.flipX = true;
                yield return new WaitForSeconds(0.5f);
                playerSprite.flipX = false;
                yield return new WaitForSeconds(0.5f);

                characterName = player.name;
                isMoving = true;

                p_Animator.SetBool("isRunning", true);
                yield return new WaitWhile(() => playerPos.x < 2);
                p_Animator.SetBool("isRunning", false);
                isMoving = false;
                yield return new WaitForSeconds(0.5f);
                playerSprite.flipX = true;
                movementStep = 1;
                isMoving = true;
                p_Animator.SetBool("isRunning", true);
                yield return new WaitWhile(() => playerPos.x > 0.25f);
                p_Animator.SetBool("isRunning", false);
                isMoving = false;

                sceneCam.enabled = true;
                c_Animator.enabled = true;

                c_Animator.SetBool("isScene_01", true);
                NextStep(c_Animator); // c-1

                //StartCoroutine(dManager.initScript(1, 1));
                //yield return new WaitWhile(() => dManager.sceneIsPlaying);

                yield return new WaitForSeconds(0.750f);
                NextStep(c_Animator); // c-2
                yield return new WaitForSeconds(1.0f);
                NextStep(c_Animator); // c-3
                yield return new WaitForSeconds(0.750f);
                NextStep(c_Animator); // c-4
                yield return new WaitForSeconds(0.750f);

                //StartCoroutine(dManager.initScript(1, 2));
                //yield return new WaitWhile(() => dManager.sceneIsPlaying);

                yield return new WaitForSeconds(0.750f);
                NextStep(c_Animator); // c-5
                yield return new WaitForSeconds(0.750f);

                NextStep(e_Animator); // e-1
                yield return new WaitForSeconds(2.017f);
                NextStep(e_Animator); // e-2
                NextStep(c_Animator); // c-6
                playerSprite.flipX = false;
                yield return new WaitForSeconds(1.5f);

                //StartCoroutine(dManager.initScript(1, 3));
                //yield return new WaitWhile(() => dManager.sceneIsPlaying);

                NextStep(e_Animator); // e-3
                yield return new WaitForSeconds(0.517f);
                NextStep(e_Animator); // e-4
                x_Animator.enabled = true;
                yield return new WaitForSeconds(0.75f);
                x_Animator.enabled = false;

                StartCoroutine(dManager.initScript(1, 4));
                yield return new WaitWhile(() => dManager.sceneIsPlaying);

                teleportMask.SetActive(true);
                NextStep(e_Animator); // e-5
                yield return new WaitForSeconds(1.25f);
                enemy.SetActive(false);

                StartCoroutine(dManager.initScript(1, 5));
                yield return new WaitWhile(() => dManager.sceneIsPlaying);

                playerData.cutscene = Cutscene.S01_02;
                DataManager.Save(playerData);

                NextStep(c_Animator); // c-7
                yield return new WaitForSeconds(0.75f);

                ResetAnimator(c_Animator, sceneNum); // c-0
                c_Animator.enabled = false;
                sceneCam.enabled = false;

                break;
// !L1S2
            case Cutscene.S01_02:

                levelNum = 1;
                sceneNum = 2;

                c_Animator.SetBool("isScene_02", true);

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

    void ResetAnimator (Animator animator, int sceneNum)
    {
        animator.SetInteger("SceneStep", 0);
        animator.SetBool($"isScene_0{sceneNum}", false);
    }

    void MoveCharacter(string characterName, int levelNum, int sceneNum)
    {
        if (levelNum == 1)
        {
            switch (sceneNum)
            {
                case 1:

                    if (characterName == "Player")
                    {
                        if (playerPos.x <= 2 && movementStep == 0)
                        {
                            player.transform.position = new Vector3(Mathf.MoveTowards(playerPos.x, 2.5f, 2.5f * Time.deltaTime), playerPos.y, playerPos.z);
                            playerPos = player.transform.position;
                        }
                        if (playerPos.x > 0.25f && movementStep == 1)
                        {
                            player.transform.position = new Vector3(Mathf.MoveTowards(playerPos.x, 0, 2.5f * Time.deltaTime), playerPos.y, playerPos.z);
                            playerPos = player.transform.position;
                        }
                    }

                    break;

                default:
                    break;

            }
        }


    }

}
