using System.Collections;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField]
    private Animator myAnimator = null;

    [SerializeField]
    private MovingBackAndForth myMovingBackAndForth = null;

    private bool myIsPunching = false;

    [SerializeField]
    private AnimationClip myPunchAnimation = null;

    private PlayerStats myPlayerTarget = null;

    private Enemy myEnemy = null;

    private Coroutine myAttackCoroutine = null;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(myIsPunching)
        {
            return;
        }

        if(myEnemy.GetEnemyState() == EnemyState.KNOCKED)
        {
            return;
        }

        PlayerStats playerStats = collision.GetComponent<PlayerStats>();
        if(playerStats != null && playerStats.GetCurrentLife() > 0)
        {
            myPlayerTarget = playerStats;
            if(myAttackCoroutine != null)
                StopCoroutine(myAttackCoroutine);
            myAttackCoroutine = StartCoroutine("IE_Punch");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerStats playerStats = collision.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            myPlayerTarget = null;
        }
    }

    public void SetEnemy(Enemy aNewEnemy)
    {
        myEnemy = aNewEnemy;
    }

    public void StopAttack()
    {
        if (myAttackCoroutine != null)
            StopCoroutine(myAttackCoroutine);
        myAnimator.SetTrigger("StopAttack");
        myIsPunching = false;
        myAnimator.ResetTrigger("Attack");
    }

    private IEnumerator IE_Punch()
    {
        myIsPunching = true;
        myMovingBackAndForth.SetCanMove(false);

        myAnimator.SetTrigger("Attack");

        float timeTaken = myPunchAnimation.length / 1.25f;

        yield return new WaitForSeconds(timeTaken);

        if(myPlayerTarget != null)
        {
            myPlayerTarget.Removelife(20);
        }

        yield return new WaitForSeconds(myPunchAnimation.length - timeTaken);

        myMovingBackAndForth.SetCanMove(true);
        myIsPunching = false;
    }
}
