using UnityEngine;
using static IFKeyInteractable;

public class SleepToBed : MonoBehaviour, IFInteractable
{
    public float canDistance = 3f; // 상호작용 가능한 거리

    public GameObject canvas;

    public void Interact(float distance)
    {
        if(distance < canDistance)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            canvas.SetActive(true);
        }
    }
}
