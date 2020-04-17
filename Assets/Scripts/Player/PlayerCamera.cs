using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float mySpeed = 2.0f;

    [SerializeField]
    private Transform myPlayerTransform = null;

    private Rigidbody2D myPlayerRigidBody = null;

    private Transform myTransform = null;
    private float myZStart = 0.0f;

    [SerializeField]
    private float myYOffset = 2.0f;

    [SerializeField]
    private float myPlayerVelocityImpactX = 3.0f;

    [SerializeField]
    private float myPlayerVelocityImpactY = 1.0f;

    private bool myWantToSeeUnder = false;

    [SerializeField]
    private float myDownVisibility = 3.0f;

    private void Start()
    {
        myTransform = transform;
        myZStart = myTransform.position.z;
        myPlayerRigidBody = myPlayerTransform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 targetPos = myPlayerTransform.position;
        targetPos.z = myZStart;
        targetPos.y += myYOffset;
        Vector2 playerVel = myPlayerRigidBody.velocity.normalized;
        targetPos += new Vector3(playerVel.x * myPlayerVelocityImpactX, playerVel.y * myPlayerVelocityImpactY, 0);
        if(myWantToSeeUnder)
        {
            targetPos += Vector3.down * myDownVisibility;
        }
        myTransform.position = Vector3.Lerp(myTransform.position, targetPos, Time.deltaTime * mySpeed);
    }

    public void SetWantToSeeUnder(bool aNewState)
    {
        myWantToSeeUnder = aNewState;
    }
}
