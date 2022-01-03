using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    PlayerController playerScript;
    
    [HideInInspector] public CharType atkChar;

    GameObject player;

    Rigidbody2D skeletonBody;
    SpriteRenderer skeletonSprite;
    Animator sk_Animator;

    BoxCollider2D hitCol;
    BoxCollider2D groundCol;
    BoxCollider2D playerCol;

    Vector3 playerPos;

    static int HP;
    static float DEF;

    static float distFromPlayer;
    static float movementSpeed = 1.75f;

    // > knockback distance from enemy attack
    float knockbackDist = 0;
    // <
    // > force of knockback / how hard it hits
    float knockbackForce = 6.5f;
    // <

    static bool routineIsPaused = false;

    static bool hasSpawned = false;
    static bool isMoving = false;

    [HideInInspector] public bool hasContact = false;
    bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        HP = DataManager.skeletonData.stats.HP;
        DEF = DataManager.skeletonData.stats.DEF;

        skeletonBody = this.GetComponent<Rigidbody2D>();
        skeletonSprite = this.GetComponent<SpriteRenderer>();
        sk_Animator = this.GetComponent<Animator>();
        hitCol = this.GetComponentInChildren<BoxCollider2D>();
        //groundCol = this.GetComponentInChildren<BoxCollider2D>();

        player = GameObject.Find("Player");
        playerPos = player.transform.position;
        playerCol = player.GetComponentInChildren<BoxCollider2D>();

        playerScript = player.GetComponent<PlayerController>();

        skeletonSprite.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        // update player position
        playerPos = player.transform.position;
        // records distance from player to determine whether to spawn or not
        distFromPlayer = Physics2D.Distance(playerCol, hitCol).distance;

        if (distFromPlayer < 8)
        {
            if (!hasSpawned)
            {
                skeletonSprite.enabled = true;
                StartCoroutine(Animate(Movement.Spawn));
            }
            else if (hasSpawned && !routineIsPaused)
            {
                if (!isMoving)
                {
                    StartCoroutine(Animate(Movement.Walk));
                }
            }

            if (isMoving)
            {
                if (hitCol.IsTouching(playerCol) || playerScript.isHit)
                {
                    StartCoroutine(Stop(Movement.Walk));
                    return;
                }

                skeletonSprite.flipX = playerPos.x > this.transform.position.x;

                // move towards player
                this.transform.position = 
                    new Vector3(
                        Mathf.MoveTowards(this.transform.position.x, playerPos.x, (movementSpeed * Time.deltaTime)), 
                        this.transform.position.y,
                        this.transform.position.z
                    );
            }

            if (isHit) Debug.Log("hittt");

        }

        if (HP <= 0)
        {
            // die
        }

    }

    private void FixedUpdate()
    {
        if (hasContact)
        {
            Debug.Log("hit on target");

            HP -= 
                Combat.CalculateDamage 
                (
                    DEF, 
                    atkChar == CharType.Player ? DataManager.playerData.stats.ATK 
                    : atkChar == CharType.Rin ? DataManager.rinData.stats.ATK
                    : 0
                );

            knockbackDist = playerPos.x > this.transform.position.x ? -2 : 2;

            isHit = true;
            hasContact = false;
            StartCoroutine(Stop(Movement.Walk));

        }

        if (isHit)
        {
            Combat.Knockback(skeletonBody, knockbackDist, knockbackForce);
        }

    }

    IEnumerator Animate (Movement movementType)
    {

        switch (movementType)
        {
            case Movement.Spawn:

                hasSpawned = true;
                StartCoroutine(Pause(1.017f));

                break;

            case Movement.Walk:

                isMoving = true;
                sk_Animator.SetBool("isMoving", isMoving);
                sk_Animator.Play("Skeleton_Walk");

                break;

            default:
                break;

        }

        yield return null;

    }

    IEnumerator Stop (Movement movementType)
    {

        switch (movementType)
        {
            case Movement.Walk:

                isMoving = false;
                StartCoroutine(Pause(0.5f));
                yield return new WaitWhile(() => routineIsPaused);
                isHit = false;

                break;

            default:
                break;

        }

        yield return null;
    }

    IEnumerator Pause(float secondsToWait)
    {
        routineIsPaused = true;
        yield return new WaitForSeconds(secondsToWait);
        routineIsPaused = false;

    }

}
