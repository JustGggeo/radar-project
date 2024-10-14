using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс для визуализации точек на основе данных локатора.
/// Создаёт объекты в сцене по данным триангуляции.
/// </summary>
public class PointVisualizer : MonoBehaviour
{
    /// <summary>
    /// Префаб объекта, который отображается в каждой точке.
    /// </summary>
    public GameObject pointPrefab;

    /// <summary>
    /// Ссылка на компонент приёма данных от локатора.
    /// </summary>
    public RadarDataReceiver dataReceiver;

    /// <summary>
    /// Ссылка на компонент триангуляции.
    /// </summary>
    public Triangulation triangulation;

    void Update()
    {
        foreach (Vector2 data in dataReceiver.inputData)
        {
            // Преобразуем данные локатора в координаты
            Vector3 pointPosition = triangulation.RadarTo3DPoint(data, dataReceiver.radarPosition);

            // Создаем объект в сцене
            Instantiate(pointPrefab, pointPosition, Quaternion.identity);
        }

        // Очищаем список после обработки, чтобы избежать дублирования объектов
        dataReceiver.inputData.Clear();
    }
}