using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    //When a bullet collides with an object other than the player, destroy itself.
    //If that object is an enemy, call DamageEnemy on enemy script
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyManager>().DamageEnemy(1);
        }

        Destroy(gameObject);
    }


}
