using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAttack : MonoBehaviour
{

    PlayerController player;

    Collider2D otherCollider;

    bool hitLanded = false;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        otherCollider = Physics2D.OverlapCircle(this.transform.position, 0.25f);
    }

    private void FixedUpdate()
    {
        
        
        if (otherCollider != null && player.isAttacking)
        {
            if (!hitLanded)
            {

                hitLanded = true;

                switch (otherCollider.tag)
                {
                    case "Platform":
                        Debug.Log("Hit Platform");
                        break;
                    case "Attack":

                        if (otherCollider.transform.parent.name == "Skeleton")
                        {
                            SkeletonAI skel = otherCollider.GetComponentInParent<SkeletonAI>();
                            skel.hasContact = true;
                            skel.atkChar = CharType.Player;
                        }

                        break;

                    default:
                        break;
                }
            }
        }
        else
        {
            hitLanded = false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, 0.25f);
    }

}
