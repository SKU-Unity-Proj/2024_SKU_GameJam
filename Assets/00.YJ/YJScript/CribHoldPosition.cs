using UnityEngine;

public class CribHoldPosition : MonoBehaviour
{
    public Transform baby;
    public Transform holdPosition;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == baby)
        {
            baby.transform.position = holdPosition.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == baby)
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
