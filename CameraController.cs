using UnityEngine;

/// <summary>
/// ���������� ������ ��� ���������� � ������������ � ���������.
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// �������� ����������� ������.
    /// </summary>
    public float moveSpeed = 10f;

    /// <summary>
    /// �������� �������� ������.
    /// </summary>
    public float rotationSpeed = 100f;

    /// <summary>
    /// �����, ������� ���������� ������ ����.
    /// ��������� ������������ � ��������� ������.
    /// </summary>
    void Update()
    {
        // ���������� ������������ ������ � ������� ������ WASD
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.Self);

        // ���������� ������� ������ � ������� ������ Q � E
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World);
        }

        // ���������� ��������� ������ � ������� ����
        float rotationHorizontal = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float rotationVertical = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, rotationHorizontal, Space.World);
        transform.Rotate(Vector3.right, rotationVertical, Space.Self);
    }
}
