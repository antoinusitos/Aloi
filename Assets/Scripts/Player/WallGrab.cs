using UnityEngine;

public class WallGrab : MonoBehaviour
{
    [SerializeField]
    private Player myPlayer = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WallLedge wallLedge = collision.GetComponent<WallLedge>();
        if(wallLedge != null)
        {
            myPlayer.GetPlayerMovement().WallGrabbed(true);
        }
    }
}
