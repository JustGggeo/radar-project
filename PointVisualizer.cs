using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ��� ������������ ����� �� ������ ������ ��������.
/// ������ ������� � ����� �� ������ ������������.
/// </summary>
public class PointVisualizer : MonoBehaviour
{
    /// <summary>
    /// ������ �������, ������� ������������ � ������ �����.
    /// </summary>
    public GameObject pointPrefab;

    /// <summary>
    /// ������ �� ��������� ����� ������ �� ��������.
    /// </summary>
    public RadarDataReceiver dataReceiver;

    /// <summary>
    /// ������ �� ��������� ������������.
    /// </summary>
    public Triangulation triangulation;

    void Update()
    {
        foreach (Vector2 data in dataReceiver.inputData)
        {
            // ����������� ������ �������� � ����������
            Vector3 pointPosition = triangulation.RadarTo3DPoint(data, dataReceiver.radarPosition);

            // ������� ������ � �����
            Instantiate(pointPrefab, pointPosition, Quaternion.identity);
        }

        // ������� ������ ����� ���������, ����� �������� ������������ ��������
        dataReceiver.inputData.Clear();
    }
}