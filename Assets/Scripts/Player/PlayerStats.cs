using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private int myCurrentlife = 100;
    private int myMaxLife = 100;

    [SerializeField]
    private Slider myLifeSlider = null;

    private float myCurrentHeat = 100;
    private float myMaxHeat = 100;

    private float myHeatCoolSpeed = 5.0f;

    [SerializeField]
    private Slider myHeatSlider = null;

    private int myCurrency = 0;

    [SerializeField]
    private Text myCurrentText = null;

    private int myExperience = 0;

    private int myMaxWatt = 200;
    private int myCurrentWatt = 0;

    [SerializeField]
    private Animator myAnimator = null;

    private PlayerMovement myPlayerMovement = null;

    private bool myIsDead = false;

    private PlayerUI myPlayerUI = null;

    private void Start()
    {
        myPlayerUI = GetComponent<PlayerUI>();

        myLifeSlider.value = myCurrentlife / (float)myMaxLife;
        myHeatSlider.value = myCurrentHeat / myMaxHeat;
        myPlayerMovement = GetComponent<PlayerMovement>();
        myCurrentText.text = myCurrency.ToString();
    }

    private void Update()
    {
        myCurrentHeat -= Time.deltaTime * myHeatCoolSpeed;
        myHeatSlider.value = myCurrentHeat / myMaxHeat;
    }

    public void AddHeat(float value)
    {
        if (myIsDead)
        {
            return;
        }

        myCurrentHeat += value;
    }

    public float GetCurrentHeat()
    {
        return myCurrentHeat;
    }

    public float GetMaxHeat()
    {
        return myMaxHeat;
    }

    public void AddCurrency(int aValue)
    {
        myCurrency += aValue;
        myCurrentText.text = myCurrency.ToString();
    }

    public void AddExperience(int aValue)
    {
        myExperience += aValue;
    }

    public int GetExperience()
    {
        return myExperience;
    }

    public void RemoveCurrency(int aValue)
    {
        myCurrency -= aValue;
        myCurrentText.text = myCurrency.ToString();
    }

    public void Removelife(int aValue)
    {
        if(myIsDead)
        {
            return;
        }

        myCurrentlife -= aValue;
        myLifeSlider.value = myCurrentlife / (float)myMaxLife;

        if(myCurrentlife <= 0)
        {
            myAnimator.SetTrigger("Dead");
            myIsDead = true;
            myPlayerMovement.SetCanMove(false);
            myPlayerUI.ShowDeadText();
            gameObject.layer = 8;
        }
    }

    public int GetCurrentLife()
    {
        return myCurrentlife;
    }

    public int GetMaxWatt()
    {
        return myMaxWatt;
    }

    public int GetCurrentWatt()
    {
        return myCurrentWatt;
    }

    public void AddCurrentWatt(int aValue)
    {
        myCurrentWatt += aValue;
    }
}
