using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GrapplingGun : MonoBehaviour {

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public float timeRemaining = 0;
    public double ceilingTime;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    private bool grap = false;
    public AudioSource shoot;
    public AudioSource cooldown;

    [SerializeField] public TextMeshProUGUI timer;

    void Awake() {
        lr = GetComponent<LineRenderer>();
    }

    void Update() {

        ceilingTime = System.Math.Ceiling(timeRemaining);
        timer.text = "Grapple Cooldown:\n" + ceilingTime;

        if (Input.GetMouseButtonDown(0) && timeRemaining <= 0) 
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0) && grap) 
        {
            grap = false;
            StopGrapple();
            timeRemaining = 10;
        }

        else if (Input.GetMouseButtonUp(0) && !grap)
        {
            StopGrapple();
        }

        if (timeRemaining > 0)
        {
            timeRemaining-=Time.deltaTime;
        }

        if ((timeRemaining > 0) && Input.GetMouseButtonDown(0))
        {
            Debug.Log("H");
            cooldown.Play();
        }
        

        
    }

    //Called after Update
    void LateUpdate() {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) {
            shoot.Play();
            grap = true;
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = 1f;
            joint.damper = 4f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple() {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;
    
    void DrawRope() {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
    }

}
