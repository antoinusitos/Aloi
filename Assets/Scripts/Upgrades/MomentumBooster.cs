using UnityEngine;

public class MomentumBooster : Upgrade
{
    private float mySpeedBonusValue = 0.15f;

    public MomentumBooster()
    {
        myUpgradeName = "Momemtum Booster";
        myUpgradeType = UpgradeType.TYPEB;
    }

    public override void UseUpgrade(Player aPlayer)
    {

    }

    public override void AddUpgrade(Player aPlayer) 
    {
        base.AddUpgrade(aPlayer);

        aPlayer.GetPlayerMovement().AddSpeedBonus(mySpeedBonusValue);
    }

    public override void RemoveUpgrade(Player aPlayer)
    {
        base.RemoveUpgrade(aPlayer);

        aPlayer.GetPlayerMovement().RemoveSpeedBonus(mySpeedBonusValue);
    }
}
