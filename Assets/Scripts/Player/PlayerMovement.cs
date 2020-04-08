using UnityEngine;

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

    private bool myIsJumping = false;
    private bool myIsGrounded = false;

    [SerializeField]
    private Animator myAnimator = null;

    private bool myCanMove = true;

    private bool myCanDash = true;

    private bool myIsDashing = false;

    private Vector2 myStickDirection = Vector2.zero;

    private CircleCollider2D myTestGroundCheck = null;

    private float myCurrentDashTime = 0;

    private const float myDashTime = 0.15f;

    private Vector2 myDashDirection = Vector2.zero;

    private float mySpeedBonus = 0f;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myTestGroundCheck = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        myStickDirection.x = Input.GetAxis("Horizontal");
        myStickDirection.y = Input.GetAxis("Vertical");

        myIsGrounded = myTestGroundCheck.IsTouchingLayers(LayerMask.GetMask("GroundCheck"));

        myAnimator.SetFloat("Speed", myRigidbody2D.velocity.x < 0 ? myRigidbody2D.velocity.x * -1 : myRigidbody2D.velocity.x);

        if (myRigidbody2D.velocity.x != 0)
        {
            Vector2 spriteDir = Vector2.one;
            if (myRigidbody2D.velocity.x < 0)
                spriteDir.x = -1;

            mySpritePivot.localScale = spriteDir;
        }

        if (!myCanMove)
        {
            return;
        }

        if(myIsGrounded)
        {
            myCanDash = true;
        }

        if(Input.GetButtonDown("Jump") && myIsGrounded)
        {
            myIsJumping = true;
        }

        if(Input.GetButtonDown("Dash") && myCanDash && !myIsDashing)
        {
            myCanDash = false;
            myIsDashing = true;
            myCurrentDashTime = myDashTime;
            gameObject.layer = 12;
            myDashDirection = myStickDirection;
        }

        if(myIsDashing)
        {
            myCurrentDashTime -= Time.deltaTime;
            if(myCurrentDashTime <= 0)
            {
                gameObject.layer = 10;
                myIsDashing = false;
            }
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
        if (!myCanMove)
        {
            Vector3 targetVelocity = new Vector2(0.0f, myRigidbody2D.velocity.y);
            myRigidbody2D.velocity = Vector3.SmoothDamp(myRigidbody2D.velocity, targetVelocity, ref myVelocity, 0.5f);
            return;
        }

        float horizontalMovement = Input.GetAxis("Horizontal") * (mySpeed + (mySpeed * mySpeedBonus)) * Time.fixedDeltaTime;

        MovePlayer(horizontalMovement);
    }

    private void MovePlayer(float aMovement)
    {
        Vector3 targetVelocity = new Vector2(aMovement, myRigidbody2D.velocity.y);
        myRigidbody2D.velocity = Vector3.SmoothDamp(myRigidbody2D.velocity, targetVelocity, ref myVelocity, 0.05f);

        if (myIsJumping)
        {
            myRigidbody2D.AddForce(Vector2.up * myJumpForce);
            myIsJumping = false;
        }

        if(myIsDashing)
        {
            myRigidbody2D.velocity += myDashDirection.normalized * myDashForce;
        }
    }

    public void SetCanMove(bool aNewState)
    {
        myCanMove = aNewState;
    }
}
