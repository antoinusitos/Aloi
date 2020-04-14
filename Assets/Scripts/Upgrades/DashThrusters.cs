using UnityEngine;

public class DashThrusters : Upgrade
{
    public DashThrusters()
    {
        myUpgradeName = "Dash Thrusters";
        myUpgradeDescription = "Adds a small knockback to any enemy hit by a dash, pushing them up to 1m away";
        myUpgradeType = UpgradeType.TYPEA;
    }

    public override void UseUpgrade(Player aPlayer)
    {

    }

    public override void AddUpgrade(Player aPlayer)
    {
        base.AddUpgrade(aPlayer);
    }

    public override void RemoveUpgrade(Player aPlayer)
    {
        base.RemoveUpgrade(aPlayer);
    }
}
