using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Text myZoneText = null;

    [SerializeField]
    private Image myTransitionImage = null;

    private PlayerMovement myPlayerMovement = null;

    [SerializeField]
    private Text myDeadText = null;

    [SerializeField]
    private Text myDialogueText = null;

    [SerializeField]
    private GameObject myDialogueGO = null;

    private void Awake()
    {
        myPlayerMovement = GetComponent<PlayerMovement>();
    }

    public void ShowNewZone(string aName)
    {
        StopCoroutine("IE_ShowNewZone");
        StartCoroutine("IE_ShowNewZone", aName);
    }

    private IEnumerator IE_ShowNewZone(string aName)
    {
        myZoneText.text = aName;
        Color col = myZoneText.color;
        col.a = 0;
        myZoneText.color = col;
        const float totalTimer = 1.0f;
        float timer = 0;
        while(timer < totalTimer)
        {
            col.a = timer / totalTimer;
            myZoneText.color = col;
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        timer = 0;
        while (timer < totalTimer)
        {
            col.a = 1 - (timer / totalTimer);
            myZoneText.color = col;
            timer += Time.deltaTime;
            yield return null;
        }

        col.a = 0;
        myZoneText.color = col;
    }

    public void ShowTransition()
    {
        StopCoroutine("IE_ShowTransition");
        StartCoroutine("IE_ShowTransition");
    }

    private IEnumerator IE_ShowTransition()
    {
        myPlayerMovement.SetCanMove(false);

        Color col = myTransitionImage.color;
        col.a = 0;
        myTransitionImage.color = col;
        float timer = 0;
        const float totalTimer = 0.5f;
        while (timer < totalTimer)
        {
            col.a = timer / totalTimer;
            myTransitionImage.color = col;
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        timer = 0;
        while (timer < totalTimer)
        {
            col.a = 1 - (timer / totalTimer);
            myTransitionImage.color = col;
            timer += Time.deltaTime;
            yield return null;
        }

        col.a = 0;
        myTransitionImage.color = col;

        myPlayerMovement.SetCanMove(true);
    }

    public void ShowDeadText()
    {
        StopCoroutine("IE_ShowDeadText");
        StartCoroutine("IE_ShowDeadText");
    }

    private IEnumerator IE_ShowDeadText()
    {
        Color col = myDeadText.color;
        col.a = 0;
        myDeadText.color = col;
        const float totalTimer = 3.0f;
        float timer = 0;
        while (timer < totalTimer)
        {
            col.a = timer / totalTimer;
            myDeadText.color = col;
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void ShowDialogue(string aText)
    {
        myDialogueGO.SetActive(true);
        myDialogueText.text = aText;
    }

    public void HideDialogue()
    {
        myDialogueGO.SetActive(false);
    }
}
