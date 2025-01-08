using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterJet : MonoBehaviour
{
    [SerializeField] private WaterBubble _bubble;

    private WaterBubble _bubbleInstance;
    // Start is called before the first frame update
    void Start()
    {
        
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
            _bubbleInstance = Instantiate(_bubble, transform.position, _bubble.transform.rotation);
            Destroy(_bubbleInstance, 5.0f);
        }
    }
}
