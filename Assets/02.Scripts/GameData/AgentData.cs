using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentData : MonoBehaviour
{
    private static string agentName;

    public static void SetPlayerName(string name)
    {
        agentName = name;
    }

    public static string GetPlayerName()
    {
        return agentName;
    }
}
