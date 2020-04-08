using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerAction myPlayerAction = null;
    private PlayerMovement myPlayerMovement = null;
    private PlayerStats myPlayerStats = null;

    private void Awake()
    {
        myPlayerAction = GetComponent<PlayerAction>();
        myPlayerMovement = GetComponent<PlayerMovement>();
        myPlayerStats = GetComponent<PlayerStats>();
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
}
