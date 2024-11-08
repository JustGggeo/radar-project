using UnityEngine;

/// <summary>
/// Контроллер камеры для управления её перемещением и вращением.
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Скорость перемещения камеры.
    /// </summary>
    public float moveSpeed = 10f;

    /// <summary>
    /// Скорость вращения камеры.
    /// </summary>
    public float rotationSpeed = 100f;

    /// <summary>
    /// Метод, который вызывается каждый кадр.
    /// Управляет перемещением и вращением камеры.
    /// </summary>
    void Update()
    {
        // Управление перемещением камеры с помощью клавиш WASD
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.Self);

        // Управление высотой камеры с помощью клавиш Q и E
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World);
        }

        // Управление вращением камеры с помощью мыши
        float rotationHorizontal = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float rotationVertical = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, rotationHorizontal, Space.World);
        transform.Rotate(Vector3.right, rotationVertical, Space.Self);
    }
}
