using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    IEnumerator Wait;

    Rigidbody2D skeletonBody;
    SpriteRenderer skeletonSprite;
    Animator sk_Animator;

    BoxCollider2D hitCol;
    BoxCollider2D groundCol;
    BoxCollider2D playerCol;

    static int HP;

    static float distFromPlayer;

    static bool routineIsPaused = false;

    static bool hasSpawned = false;
    static bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        skeletonBody = this.GetComponent<Rigidbody2D>();
        skeletonSprite = this.GetComponent<SpriteRenderer>();
        sk_Animator = this.GetComponent<Animator>();
        hitCol = this.GetComponentInChildren<BoxCollider2D>();
        groundCol = this.GetComponentInChildren<BoxCollider2D>();
        playerCol = GameObject.Find("Player").GetComponentInChildren<BoxCollider2D>();


        skeletonSprite.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
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
                // move towards player
            }

        }

        if (HP <= 0)
        {
            // die
        }

    }

    public IEnumerator Animate(Movement movementType)
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

    public IEnumerator Pause(float secondsToWait)
    {
        routineIsPaused = true;
        yield return new WaitForSeconds(secondsToWait);
        routineIsPaused = false;

    }

}
