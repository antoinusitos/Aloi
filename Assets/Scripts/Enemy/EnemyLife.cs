using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField]
    private int myMaxLife = 50;
    private int myCurrentLife = 50;

    private Enemy myEnemy = null;

    private float myKnockTime = 1.0f;
    private float myCurrentKnockTime = 0;

    public void SetEnemy(Enemy aNewEnemy)
    {
        myEnemy = aNewEnemy;
    }

    private void Start()
    {
        myCurrentLife = myMaxLife;
    }

    private void Update()
    {
        if(myEnemy.GetEnemyState() == EnemyState.KNOCKED)
        {
            myCurrentKnockTime += Time.deltaTime;
            if(myCurrentKnockTime >= myKnockTime)
            {
                myCurrentKnockTime = 0;
                myEnemy.SetEnemyState(EnemyState.IDLE);
            }
        }
    }

    public void RemoveLife(int aValue)
    {
        myCurrentLife -= aValue;
        myEnemy.SetEnemyState(EnemyState.KNOCKED);
        if(myCurrentLife <= 0)
        {
            myEnemy.Death();
            Destroy(gameObject);
        }
    }
}
