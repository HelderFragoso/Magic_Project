using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float movingTime = 2;
    [SerializeField] private bool goingRight;
    [SerializeField] protected int damage;
    [SerializeField] protected int hp;
    [SerializeField] private bool canMove = true;
    private SpriteRenderer spriteRenderer;

    public bool CanMove { get => canMove; set => canMove = value; }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Move());
    }

    private void Update()
    {
       
        if (goingRight && canMove == true)
        {
            spriteRenderer.flipX = false;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            
        }
        else
        {
            spriteRenderer.flipX = true;
            transform.Translate(Vector2.left * speed * Time.deltaTime);

        }
    }


    IEnumerator Move()
    {
        yield return new WaitForSeconds(movingTime);
        goingRight = !goingRight;

        spriteRenderer.flipX = goingRight;


        StartCoroutine(Move());
    }



    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Player playerDetected = collision.gameObject.GetComponent<Player>();
        if (playerDetected != null)
        {
            EnemyBehaviour(playerDetected);
        }
    }

    protected virtual void EnemyBehaviour(Player player)
    {
        //player.TakeDamage(damage);
        Debug.Log("player detected");
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {

            Destroy(gameObject);
        }
    }
}
