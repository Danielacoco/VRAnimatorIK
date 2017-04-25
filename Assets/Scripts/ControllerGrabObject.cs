using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;

    private GameObject collidingObject;

    private GameObject holdingObject;

    public PositionCapture animFunct;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Update()
    {

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            if (collidingObject)
            {
                GrabObject();
            }

        }

        if (Controller.GetHairTriggerDown())
        {
            animFunct.CapturePos();
        }
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //animFunct.PlayAnim();
        }
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            if (holdingObject)
            {
                LetGoObject();
            }
        }

    }


    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObject = col.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("enter");
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        //Debug.Log("stay");
        //Debug.Log(other.tag);
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
       // Debug.Log("out");
        if (!collidingObject)
        {
            return;
        }
        collidingObject = null;
    }


    //Grabbing an object
    private void GrabObject()
    {
        holdingObject = collidingObject;
        collidingObject = null;
        //Debug.Log("yas");

        var joint = AddFixedJoint();
        joint.connectedBody = holdingObject.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fix = gameObject.AddComponent<FixedJoint>();
        fix.breakForce = 200000;
        fix.breakTorque = 20000;
        return fix;
    }

    //Letting go of Object

    private void LetGoObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            //holdingObject.GetComponent<Rigidbody>().velocity = Controller.velocity;
            // holdingObject.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;

        }

        holdingObject = null;
    }
}