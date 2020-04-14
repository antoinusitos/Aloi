using UnityEngine;

public enum EnemyState
{
    IDLE,
    MOVING,
    ATTACKING,
    KNOCKED,
    FOLLOWING,
}

public class Enemy : MonoBehaviour
{
    private EnemyLife myEnemyLife = null;

    private MovingBackAndForth myMovingBackAndForth = null;

    private EnemyHitBox myEnemyHitBox = null;

    private EnemyState myEnemyState = EnemyState.IDLE;

    private void Start()
    {
        myEnemyLife = GetComponent<EnemyLife>();
        myEnemyLife.SetEnemy(this);
        myMovingBackAndForth = GetComponent<MovingBackAndForth>();
        myMovingBackAndForth.SetEnemy(this);
        myEnemyHitBox = GetComponentInChildren<EnemyHitBox>();
        myEnemyHitBox.SetEnemy(this);
    }

    public EnemyLife GetEnemyLife()
    {
        return myEnemyLife;
    }

    public EnemyState GetEnemyState()
    {
        return myEnemyState;
    } 

    public void SetEnemyState(EnemyState aNewState)
    {
        myEnemyState = aNewState;
        if(aNewState == EnemyState.KNOCKED)
        {
            myEnemyHitBox.StopAttack();
        }
        else
        {
            myMovingBackAndForth.SetCanMove(true);
        }
    }
}
