using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    private Player myPlayer = null;

    private Upgrade[] myTypeASlots = new Upgrade[2];
    private Upgrade[] myTypeBSlots = new Upgrade[2];
    private Upgrade[] myTypeCSlots = new Upgrade[2];
    private Upgrade[] myTypeDSlots = new Upgrade[4];

    private int myTapeASlotAvalable = 1;
    private int myTapeBSlotAvalable = 1;
    private int myTapeCSlotAvalable = 1;
    private int myTapeDSlotAvalable = 1;

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
        //myTypeBSlots[0] = new OverampedBiceps();
        //myTypeBSlots[0].AddUpgrade(myPlayer);

        myTypeBSlots[0] = new MomentumBooster();
        myTypeBSlots[0].AddUpgrade(myPlayer);
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

    public Upgrade[] GetTypeBUpgrades()
    {
        return myTypeBSlots;
    }

    public Upgrade[] GetTypeCUpgrades()
    {
        return myTypeCSlots;
    }

    public Upgrade[] GetTypeDUpgrades()
    {
        return myTypeDSlots;
    }
}
