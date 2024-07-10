using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMission1 : MonoBehaviour
{
    public GameObject coffee;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == coffee)
        {
            MissionManager.Instance.ChangeMissionStatus(1, false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == coffee)
        {
            MissionManager.Instance.ChangeMissionStatus(1, true);
        }
    }
}
