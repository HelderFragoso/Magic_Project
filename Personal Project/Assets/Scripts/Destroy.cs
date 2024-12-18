using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        BaseEnemy enemyDetected = collision.gameObject.GetComponent<BaseEnemy>();
        if (enemyDetected != null)
        {
            Destroy(this.gameObject);
        }
    }
}
