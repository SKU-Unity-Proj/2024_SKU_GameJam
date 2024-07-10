using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class CheckMission8 : MonoBehaviour
{
    public int i = 0;

    public GameObject medic1;
    public GameObject medic2;
    public GameObject medic3;
    public GameObject medic4;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == medic1 || other.gameObject == medic2 || other.gameObject == medic3 || other.gameObject == medic4)
        {
            i++;

            if(i > 3)
            {
                MissionManager.Instance.ChangeMissionStatus(8, true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == medic1 || other.gameObject == medic2 || other.gameObject == medic3 || other.gameObject == medic4)
        {
            i--;
        }
    }
}
