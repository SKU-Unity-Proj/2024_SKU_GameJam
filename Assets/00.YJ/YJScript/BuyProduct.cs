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

        // ī�޶󿡼� ������ �Ÿ���ŭ ������ ��ġ�� ����մϴ�.
        Vector3 spawnPosition = cameraPosition + cameraForward * distanceFromCamera;

        // ������Ʈ�� �����մϴ�.
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
