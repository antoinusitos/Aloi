using UnityEngine;
using UnityEngine.Analytics;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float mySpeed = 5.0f;

    [SerializeField]
    private float myJumpForce = 5.0f;

    [SerializeField]
    private float myDashForce = 10.0f;

    private Rigidbody2D myRigidbody2D = null;

    [SerializeField]
    private Transform  mySpritePivot = null;

    private Vector3 myVelocity = Vector3.zero;

    private bool myIsGrounded = false;

    private bool mycanJump = true;
    private float myTimeToJump = 0;

    [SerializeField]
    private Animator myAnimator = null;

    private bool myCanMove = true;

    private bool myCanDash = true;
    private float myTimeToDash = 0;

    private bool myIsDashing = false;

    private Vector2 myStickDirection = Vector2.zero;

    private CircleCollider2D myTestGroundCheck = null;

    private float mySpeedBonus = 0f;

    private bool myIsBlocked = false;

    private bool myCanGoThroughEnemies = false;

    private bool myIsInCinematic = false;

    private bool myWallGrabbed = false;

    private float myWallDirection = 0;

    [SerializeField]
    private PlayerCamera myPlayerCamera = null;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myTestGroundCheck = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if(!mycanJump)
        {
            myTimeToJump += Time.deltaTime;
        }

        if(!myCanDash)
        {
            myTimeToDash += Time.deltaTime;
        }

        if (myIsBlocked)
        {
            return;
        }

        if (myWallGrabbed)
        {
            myRigidbody2D.velocity = Vector2.zero;
            myRigidbody2D. gravityScale = 0;
            myWallDirection = mySpritePivot.localScale.x;
            myAnimator.SetBool("WallGrab", true);
            myAnimator.SetBool("Jumping", false);
            myAnimator.SetBool("Falling", false);
        }

        myAnimator.SetFloat("Speed", myRigidbody2D.velocity.x < 0 ? myRigidbody2D.velocity.x * -1 : myRigidbody2D.velocity.x);

        if (myRigidbody2D.velocity.y > 0)
        {
            myAnimator.SetBool("Jumping", true);
            myAnimator.SetBool("Falling", false);
        }
        else if (myRigidbody2D.velocity.y < 0)
        {
            myAnimator.SetBool("Jumping", false);
            myAnimator.SetBool("Falling", true);
        }
        else
        {
            myAnimator.SetBool("Jumping", false);
            myAnimator.SetBool("Falling", false);
        }

        if (!myCanMove)
        {
            return;
        }

        if(myIsInCinematic)
        {
            return;
        }

        myStickDirection.y = Input.GetAxis("Vertical");
        myStickDirection.x = Input.GetAxis("Horizontal");

        myIsGrounded = myTestGroundCheck.IsTouchingLayers(LayerMask.GetMask("GroundCheck"));

        if(myStickDirection.x == 0 && myStickDirection.y < 0)
        {
            myPlayerCamera.SetWantToSeeUnder(true);
            myAnimator.SetBool("Crouch", true);
        }
        else
        {
            myPlayerCamera.SetWantToSeeUnder(false);
            myAnimator.SetBool("Crouch", false);
        }

        if (myRigidbody2D.velocity.x != 0)
        {
            Vector2 spriteDir = Vector2.one;
            if (myRigidbody2D.velocity.x < 0)
                spriteDir.x = -1;

            mySpritePivot.localScale = spriteDir;
        }
        
        if(myIsGrounded)
        {
            if(myTimeToJump >= 0.1f)
            {
                myTimeToJump = 0;
                mycanJump = true;
            }
            if(myTimeToDash >= 0.1f)
            {
                myTimeToDash = 0;
                myCanDash = true;
            }
        }

        if(Input.GetButtonDown("Jump") && mycanJump)
        {
            myRigidbody2D.velocity = Vector2.up * 10.0f;
            mycanJump = false;
            myWallGrabbed = false;
            myAnimator.SetBool("WallGrab", false);
            myRigidbody2D.gravityScale = 1;
        }

        if(Input.GetButtonDown("Dash") && myCanDash && !myIsDashing)
        {
            myRigidbody2D.gravityScale = 1;
            myWallGrabbed = false;
            myAnimator.SetBool("WallGrab", false);
            mycanJump = false;
            myCanDash = false;
            myRigidbody2D.velocity = myStickDirection.normalized * 10.0f;
            if(myCanGoThroughEnemies)
                gameObject.layer = 12;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!myCanGoThroughEnemies && myIsDashing)
        {
            gameObject.layer = 10;
            myIsDashing = false;
        }
    }

    public void AddSpeedBonus(float aBonus)
    {
        mySpeedBonus += aBonus;
    }

    public void RemoveSpeedBonus(float aBonus)
    {
        mySpeedBonus -= aBonus;
    }

    private void FixedUpdate()
    {
        if (myIsBlocked)
        {
            return;
        }

        if (myIsInCinematic)
        {
            return;
        }

        if (!myWallGrabbed)
        {
            if (!myCanMove && myRigidbody2D.velocity != Vector2.zero)
            {
                Vector3 targetVelocity = new Vector2(0.0f, myRigidbody2D.velocity.y);
                myRigidbody2D.velocity = Vector3.SmoothDamp(myRigidbody2D.velocity, targetVelocity, ref myVelocity, 0.5f);
                return;
            }
        }

        float moveInput = Input.GetAxis("Horizontal");
        myRigidbody2D.velocity = new Vector2(moveInput * mySpeed * Time.deltaTime, myRigidbody2D.velocity.y);
        if (moveInput != 0 && ((moveInput > 0 && myWallDirection < 0) || (moveInput < 0 && myWallDirection > 0)))
        {
            myWallGrabbed = false;
            myAnimator.SetBool("WallGrab", false);
            myRigidbody2D.gravityScale = 1;
        }
    }

    public void SetCanMove(bool aNewState)
    {
        myCanMove = aNewState;
    }

    public void SetIsInCinematic(bool aNewState)
    {
        myIsInCinematic = aNewState;
    }

    public void Block(bool aNewState)
    {
        myIsBlocked = aNewState;
        myRigidbody2D.velocity = Vector2.zero;
    }

    public void WallGrabbed(bool aNewState)
    {
        myWallGrabbed = aNewState;
        if(myWallGrabbed)
        {
            mycanJump = true;
        }
    }
}
