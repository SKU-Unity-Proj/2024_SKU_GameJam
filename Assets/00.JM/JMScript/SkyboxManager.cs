using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public Material[] skyboxMaterials; // 사용할 스카이박스들을 배열로 저장
    private int currentSkyboxIndex = 0; // 현재 스카이박스 인덱스

    private void Start()
    {
        if (skyboxMaterials.Length > 0)
            RenderSettings.skybox = skyboxMaterials[currentSkyboxIndex];
    }

    // 스카이박스를 특정 인덱스로 변경하는 함수
    public void SetSkybox(int index)
    {
        if (index >= 0 && index < skyboxMaterials.Length)
        {
            RenderSettings.skybox = skyboxMaterials[index];
            currentSkyboxIndex = index;
            DynamicGI.UpdateEnvironment(); // 변경된 환경 반영
        }
        else
        {
            Debug.LogWarning("Skybox index out of range!");
        }
    }

    // 다음 스카이박스로 변경하는 함수 (순환)
    public void NextSkybox()
    {
        currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxMaterials.Length;
        SetSkybox(currentSkyboxIndex);
    }
}
