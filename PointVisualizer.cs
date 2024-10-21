using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// ����� ��� ������������ ����� �� ������ ������ ��������.
/// ������ ������� � ����� �� ������ ������������.
/// </summary>
public class PointVisualizer : MonoBehaviour
{
    public GameObject pointPrefab;

    RadarDataReceiver dataReceiver;

    

    void Update()
    {
        
            // ����������� ������ �������� � ����������
            Vector3 pointPosition = testing.position;

            // ������� ������ � �����
            Instantiate(pointPrefab, pointPosition, Quaternion.identity);

        // ������� ������ ����� ���������, ����� �������� ������������ ��������
        dataReceiver.inputData.Clear();
    }
}