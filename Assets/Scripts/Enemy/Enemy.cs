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

    [SerializeField]
    private int myGoldReward = 5;

    [SerializeField]
    private int myExperienceReward = 50;

    private void Start()
    {
        myEnemyLife = GetComponent<EnemyLife>();
        if(myEnemyLife != null)
            myEnemyLife.SetEnemy(this);
        myMovingBackAndForth = GetComponent<MovingBackAndForth>();
        myMovingBackAndForth.SetEnemy(this);
        myEnemyHitBox = GetComponentInChildren<EnemyHitBox>();
        if(myEnemyHitBox != null)
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
            if (myEnemyHitBox != null)
                myEnemyHitBox.StopAttack();
        }
        else
        {
            myMovingBackAndForth.SetCanMove(true);
        }
    }

    public void Death()
    {
        FindObjectOfType<PlayerStats>().AddCurrency(myGoldReward);
        FindObjectOfType<PlayerStats>().AddExperience(myExperienceReward);
    }
}
