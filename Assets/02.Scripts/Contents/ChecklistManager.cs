using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChecklistManager : MonoBehaviour
{
    public List<TextMeshPro> checklistItems; // 체크리스트 항목들

    // 체크리스트를 업데이트하는 함수
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
