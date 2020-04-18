using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasEarnObject : MonoBehaviour
{
    private List<string> myObjectsToShow;

    private bool myIsShowing = false;

    [SerializeField]
    private Text myText = null;

    private void Start()
    {
        myObjectsToShow = new List<string>();
    }

    public void AddObjectToShow(string aName)
    {
        myObjectsToShow.Add(aName);
    }

    private void Update()
    {
        if (myIsShowing)
            return;

        if(myObjectsToShow.Count > 0)
        {
            myIsShowing = true;
            StartCoroutine("ShowObject");
        }
    }

    private IEnumerator ShowObject()
    {
        myText.text = "Earned : " + myObjectsToShow[0];
        myObjectsToShow.RemoveAt(0);

        Color col = myText.color;
        col.a = 0;
        myText.color = col;
        const float totalTimer = 1.0f;
        float timer = 0;
        while (timer < totalTimer)
        {
            col.a = timer / totalTimer;
            myText.color = col;
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        timer = 0;
        while (timer < totalTimer)
        {
            col.a = 1 - (timer / totalTimer);
            myText.color = col;
            timer += Time.deltaTime;
            yield return null;
        }

        col.a = 0;
        myText.color = col;

        myIsShowing = false;
    }
}
