using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideManager : MonoBehaviour
{
    public LayerMask whatIsWalkable;

    [Header("Slide settings")]
    public float slideVelocity;
    float currentSlideVelocity;
    bool _isSliding;
    public bool isSliding
    {
        get => _isSliding;
        set
        {
            _isSliding = value;
        }
    }

    PhysicMaterial physMat;
    PlayerManager playerMgmt;

    private void Start()
    {
        physMat = GetComponent<CapsuleCollider>().material;
        playerMgmt = GetComponent<PlayerManager>();

        currentSlideVelocity = slideVelocity;
    }

    private void FixedUpdate()
    {
        HandleSliding();
    }

    void HandleSliding()
    {
        Vector3 adjustedPos = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
        if (!CheckSlope(adjustedPos, Vector3.down, 10f))
        {
            _isSliding = false;
            currentSlideVelocity = 0f;
        }
        else
        {
            _isSliding = true;
            currentSlideVelocity = slideVelocity;
            physMat.dynamicFriction = 0f;
            playerMgmt.myRb.velocity += -Vector3.up * slideVelocity;
        }
    }

    bool CheckSlope(Vector3 position, Vector3 desiredDirection, float distance)
    {
        Debug.DrawRay(position, desiredDirection, Color.green);

        Ray myRay = new Ray(position, desiredDirection); // cast a Ray from the position of our gameObject into our desired direction. Add the slopeRayHeight to the Y parameter.
        RaycastHit hit;

        if (Physics.Raycast(myRay, out hit, distance, whatIsWalkable))
        {
            float slopeAngle = Mathf.Deg2Rad * Vector3.Angle(Vector3.up, hit.normal); // Here we get the angle between the Up Vector and the normal of the wall we are checking against: 90 for straight up walls, 0 for flat ground.

            if (slopeAngle >= 45f * Mathf.Deg2Rad) //You can set "steepSlopeAngle" to any angle you wish.
            {
                return true; // return false if we are very near / on the slope && the slope is steep
            }

            return false; // return true if the slope is not steep
        }

        return false;
    }
}
