using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// Класс для визуализации точек на основе данных локатора.
/// Создаёт объекты в сцене по данным триангуляции.
/// </summary>
public class PointVisualizer : MonoBehaviour
{
    public GameObject pointPrefab;

    RadarDataReceiver dataReceiver;

    

    void Update()
    {
        
            // Преобразуем данные локатора в координаты
            Vector3 pointPosition = testing.position;

            // Создаем объект в сцене
            Instantiate(pointPrefab, pointPosition, Quaternion.identity);

        // Очищаем список после обработки, чтобы избежать дублирования объектов
        dataReceiver.inputData.Clear();
    }
}