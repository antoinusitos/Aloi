using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    private static FadeManager myInstance = null;

    public static FadeManager GetInstance()
    {
        return myInstance;
    }

    private float myFadeTime = 1.0f;
    [SerializeField]
    private Image myImage = null;

    private void Awake()
    {
        myInstance = this;
    }

    public void FadeToBlack(float aDuration = 1.0f)
    {
        myFadeTime = aDuration;
        StartCoroutine(Fade(1));
    }

    public void FadeToVisible(float aDuration = 1.0f)
    {
        myFadeTime = aDuration;
        StartCoroutine(Fade(-1));
    }

    private IEnumerator Fade(float aDirection)
    {
        float timer = 0;
        Color col = Color.black;
        if (aDirection == 1.0f)
            col.a = 0;
        while(timer < myFadeTime)
        {
            myImage.color = col;
            if (aDirection == 1.0f)
            {
                col.a = (timer / myFadeTime);
            }
            else
            {
                col.a = 1 - (timer / myFadeTime);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        if (aDirection == 1.0f)
        {
            col.a = 1;
        }
        else
        {
            col.a = 0;
        }
        myImage.color = col;
    }
}
