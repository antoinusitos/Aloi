using UnityEngine;

public class OverampedBiceps : Upgrade
{
    private float myDamageBonusValue = 0.15f;
    private float myHeatMalusValue = 0.2f;

    public OverampedBiceps()
    {
        myUpgradeName = "Overamped Biceps";
        myUpgradeDescription = "Moving Faster.";
        myUpgradeType = UpgradeType.TYPEB;
        myHeatValue = 0;
        myWattValue = 50;
    }

    public override void UseUpgrade(Player aPlayer)
    {

    }

    public override void AddUpgrade(Player aPlayer) 
    {
        base.AddUpgrade(aPlayer);

        aPlayer.GetPlayerAction().AddDamageBonus(myDamageBonusValue);
        aPlayer.GetPlayerAction().AddHeatMalus(myHeatMalusValue);
    }

    public override void RemoveUpgrade(Player aPlayer)
    {
        base.RemoveUpgrade(aPlayer);

        aPlayer.GetPlayerAction().RemoveDamageBonus(myDamageBonusValue);
        aPlayer.GetPlayerAction().RemoveHeatMalus(myHeatMalusValue);
    }
}
