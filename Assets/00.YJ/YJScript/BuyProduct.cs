using UnityEngine;

public class BuyProduct : MonoBehaviour
{
    public GameObject selectProduct;
    private float distanceFromCamera = 1.0f;

    public GameObject carpet;
    public GameObject corner;
    public GameObject jokjoke;

    public void BuyProductBtn()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 cameraForward = Camera.main.transform.forward;

        // 카메라에서 지정된 거리만큼 떨어진 위치를 계산합니다.
        Vector3 spawnPosition = cameraPosition + cameraForward * distanceFromCamera;

        // 오브젝트를 생성합니다.
        if (selectProduct != null)
            Instantiate(selectProduct, spawnPosition, Quaternion.identity);
    }

    public void ChangeSelectProduct(int i)
    {
        if (i == 1)
            selectProduct = carpet;
        if (i == 2)
            selectProduct = corner;
        if (i == 3)
            selectProduct = jokjoke;
    }
}
