using UnityEngine;
using TMPro;

public class BabyNameUpdate : MonoBehaviour
{
    private TextMeshProUGUI babyName;

    void Start()
    {
        babyName = GetComponent<TextMeshProUGUI>();

        babyName.text = AgentData.GetPlayerName();
    }
}
