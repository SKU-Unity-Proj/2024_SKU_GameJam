using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMission2 : MonoBehaviour
{
    public GameObject toy;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == toy)
        {
            MissionManager.Instance.ChangeMissionStatus(2, false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == toy)
        {
            MissionManager.Instance.ChangeMissionStatus(2, true);
        }
    }
}
