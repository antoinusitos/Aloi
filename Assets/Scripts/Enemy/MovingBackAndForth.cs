using UnityEngine;
using UnityEngine.Analytics;

public class MovingBackAndForth : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D = null;

    [SerializeField]
    private Transform myPointA = null;
    
    [SerializeField]
    private Transform myPointB = null;

    private bool myTargetIsB = true;

    [SerializeField]
    private float mySpeed = 2.0f;

    [SerializeField]
    private Transform mySpritePivotTransform = null;

    private bool myCanMove = true;

    private Enemy myEnemy = null;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetEnemy(Enemy aNewEnemy)
    {
        myEnemy = aNewEnemy;
    }

    private void FixedUpdate()
    {
        if(!myCanMove)
        {
            return;
        }

        if(myEnemy.GetEnemyState() == EnemyState.KNOCKED)
        {
            return;
        }

        Vector2 pos = Vector2.zero;
        if (myTargetIsB)
        {
            pos = myPointB.position;
        }
        else
        {
            pos = myPointA.position;
        }
        Vector2 diff = pos - myRigidbody2D.position;
        myRigidbody2D.MovePosition(myRigidbody2D.position + diff.normalized * mySpeed * Time.fixedDeltaTime);

        if(Vector3.Distance(myRigidbody2D.position, pos) < 0.1f)
        {
            myTargetIsB = !myTargetIsB;
        }

        if (myRigidbody2D.position.x > pos.x)
        {
            Vector3 scale = Vector3.one;
            scale.x = -1;
            mySpritePivotTransform.localScale = scale;
        }
        else
        {
            mySpritePivotTransform.localScale = Vector3.one;
        }
    }

    public void SetCanMove(bool aNewState)
    {
        myCanMove = aNewState;
    }
}
