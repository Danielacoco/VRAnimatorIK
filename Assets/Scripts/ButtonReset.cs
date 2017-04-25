using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.UnityEventHelper;

public class ButtonReset : MonoBehaviour
{
    //public GameObject go;
    public PositionCapture animFunct;
    //public Transform dispenseLocation;

    private VRTK_Button_UnityEvents buttonEvents;

    private void Start()
    {
        buttonEvents = GetComponent<VRTK_Button_UnityEvents>();
        if (buttonEvents == null)
        {
            buttonEvents = gameObject.AddComponent<VRTK_Button_UnityEvents>();
        }
        buttonEvents.OnPushed.AddListener(handlePush);
    }

    private void handlePush(object sender, VRTK.Control3DEventArgs e)
    {
        Debug.Log("Pushed");

        animFunct.ResetAnim();
    }
}


