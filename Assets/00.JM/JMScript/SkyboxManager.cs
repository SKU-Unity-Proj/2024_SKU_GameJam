using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public Material[] skyboxMaterials; // ����� ��ī�̹ڽ����� �迭�� ����
    private int currentSkyboxIndex = 0; // ���� ��ī�̹ڽ� �ε���

    private void Start()
    {
        if (skyboxMaterials.Length > 0)
            RenderSettings.skybox = skyboxMaterials[currentSkyboxIndex];
    }

    // ��ī�̹ڽ��� Ư�� �ε����� �����ϴ� �Լ�
    public void SetSkybox(int index)
    {
        if (index >= 0 && index < skyboxMaterials.Length)
        {
            RenderSettings.skybox = skyboxMaterials[index];
            currentSkyboxIndex = index;
            DynamicGI.UpdateEnvironment(); // ����� ȯ�� �ݿ�
        }
        else
        {
            Debug.LogWarning("Skybox index out of range!");
        }
    }

    // ���� ��ī�̹ڽ��� �����ϴ� �Լ� (��ȯ)
    public void NextSkybox()
    {
        currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxMaterials.Length;
        SetSkybox(currentSkyboxIndex);
    }
}
