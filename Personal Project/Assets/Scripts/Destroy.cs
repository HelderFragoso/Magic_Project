using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        var other = collision.gameObject.GetComponent<BoxCollider2D>();
        //var other2 = collision.gameObject.GetComponent<CircleCollider2D>();
        if (other.CompareTag("Pebble")) 
        {

        }
        else if (other != null) 
        {
            Destroy(this.gameObject);
        }
    }
}
