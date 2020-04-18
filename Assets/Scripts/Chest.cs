using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour
{
    private Animator myAnimator = null;

    private bool myIsOpened = false;

    private Player myPlayer = null;

    [SerializeField]
    private string myUpgradeGiven = string.Empty;

    [SerializeField]
    private UnityEvent myPostOpenChest = null;
    
    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myIsOpened)
            return;

        Player p = collision.GetComponent<Player>();
        if (p != null)
        {
            p.GetPlayerAction().SetCanOpenChest(true, this);
            myPlayer = p;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (myIsOpened)
            return;

        Player p = collision.GetComponent<Player>();
        if (p != null)
        {
            p.GetPlayerAction().SetCanOpenChest(false, null);
        }
    }

    public void OpenChest()
    {
        if (myIsOpened)
            return;

        myPlayer.GetPlayerAction().SetCanOpenChest(false, null);
        myAnimator.SetTrigger("Open");

        if(myUpgradeGiven != string.Empty)
        {
            Upgrade upgrade = UpgradesManager.GetInstance().GetUpgrade(myUpgradeGiven);
            if(upgrade != null)
            {
                myPlayer.GetPlayerInventory().AddUpgradeInventory(upgrade);
                myPlayer.GetPlayerInventory().ShowEarnObject(upgrade);
            }
        }

        myPostOpenChest.Invoke();
    }
}
