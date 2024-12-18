using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto : MonoBehaviour
{
    private Vector3 _mousePos;
    private Camera _mainCam;
    public Rigidbody2D _rb;
    public float _force;
    public int _dmg;
    public float _timeToDie;

   


    // Start is called before the first frame update
    void Start()
    {
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = transform.position - _mousePos;
        Vector3 direction = _mousePos - transform.position;
        _rb.velocity = new Vector2(direction.x, direction.y).normalized * _force;   
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0 , rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, _timeToDie);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        BaseEnemy enemyDetected = collision.gameObject.GetComponent<BaseEnemy>();
        if (enemyDetected != null)
        {
            enemyDetected.TakeDamage(_dmg);
        }
    }
}
