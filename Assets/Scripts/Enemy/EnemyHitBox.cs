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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(myIsPunching)
        {
            return;
        }

        PlayerStats playerStats = collision.GetComponent<PlayerStats>();
        if(playerStats != null && playerStats.GetCurrentLife() > 0)
        {
            myPlayerTarget = playerStats;
            StopCoroutine("IE_Punch");
            StartCoroutine("IE_Punch");
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
