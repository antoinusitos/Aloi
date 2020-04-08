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

    protected string myUpgradeName = "";

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
}
