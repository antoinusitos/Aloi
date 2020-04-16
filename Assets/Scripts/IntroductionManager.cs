using System.Collections;
using UnityEngine;

public class IntroductionManager : MonoBehaviour
{
    private Player myPlayer = null;
    private Rigidbody2D myPlayerRigidBody = null;

    [SerializeField]
    private GameObject myIntroBlocker = null;

    [SerializeField]
    private GameObject myPlayerUI = null;
    private void Start()
    {
        myPlayer = FindObjectOfType<Player>();
        myPlayerRigidBody = myPlayer.GetComponent<Rigidbody2D>();
        myPlayer.GetPlayerMovement().SetIsInCinematic(true);
        StartCoroutine("Introduction");
    }

    private IEnumerator Introduction()
    {
        FadeManager.GetInstance().FadeToVisible(3);
        SoundManager.GetInstance().PlayBackgroundMusic();

        float timer = 0;
        while(timer < 10.0f)
        {
            myPlayerRigidBody.velocity = Vector2.right * 2.0f;
            timer += Time.deltaTime;
            yield return null;
        }

        yield return null;

        myPlayer.GetPlayerMovement().SetIsInCinematic(false);
        myIntroBlocker.SetActive(true);
        myPlayerUI.SetActive(true);
    }
}
