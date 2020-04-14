using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private PlayerMovement myPlayerMovement = null;

    private PlayerStats myPlayerStats = null;

    private PlayerUpgrades myPlayerUpgrades = null;

    private float myPunchHeat = 5.0f;

    private float myPunchHeatMalus = 0f;

    private int myPunchDamage = 10;

    private float myPunchDamageBonus = 0;

    private float myTimeToHit = 0.51f; //this is the length of the animation for now

    private float myCurrentTimeToHit = 0;

    private float myTimeToCancel = 0.2f;

    [SerializeField]
    private Animator myAnimator = null;

    [SerializeField]
    private AnimationClip myPunchAnimation = null;

    private bool myIsPunching = false;

    [SerializeField]
    private HitBox myHitBox = null;

    private Coroutine myPunchCoroutine = null;

    private void Start()
    {
        myPlayerMovement = GetComponent<PlayerMovement>();
        myPlayerStats = GetComponent<PlayerStats>();
        myPlayerUpgrades = GetComponent<PlayerUpgrades>();
    }

    public void AddDamageBonus(float aBonus)
    {
        myPunchDamageBonus += aBonus;
    }

    public void RemoveDamageBonus(float aBonus)
    {
        myPunchDamageBonus -= aBonus;
    }

    public void AddHeatMalus(float aMalus)
    {
        myPunchHeatMalus += aMalus;
    }

    public void RemoveHeatMalus(float aMalus)
    {
        myPunchHeatMalus -= aMalus;
    }

    private void Update()
    {
        bool canPunch = myPlayerStats.GetCurrentHeat() + myPunchHeat + myPunchHeat * myPunchHeatMalus <= myPlayerStats.GetMaxHeat() ? true : false;

        if (Input.GetButton("Ability"))
        {
            if (Input.GetButtonDown("Punch"))
            {
                myPlayerUpgrades.UseAbility(UpgradeType.TYPEC);
            }
            else if (Input.GetButtonDown("Dash"))
            {
                myPlayerUpgrades.UseAbility(UpgradeType.TYPEB);
            }
            else if (Input.GetButtonDown("Jump"))
            {
                myPlayerUpgrades.UseAbility(UpgradeType.TYPEA);
            }
        }
        else if (Input.GetButtonDown("Punch") && !myIsPunching && canPunch)
        {
            myPlayerStats.AddHeat(myPunchHeat + myPunchHeat * myPunchHeatMalus);
            if(myPunchCoroutine != null)
                StopCoroutine(myPunchCoroutine);
            myPunchCoroutine = StartCoroutine("IE_Punch");
        }

        if(myIsPunching)
        {
            myCurrentTimeToHit += Time.deltaTime;
            if(myCurrentTimeToHit >= myTimeToHit)
            {
                myCurrentTimeToHit = 0;
                myPlayerMovement.Block(false);
                myIsPunching = false;
            }
        }
    }

    public void TryCancelPunch()
    {
        if (myCurrentTimeToHit > myTimeToCancel)
            return;

        StopCoroutine(myPunchCoroutine);
        myPlayerMovement.SetCanMove(true);
        myIsPunching = false;
        myCurrentTimeToHit = 0;
    }

    private IEnumerator IE_Punch()
    {
        myIsPunching = true;
        myPlayerMovement.Block(true);
        myAnimator.SetTrigger("Punch");

        List<Enemy> enemies = myHitBox.GetCollidingObjects();

        yield return new WaitForSeconds(myPunchAnimation.length / 2.0f);

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetEnemyLife().RemoveLife(myPunchDamage);
        }

        yield return new WaitForSeconds(myPunchAnimation.length / 2.0f);
    }
}
