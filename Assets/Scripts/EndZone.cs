using System.Collections;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    [SerializeField]
    private Transform myArrivingPosition = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerUI playerUI = collision.GetComponent<PlayerUI>();
        if(playerUI != null)
        {
            StartCoroutine("IE_Transition", playerUI);
        }
    }

    private IEnumerator IE_Transition(PlayerUI aPlayerUI)
    {
        aPlayerUI.ShowTransition();
        yield return new WaitForSeconds(0.5f);
        aPlayerUI.transform.position = myArrivingPosition.position;
    }
}
