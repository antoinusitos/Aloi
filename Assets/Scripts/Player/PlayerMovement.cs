using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float mySpeed = 5.0f;

    [SerializeField]
    private float myJumpForce = 5.0f;

    private Rigidbody2D myRigidbody2D = null;

    [SerializeField]
    private Transform  mySpritePivot = null;

    private Vector3 myVelocity = Vector3.zero;

    private bool myIsJumping = false;
    private bool myIsGrounded = false;

    [SerializeField]
    private Transform myGroundCheckLeft = null;

    [SerializeField]
    private Transform myGroundCheckRight = null;

    [SerializeField]
    private Animator myAnimator = null;

    private bool myCanMove = true;
    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        myIsGrounded = Physics2D.OverlapArea(myGroundCheckLeft.position, myGroundCheckRight.position);

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

        if(Input.GetButtonDown("Jump") && myIsGrounded)
        {
            myIsJumping = true;
        }
    }

    private void FixedUpdate()
    {
        if (!myCanMove)
        {
            Vector3 targetVelocity = new Vector2(0.0f, myRigidbody2D.velocity.y);
            myRigidbody2D.velocity = Vector3.SmoothDamp(myRigidbody2D.velocity, targetVelocity, ref myVelocity, 0.5f);
            return;
        }

        float horizontalMovement = Input.GetAxis("Horizontal") * mySpeed * Time.fixedDeltaTime;

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
    }

    public void SetCanMove(bool aNewState)
    {
        myCanMove = aNewState;
    }
}
