using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public float speed = 1.0f; // 카메라 이동 속도
    public float minX = -100.0f; // X축 최소 위치
    public float maxX = 100.0f; // X축 최대 위치
    private bool movingRight = true;

    private void Update()
    {
        // X축 방향으로 카메라를 이동시킵니다.
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