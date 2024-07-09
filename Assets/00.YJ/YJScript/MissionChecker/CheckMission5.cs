using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMission5 : MonoBehaviour
{
    public Transform wineBottle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == wineBottle)
        {
            MissionManager.Instance.ChangeMissionStatus(5, false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == wineBottle)
        {
            MissionManager.Instance.ChangeMissionStatus(5, true);
        }
    }
}
