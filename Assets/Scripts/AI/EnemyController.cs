using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Rigidbody2D enemyBody;
    Animator e_Animator;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = this.GetComponent<Rigidbody2D>();
        e_Animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Animate (Movement movementType)
    {

        switch (movementType)
        {
            case Movement.Walk:

                //

                break;

            default:
                break;

        }

        yield return null;

    }

}
