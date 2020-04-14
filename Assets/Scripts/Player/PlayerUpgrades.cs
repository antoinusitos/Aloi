using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    private Player myPlayer = null;

    private Upgrade[] myTypeASlots = new Upgrade[2];
    private Upgrade[] myTypeBSlots = new Upgrade[2];
    private Upgrade[] myTypeCSlots = new Upgrade[2];
    private Upgrade[] myTypeDSlots = new Upgrade[4];

    private int myTypeASlotAvalable = 1;
    private int myTypeBSlotAvalable = 1;
    private int myTypeCSlotAvalable = 1;
    private int myTypeDSlotAvalable = 1;

    private void Awake()
    {
        myPlayer = GetComponent<Player>();
    }

    private void Start()
    {
        Invoke("AddDEBUG", 2.0f);
    }

    private void AddDEBUG()
    {
        myTypeASlots[0] = new DashThrusters();
        myTypeASlots[0].AddUpgrade(myPlayer);

        myTypeBSlots[0] = new MomentumBooster();
        myTypeBSlots[0].AddUpgrade(myPlayer);

        myTypeBSlots[1] = new OverampedBiceps();
        myTypeBSlots[1].AddUpgrade(myPlayer);
    }

    public void UseAbility(UpgradeType anUpgradeType)
    {
        switch(anUpgradeType)
        {
            case UpgradeType.TYPEA:
                {
                    if(myTypeASlots[0] != null)
                    {

                    }
                    break;
                }
        }
    }

    public Upgrade[] GetTypeAUpgrades()
    {
        return myTypeASlots;
    }

    public void SetUpgradeTypeA(Upgrade anUpgrade, int anIndex)
    {
        myTypeASlots[anIndex] = anUpgrade;
    }

    public Upgrade[] GetTypeBUpgrades()
    {
        return myTypeBSlots;
    }
    public void SetUpgradeTypeB(Upgrade anUpgrade, int anIndex)
    {
        myTypeBSlots[anIndex] = anUpgrade;
    }

    public Upgrade[] GetTypeCUpgrades()
    {
        return myTypeCSlots;
    }
    public void SetUpgradeTypeC(Upgrade anUpgrade, int anIndex)
    {
        myTypeCSlots[anIndex] = anUpgrade;
    }

    public Upgrade[] GetTypeDUpgrades()
    {
        return myTypeDSlots;
    }
    public void SetUpgradeTypeD(Upgrade anUpgrade, int anIndex)
    {
        myTypeDSlots[anIndex] = anUpgrade;
    }

}
