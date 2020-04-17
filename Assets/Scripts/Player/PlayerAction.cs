using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private Player myPlayer = null;

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

    private bool myCanOpenWorkbench = false;
    private bool myIsInWorkbench = false;

    [SerializeField]
    private GameObject myInteractionButtonGameObject = null;

    private void Start()
    {
        myPlayer = GetComponent<Player>();
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
        bool canPunch = myPlayer.GetPlayerStats().GetCurrentHeat() + myPunchHeat + myPunchHeat * myPunchHeatMalus <= myPlayer.GetPlayerStats().GetMaxHeat() ? true : false;

        if (Input.GetButton("Ability"))
        {
            if (Input.GetButtonDown("Punch"))
            {
                myPlayer.GetPlayerUpgrades().UseAbility(UpgradeType.TYPEC);
            }
            else if (Input.GetButtonDown("Dash"))
            {
                myPlayer.GetPlayerUpgrades().UseAbility(UpgradeType.TYPEB);
            }
            else if (Input.GetButtonDown("Jump"))
            {
                myPlayer.GetPlayerUpgrades().UseAbility(UpgradeType.TYPEA);
            }
        }
        else if (Input.GetButtonDown("Punch") && !myIsPunching && canPunch)
        {
            myPlayer.GetPlayerStats().AddHeat(myPunchHeat + myPunchHeat * myPunchHeatMalus);
            if (myPunchCoroutine != null)
                StopCoroutine(myPunchCoroutine);
            myPunchCoroutine = StartCoroutine("IE_Punch");
        }
        else if (Input.GetButtonDown("Interact") && myCanOpenWorkbench && !myIsInWorkbench)
        {
            myIsInWorkbench = true;
            myPlayer.GetPlayerInventory().SetInWorkBench(true);
        }
        else if (Input.GetButtonDown("Cancel") && myCanOpenWorkbench && myIsInWorkbench)
        {
            myIsInWorkbench = false;
            myPlayer.GetPlayerInventory().SetInWorkBench(false);
        }

        if (myIsPunching)
        {
            myCurrentTimeToHit += Time.deltaTime;
            if(myCurrentTimeToHit >= myTimeToHit)
            {
                myCurrentTimeToHit = 0;
                myPlayer.GetPlayerMovement().Block(false);
                myIsPunching = false;
            }
        }
    }

    public void TryCancelPunch()
    {
        if (myCurrentTimeToHit > myTimeToCancel)
            return;

        StopCoroutine(myPunchCoroutine);
        myPlayer.GetPlayerMovement().SetCanMove(true);
        myIsPunching = false;
        myCurrentTimeToHit = 0;
    }

    private IEnumerator IE_Punch()
    {
        myIsPunching = true;
        myPlayer.GetPlayerMovement().Block(true);
        myAnimator.SetTrigger("Punch");

        List<Enemy> enemies = myHitBox.GetCollidingObjects();

        yield return new WaitForSeconds(myPunchAnimation.length / 2.0f);

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetEnemyLife().RemoveLife(myPunchDamage);
        }

        yield return new WaitForSeconds(myPunchAnimation.length / 2.0f);
    }

    public void SetCanOpenWorkbench(bool aNewState)
    {
        myCanOpenWorkbench = aNewState;
        myInteractionButtonGameObject.SetActive(aNewState);
    }
}
