using UnityEngine;

public class ZoneDetection : MonoBehaviour
{
    [SerializeField]
    private string myZoneName = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerUI playerUI = collision.GetComponent<PlayerUI>();
        if(playerUI != null)
        {
            playerUI.ShowNewZone(myZoneName);
        }
    }
}
