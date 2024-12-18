using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBubble : MonoBehaviour
{
    private Vector3 _mousePos;
    private Camera _mainCam;
    public Rigidbody2D _rb;
    public float _force;
    public int _dmg;
    public float _timeToDie;
    public List<BaseEnemy> _enemyList = new List<BaseEnemy>();




    // Start is called before the first frame update
    IEnumerator Start()
    {
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = transform.position - _mousePos;
        Vector3 direction = _mousePos - transform.position;
        _rb.velocity = new Vector2(direction.x, direction.y).normalized * _force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);



        yield return new WaitForSeconds(_timeToDie);
        foreach(BaseEnemy enemy in _enemyList)
        {
            var _enemyRigid = enemy.GetComponent<Rigidbody2D>();

            enemy.transform.SetParent(null);
            _enemyRigid.bodyType = RigidbodyType2D.Dynamic;

            enemy.enabled = true;
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        BaseEnemy enemyDetected = collision.gameObject.GetComponent<BaseEnemy>();
        if (enemyDetected != null)
        {
            enemyDetected.TakeDamage(_dmg);
            _enemyList.Add(enemyDetected);

            var _enemyRigid = enemyDetected.GetComponent<Rigidbody2D>();
            enemyDetected.enabled = false;

            _enemyRigid.bodyType = RigidbodyType2D.Kinematic;
            enemyDetected.transform.SetParent(this.gameObject.transform);
            enemyDetected.transform.position = this.gameObject.transform.position;
        }
    }

    
}
