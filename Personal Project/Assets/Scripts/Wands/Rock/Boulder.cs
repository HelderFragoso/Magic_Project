using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private Vector3 _mousePos;
    private Camera _mainCam;
    public Rigidbody2D _rb;
    public float _force;
    public int _dmg;
    public float _timeToDie;
    public List<RockAuto> _pebbleList = new List<RockAuto>();



    // Start is called before the first frame update
    void Start()
    {
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = transform.position - _mousePos;
        Vector3 direction = _mousePos - transform.position;
        _rb.velocity = new Vector2(direction.x, direction.y).normalized * _force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, _timeToDie);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        BaseEnemy enemyDetected = collision.gameObject.GetComponent<BaseEnemy>();
        RockAuto pebbleDetected = collision.gameObject.GetComponent<RockAuto>();
        if (enemyDetected != null)
        {
            enemyDetected.TakeDamage(_dmg);
        }
        else if (pebbleDetected != null) 
        {
            foreach(RockAuto pebble in _pebbleList) 
            {
                pebble.transform.SetParent(null);
                pebble.gameObject.SetActive(true);
            }
            // lista ou referencia a tds os pebbles e dar unparent a eles antes da pedra se destruir
            //_rockAutoInstance = Instantiate(_pebblePrefab, transform.position, _pebblePrefab.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
