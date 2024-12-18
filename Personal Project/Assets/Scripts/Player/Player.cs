using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] private int _moveSpeed;
    [Header("Jump Values")]
    [SerializeField] private int jumpHeight;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float jumpTakeOffDuration;
    private bool isJumping;
    [Header("Layers")]
    [SerializeField] LayerMask whatisGround;


    private Rigidbody2D rigidBody;

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }
    #endregion

    #region Movement
    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        //float verticalInput = Input.GetAxisRaw("Vertical");
        rigidBody.velocity = new Vector2(horizontalInput * _moveSpeed, rigidBody.velocity.y /*verticalInput * speed*/);

        //if (horizontalInput < 0)
        //{
        //    spriteRenderer.flipX = true;
        //}
        //else if (horizontalInput > 0)
        //{
        //    spriteRenderer.flipX = false;
        //}

        Vector2 finalVelocity = Vector2.right * horizontalInput * _moveSpeed;
        finalVelocity.y = rigidBody.velocity.y;


        rigidBody.velocity = finalVelocity;
    }
    #endregion

    #region Jump
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            StartCoroutine(GroundCheckDisabler());
            rigidBody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        if (isJumping)
        {
            return false;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatisGround);

        return hit;
    }

    IEnumerator GroundCheckDisabler()
    {
        isJumping = true;

        yield return new WaitForSeconds(jumpTakeOffDuration);

        isJumping = false;
    }
    #endregion

    
}
