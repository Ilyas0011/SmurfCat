using UnityEngine;

public class YourScript : MonoBehaviour
{
    [SerializeField] private float maxRightDistance = 3.0f;  // Максимальное расстояние для движения вправо
    [SerializeField] private float maxLeftDistance = 3.0f;   // Максимальное расстояние для движения влево
    [SerializeField] private float moveSpeed = 5.0f;          // Скорость движения
    [SerializeField] private float rotationSpeed = 300.0f;    // Скорость вращения

    private float initialX;             // Исходная позиция объекта по оси X
    private float currentDistance;      // Текущее расстояние, которое объект прошел
    private bool movingRight = true;     // Флаг, указывающий на направление движения

    private void Update()
    {
        // Перемещение объекта по оси X
        float movement = moveSpeed * Time.deltaTime;

        if (movingRight)
        {
            // Движение вправо
            transform.Translate(Vector3.right * movement, Space.World);  // Используем Space.World, чтобы игнорировать вращение по оси Y
            currentDistance += movement;

            if (currentDistance >= maxRightDistance)
                movingRight = false;  
        }
        else
        {
            // Движение влево
            transform.Translate(Vector3.left * movement, Space.World);  
            currentDistance -= movement;

            if (currentDistance <= -maxLeftDistance)
                movingRight = true; 
        }

        // Вращение объекта вокруг своей оси Z
        float rotation = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotation);
    }
}
