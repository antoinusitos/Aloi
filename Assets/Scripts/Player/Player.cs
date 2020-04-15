using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerAction myPlayerAction = null;
    private PlayerMovement myPlayerMovement = null;
    private PlayerStats myPlayerStats = null;
    private PlayerInventory myPlayerInventory = null;
    private PlayerUpgrades myPlayerUpgrades = null;

    private void Awake()
    {
        myPlayerAction = GetComponent<PlayerAction>();
        myPlayerMovement = GetComponent<PlayerMovement>();
        myPlayerStats = GetComponent<PlayerStats>();
        myPlayerInventory = GetComponent<PlayerInventory>();
        myPlayerUpgrades = GetComponent<PlayerUpgrades>();
    }

    public PlayerAction GetPlayerAction()
    {
        return myPlayerAction;
    }

    public PlayerMovement GetPlayerMovement()
    {
        return myPlayerMovement;
    }

    public PlayerStats GetPlayerStats()
    {
        return myPlayerStats;
    }

    public PlayerInventory GetPlayerInventory()
    {
        return myPlayerInventory;
    }

    public PlayerUpgrades GetPlayerUpgrades()
    {
        return myPlayerUpgrades;
    }
}
