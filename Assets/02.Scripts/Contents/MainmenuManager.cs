using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainmenuManager : MonoBehaviour
{
    public GameObject nameInputUI; // �̸� �Է� UI ������Ʈ
    public TMP_InputField nameInputField; // �̸� �Է� �ʵ�

    private void Start()
    {
        // ���� �޴� BGM ���
        SoundManager.Instance.PlayBGM(SoundList.mainmenuSound);

        // �� ��ε� �̺�Ʈ�� �޼��� ���
        SceneManager.sceneUnloaded += OnSceneUnloaded;

        // �̸� �Է� UI ��Ȱ��ȭ
        nameInputUI.SetActive(false);

        // InputField�� onEndEdit �̺�Ʈ�� �޼��� ���
        nameInputField.onEndEdit.AddListener(OnNameInputEnd);
    }

    private void OnDestroy()
    {
        // �� ��ε� �̺�Ʈ���� �޼��� ����
        SceneManager.sceneUnloaded -= OnSceneUnloaded;

        // InputField�� onEndEdit �̺�Ʈ���� �޼��� ����
        nameInputField.onEndEdit.RemoveListener(OnNameInputEnd);
    }

    // �� ��ε�� �� ȣ��� �޼���
    private void OnSceneUnloaded(Scene current)
    {
        SoundManager.Instance.Stop(true);
    }

    // �� �̵� �Լ�
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ��ư Ŭ�� �� ȣ��� �Լ�
    public void OnPlayButtonClicked()
    {
        SoundManager.Instance.PlayOneShotEffect((int)SoundList.click, this.transform.position, 3f);
        // �̸� �Է� UI Ȱ��ȭ
        nameInputUI.SetActive(true);
        nameInputField.Select(); // �Է� �ʵ带 �����Ͽ� Ű���带 Ȱ��ȭ�մϴ�.
    }

    // InputField���� �̸� �Է��� ������ �� ȣ��� �Լ�
    private void OnNameInputEnd(string playerName)
    {
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(playerName))
        {
            // �̸��� �����մϴ�.
            AgentData.SetPlayerName(playerName);

            Debug.Log(AgentData.GetPlayerName());
            // �̸��� �ԷµǾ����� ���� �̵��մϴ�.
            LoadScene("Scene_01Ex"); // "GameScene"�� ���ϴ� �� �̸����� �����ϼ���.
        }
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
