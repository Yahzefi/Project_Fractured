using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public static int CalculateDamage(float thisDEF, float otherATK)
    {

        int damage;

        // dmg algo here


        damage = Mathf.Abs(Mathf.FloorToInt( (thisDEF - otherATK) / 2 ));

        damage = damage == 0 ? 1 : damage;

        Debug.Log(thisDEF);
        Debug.Log(otherATK);
        Debug.Log(damage);


        return damage;

    }

    public static void Knockback(Rigidbody2D charBody, float distance, float force)
    {

        charBody.MovePosition(
            new Vector2(
                Mathf.MoveTowards(charBody.position.x, charBody.position.x + distance, (force * Time.deltaTime)),
                charBody.position.y
            )
        );

    }

}
