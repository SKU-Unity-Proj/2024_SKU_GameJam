using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMission4 : MonoBehaviour
{
    public Transform baby;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == baby)
        {
            MissionManager.Instance.ChangeMissionStatus(4, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == baby)
        {
            MissionManager.Instance.ChangeMissionStatus(4, false);
        }
    }
}
