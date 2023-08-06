using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappable;
    public Transform gunTip, mainCamera, player;
    private float maxDistance = 40f, maxGrappleTime = 3.3f, currentGrappleTime = 0f;
    private SpringJoint joint;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if(IsGrappling())
        {
            currentGrappleTime += Time.deltaTime;

            if(currentGrappleTime >= maxGrappleTime)
            {
                EndGrapple();
            }
        }
        if(Input.GetMouseButtonDown(0) && !IsGrappling())
        {
            StartGrapple();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            EndGrapple();
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        currentGrappleTime = 0f;
        RaycastHit hit;
        if(Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, maxDistance, whatIsGrappable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4f;
            joint.damper = 8f;
            joint.massScale = 5f;

            lr.positionCount = 2;
        }
    }

    void EndGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }
    
    void DrawRope() 
    {
        if(!joint)
            return;
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }
    
    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
