using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChecklistManager : MonoBehaviour
{
    public List<TextMeshPro> checklistItems; // üũ����Ʈ �׸��

    // üũ����Ʈ�� ������Ʈ�ϴ� �Լ�
    public void UpdateChecklist(int missionId, bool isCompleted)
    {
        if (missionId >= 0 && missionId <= checklistItems.Count)
        {
            checklistItems[missionId].gameObject.SetActive(isCompleted);
        }
        else
        {
            Debug.LogError("Invalid mission ID.");
        }
    }
}
