using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuManager : MonoBehaviour
{
    // �� �̵� �Լ�
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ��ư Ŭ�� �� ȣ��� �Լ�
    public void OnPlayButtonClicked()
    {
        LoadScene("Scene_01Ex"); // "GameScene"�� ���ϴ� �� �̸����� �����ϼ���.
    }

    // �ٸ� ��ư Ŭ�� �Լ� ����
    public void OnSettingsButtonClicked()
    {
        LoadScene("SettingsScene"); // "SettingsScene"�� ���ϴ� �� �̸����� �����ϼ���.
    }

    // ���� ��ư Ŭ�� �Լ� ����
    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}