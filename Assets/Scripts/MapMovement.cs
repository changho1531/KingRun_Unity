using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // ���� �̵� �ӵ�

    private void Update()
    {
        // ���� Z �� �������� �̵���ŵ�ϴ�
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }
}
