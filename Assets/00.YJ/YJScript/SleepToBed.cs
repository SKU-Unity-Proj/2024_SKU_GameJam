using UnityEngine;
using static IFKeyInteractable;

public class SleepToBed : MonoBehaviour, IFInteractable
{
    public float canDistance = 3f; // ��ȣ�ۿ� ������ �Ÿ�

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
