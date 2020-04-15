using UnityEngine;

public class WorkBench : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if(p != null)
        {
            p.GetPlayerAction().SetCanOpenWorkbench(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if (p != null)
        {
            p.GetPlayerAction().SetCanOpenWorkbench(false);
        }
    }
}
