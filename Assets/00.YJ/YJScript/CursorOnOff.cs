using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorOnOff : MonoBehaviour
{
    public FirstPersonCam firstPersonCam;

    private void Awake()
    {
        firstPersonCam = FindObjectOfType<FirstPersonCam>();
    }

    public void CursorOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        firstPersonCam.enabled = false;
    }

    public void CursorOff()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        firstPersonCam.enabled = true;
    }
}
