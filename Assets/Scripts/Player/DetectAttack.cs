using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAttack : MonoBehaviour
{

    PlayerController player;

    bool hitLanded = false;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        
        Collider2D collider = Physics2D.OverlapCircle(this.transform.position, 0.25f);
        
        if (collider != null && player.isAttacking)
        {
            if (!hitLanded)
            {

                hitLanded = true;

                switch (collider.tag)
                {
                    case "Platform":
                        Debug.Log("Hit Platform");
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
