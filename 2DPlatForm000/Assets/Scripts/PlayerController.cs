using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float runSpeed;
    public float jumpSpeed;
    public float doubleJumpSpeed;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private bool isGround;
    private bool canDoubleJump;

    private bool jumpKey;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    private float shootDelay = .5f;
    [SerializeField]
    Transform bulletSpawnPos;

    bool isFacingLeft;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //jumpKey = Input.GetKeyDown(KeyCode.W);
        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpKey = true;
            //Jump();
        }
        Shoot();
    }

    void FixedUpdate()
    {
        Run();
        Flip();
        CheckGrounded();
        Jump();
        SwitchAnimation();
        
    }
    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        //Debug.Log(isGround);
    }
    void Flip()
    {
        bool playHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playHasXAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                isFacingLeft = false;
            }
            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                isFacingLeft = true;
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
        if (jumpKey)
        {
            if (isGround)
            {
                myAnim.SetBool("IsJumping", true);
                //Vector2 jumpVel = new Vector2(0.0f, jumpSpeed * Time.deltaTime);
                //myRigidbody.velocity = Vector2.up * jumpVel;

                myRigidbody.AddForce(new Vector2(0f, jumpSpeed));
                canDoubleJump = true;
                jumpKey = false;
            }
            //else
            //{
            //    if (canDoubleJump)
            //    {
            //        myAnim.SetBool("DoubleJumping", true);
            //        Vector2 doubleJumpVel = new Vector2(0.0f, doubleJumpSpeed * Time.deltaTime);
            //        myRigidbody.velocity = Vector2.up * doubleJumpVel;
            //        canDoubleJump = false;
            //    }
            //}
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

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameObject b = Instantiate(bullet);
            b.GetComponent<poopscript>().StartShoot(isFacingLeft);
            b.transform.position = bulletSpawnPos.transform.position;
            Invoke("ResetShoot", shootDelay);
        }
    }
    void ResetShoot()
    {

    }
}
