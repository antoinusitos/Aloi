using UnityEngine;

public class PNJ : MonoBehaviour
{
    [SerializeField]
    private MovingBackAndForth myMovingBackAndForth = null;

    [SerializeField]
    private string myTextToSay = "";

    private Player myPlayer = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            myPlayer = player;
            player.GetPlayerAction().SetCanInteract(true, this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && player == myPlayer)
        {
            player.GetPlayerAction().SetCanInteract(false, null);
            myPlayer = null;
        }
    }

    public void Interact(bool aNewState)
    {
        if(myMovingBackAndForth != null)
        {
            myMovingBackAndForth.SetCanMove(!aNewState);
        }
        if(aNewState)
        {
            myPlayer.GetPlayerUI().ShowDialogue(myTextToSay);
        }
    }
}
