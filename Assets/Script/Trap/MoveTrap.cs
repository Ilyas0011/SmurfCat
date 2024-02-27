using UnityEngine;

public class YourScript : MonoBehaviour
{
    [SerializeField] private float maxRightDistance = 3.0f;  // ������������ ���������� ��� �������� ������
    [SerializeField] private float maxLeftDistance = 3.0f;   // ������������ ���������� ��� �������� �����
    [SerializeField] private float moveSpeed = 5.0f;          // �������� ��������
    [SerializeField] private float rotationSpeed = 300.0f;    // �������� ��������

    private float initialX;             // �������� ������� ������� �� ��� X
    private float currentDistance;      // ������� ����������, ������� ������ ������
    private bool movingRight = true;     // ����, ����������� �� ����������� ��������

    private void Update()
    {
        // ����������� ������� �� ��� X
        float movement = moveSpeed * Time.deltaTime;

        if (movingRight)
        {
            // �������� ������
            transform.Translate(Vector3.right * movement, Space.World);  // ���������� Space.World, ����� ������������ �������� �� ��� Y
            currentDistance += movement;

            if (currentDistance >= maxRightDistance)
                movingRight = false;  
        }
        else
        {
            // �������� �����
            transform.Translate(Vector3.left * movement, Space.World);  
            currentDistance -= movement;

            if (currentDistance <= -maxLeftDistance)
                movingRight = true; 
        }

        // �������� ������� ������ ����� ��� Z
        float rotation = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotation);
    }
}
