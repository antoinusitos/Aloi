using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    private Player myPlayer = null;

    private Upgrade[] myTypeASlots = new Upgrade[1];
    private Upgrade[] myTypeBSlots = new Upgrade[1];
    private Upgrade[] myTypeCSlots = new Upgrade[1];
    private Upgrade[] myTypeDSlots = new Upgrade[1];

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
        //Invoke("AddDEBUG", 2.0f);
    }

    private void AddDEBUG()
    {
        myTypeASlots[0] = new DashThrusters();
        myTypeASlots[0].AddUpgrade(myPlayer);
        myPlayer.GetPlayerStats().AddCurrentWatt(myTypeASlots[0].GetWatt());

        myTypeBSlots[0] = new MomentumBooster();
        myTypeBSlots[0].AddUpgrade(myPlayer);
        myPlayer.GetPlayerStats().AddCurrentWatt(myTypeBSlots[0].GetWatt());
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

    public int GetSlotAvailable(UpgradeType anUpgradeType)
    {
        switch (anUpgradeType)
        {
            case UpgradeType.TYPEA:
                return myTypeASlotAvalable;
            case UpgradeType.TYPEB:
                return myTypeBSlotAvalable;
            case UpgradeType.TYPEC:
                return myTypeCSlotAvalable;
            case UpgradeType.TYPED:
                return myTypeDSlotAvalable;
        }
        return 0;
    }
}
