using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public float speed = 1.0f; // ī�޶� �̵� �ӵ�
    public float minX = -100.0f; // X�� �ּ� ��ġ
    public float maxX = 100.0f; // X�� �ִ� ��ġ
    private bool movingRight = true;

    private void Update()
    {
        // X�� �������� ī�޶� �̵���ŵ�ϴ�.
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (transform.position.x > maxX)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (transform.position.x < minX)
            {
                movingRight = true;
            }
        }
    }
}