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

    [SerializeField]
    private Animator myAnimator = null;

    [SerializeField]
    private AnimationClip myPunchAnimation = null;

    private bool myIsPunching = false;

    [SerializeField]
    private HitBox myHitBox = null;

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
            StartCoroutine("IE_Punch");
        }
    }

    private IEnumerator IE_Punch()
    {
        myIsPunching = true;
        myPlayerMovement.SetCanMove(false);
        myAnimator.SetTrigger("Punch");

        List<Enemy> enemies = myHitBox.GetCollidingObjects();

        yield return new WaitForSeconds(myPunchAnimation.length / 2.0f);

        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i].gameObject);
        }

        yield return new WaitForSeconds(myPunchAnimation.length / 2.0f);

        myPlayerMovement.SetCanMove(true);
        myIsPunching = false;
    }
}
