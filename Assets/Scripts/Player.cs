using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //Configuration parameters 
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(15f,15f);

    [SerializeField] Joystick joystick;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    float gravityScaleAtStart;
    bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) {return;}

        Move();
        /*Jump();*/
        FlipSprite();
        ClimbLadder();
        Die();
    }
     
    private void Move()
    {
        float deltaX = joystick.Horizontal ;
        Vector2 playerVelocity = new Vector2(deltaX * moveSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }
    public void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        /*if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            
        }*/
        Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
        myRigidbody.velocity += jumpVelocityToAdd;
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards","Enemy")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
    private void ClimbLadder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ladders"))) 
        {
            myAnimator.SetBool("Climbling", false);
            myRigidbody.gravityScale = gravityScaleAtStart;
            return; 
        }

        float deltaY = joystick.Vertical;
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, deltaY * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbling", playerHasVerticalSpeed);
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f); 
        }
    }

    
}
