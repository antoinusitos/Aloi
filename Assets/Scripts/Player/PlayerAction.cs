using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private PlayerMovement myPlayerMovement = null;

    [SerializeField]
    private Animator myAnimator = null;

    [SerializeField]
    private AnimationClip myPunchAnimation = null;

    private bool myIsPunching = false;

    [SerializeField]
    private HitBox myHitBox = null;

    private void Start()
    {
        myPlayerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Punch") && !myIsPunching)
        {
            StartCoroutine("IE_Punch");
        }
    }

    private IEnumerator IE_Punch()
    {
        myIsPunching = true;
        myPlayerMovement.SetCanMove(false);
        myAnimator.SetTrigger("Punch");

        List<Enemy> enemies = myHitBox.GetCollidingObjects();

        yield return new WaitForSeconds(myPunchAnimation.length / 2.0f);

        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i].gameObject);
        }

        yield return new WaitForSeconds(myPunchAnimation.length / 2.0f);

        myPlayerMovement.SetCanMove(true);
        myIsPunching = false;
    }
}
