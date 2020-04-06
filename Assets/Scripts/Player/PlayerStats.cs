using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private int myCurrentlife = 100;
    private int myMaxLife = 100;

    [SerializeField]
    private Slider myLifeSlider = null;

    [SerializeField]
    private Animator myAnimator = null;

    private PlayerMovement myPlayerMovement = null;

    private bool myIsDead = false;

    private PlayerUI myPlayerUI = null;

    private void Start()
    {
        myPlayerUI = GetComponent<PlayerUI>();

        myLifeSlider.value = myCurrentlife / (float)myMaxLife;
        myPlayerMovement = GetComponent<PlayerMovement>();
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
}
