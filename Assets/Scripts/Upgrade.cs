using UnityEngine;

public enum UpgradeType
{
    TYPEA,
    TYPEB,
    TYPEC,
    TYPED,
}

public class Upgrade
{
    protected UpgradeType myUpgradeType;

    protected string myUpgradeName = "Simple Name";
    protected string myUpgradeDescription = "Simple Description";
    protected int myWattValue = 0;
    protected int myHeatValue = 0;

    public virtual void UseUpgrade(Player aPlayer)
    {

    }

    public virtual void AddUpgrade(Player aPlayer)
    {
        Debug.Log("Added " + myUpgradeName);
    }

    public virtual void RemoveUpgrade(Player aPlayer)
    {
        Debug.Log("Removed " + myUpgradeName);
    }

    public string GetName()
    {
        return myUpgradeName;
    }

    public string GetDescription()
    {
        return myUpgradeDescription;
    }

    public UpgradeType GetMyType()
    {
        return myUpgradeType;
    }

    public int GetHeat()
    {
        return myHeatValue;
    }

    public int GetWatt()
    {
        return myWattValue;
    }
}
