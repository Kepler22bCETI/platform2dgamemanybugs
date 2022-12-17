using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float doubleJumpSpeed;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private bool isGround;
    private bool canDoubleJump;
    [SerializeField] private AudioSource jumpAudio;
 
 
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }  
    
    void FixedUpdate()
    {
        Run();
        Flip();
        Jump();
        SwitchAnimation();
        CheckGrounded();

    }
    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    void Flip()
    {
        bool playHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playHasXAxisSpeed)
        {
            if(myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }  
    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed * Time.deltaTime, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool playHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("IsRunning", playHasXAxisSpeed);
    }
    void Jump()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                myAnim.SetBool("IsJumping", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed * Time.deltaTime);
                myRigidbody.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
                jumpAudio.Play();
            }
            else
            {
                if (canDoubleJump)
                {
                    myAnim.SetBool("DoubleJumping", true);
                    Vector2 doubleJumpVel = new Vector2(0.0f, doubleJumpSpeed * Time.deltaTime);
                    myRigidbody.velocity = Vector2.up * doubleJumpVel;
                    canDoubleJump = false;
                    jumpAudio.Play();
                }
            }
        }
    }
    void SwitchAnimation()
    {
        myAnim.SetBool("IsIdling", false);
        if (myAnim.GetBool("IsJumping"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("IsJumping", false);
                myAnim.SetBool("IsFalling", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("IsFalling", false);
            myAnim.SetBool("IsIdling", true);
        }
        if (myAnim.GetBool("DoubleJumping"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("DoubleJumping", false);
                myAnim.SetBool("DoubleFalling", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("DoubleFalling", false);
            myAnim.SetBool("IsIdling", true);
        }

    }
}
