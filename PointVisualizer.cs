using System.Collections;
using UnityEngine;
using System.Globalization;

/// <summary>
/// ����� ��� ������������ ����� �� ����� Unity �� ����� � �������.
/// </summary>
public class PointVisualizer : MonoBehaviour
{
    /// <summary>
    /// ���� � ����� � �������.
    /// </summary>
    public string filePath = "Assets/points.txt";

    /// <summary>
    /// ������ �����, ����������� �� �����.
    /// </summary>
    public float pointSize = 0.1f;

    /// <summary>
    /// ���� �����, ����������� �� �����.
    /// </summary>
    public Color pointColor = Color.red;

    /// <summary>
    /// ���������� �����, ����������� �� ���� ����.
    /// </summary>
    public int pointsPerFrame = 10;

    /// <summary>
    /// �����, ������� ���������� ��� ������� �������.
    /// ��������� �������� ��� �������� � ����������� �����.
    /// </summary>
    void Start()
    {
        StartCoroutine(LoadAndDisplayPoints());
    }

    /// <summary>
    /// �������� ��� �������� ����� �� ����� � �� ����������� �� �����.
    /// </summary>
    /// <returns>���������� IEnumerator ��� ���������� ����������� ��������.</returns>
    IEnumerator LoadAndDisplayPoints()
    {
        // ������ ��� ������ �� �����
        string[] lines = System.IO.File.ReadAllLines(filePath);
        int count = 0;

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // ��������� ������ �� ����� � ������������
            var values = line.Split(' ');

            if (values.Length < 5)
            {
                Debug.LogWarning("������������ �������� � ������: " + line);
                continue;
            }

            if (float.TryParse(values[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float _x) &&
                float.TryParse(values[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float _y) &&
                float.TryParse(values[2], NumberStyles.Float, CultureInfo.InvariantCulture, out float _z) &&
                float.TryParse(values[3], NumberStyles.Float, CultureInfo.InvariantCulture, out float _r) &&
                float.TryParse(values[4], NumberStyles.Float, CultureInfo.InvariantCulture, out float _phi))
            {
                Vector3 position = CalcCoords(_x, _y, _z, _r, _phi);
                CreatePoint(position);
                count++;

                // ���������, ����� �� ������� ����� ��� �����������
                if (count >= pointsPerFrame)
                {
                    count = 0;
                    yield return null; // ��� ���� ����, ����� Unity �������� �����
                }
            }
            else
            {
                Debug.LogError("������ ������� � ������: " + line);
            }
        }
    }

    /// <summary>
    /// ��������� ���������� ����� �� ������ ������� ����������.
    /// </summary>
    /// <param name="_x">���������� X.</param>
    /// <param name="_y">���������� Y.</param>
    /// <param name="_z">���������� Z.</param>
    /// <param name="_r">������.</param>
    /// <param name="_phi">����.</param>
    /// <returns>���������� ��������� �����.</returns>
    Vector3 CalcCoords(float _x, float _y, float _z, float _r, float _phi)
    {
        float x = _x + _r * Mathf.Cos(_phi);
        float y = _y + _r * Mathf.Sin(_phi);
        float z = _z;
        return new Vector3(x, y, z);
    }

    /// <summary>
    /// ������� ����� � �������� �������.
    /// </summary>
    /// <param name="position">�������, � ������� ����� ������� �����.</param>
    void CreatePoint(Vector3 position)
    {
        GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        point.transform.position = position;
        point.transform.localScale = Vector3.one * pointSize;

        var renderer = point.GetComponent<Renderer>();
        renderer.material.color = pointColor;

        Destroy(point.GetComponent<Collider>());
    }
}
