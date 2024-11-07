using System.Collections;
using UnityEngine;
using System.Globalization;

/// <summary>
/// Класс для визуализации точек на сцене Unity из файла с данными.
/// </summary>
public class PointVisualizer : MonoBehaviour
{
    /// <summary>
    /// Путь к файлу с данными.
    /// </summary>
    public string filePath = "Assets/points.txt";

    /// <summary>
    /// Размер точек, создаваемых на сцене.
    /// </summary>
    public float pointSize = 0.1f;

    /// <summary>
    /// Цвет точек, создаваемых на сцене.
    /// </summary>
    public Color pointColor = Color.red;

    /// <summary>
    /// Количество точек, создаваемых за один кадр.
    /// </summary>
    public int pointsPerFrame = 10;

    /// <summary>
    /// Метод, который вызывается при запуске объекта.
    /// Запускает корутину для загрузки и отображения точек.
    /// </summary>
    void Start()
    {
        StartCoroutine(LoadAndDisplayPoints());
    }

    /// <summary>
    /// Корутину для загрузки точек из файла и их отображения на сцене.
    /// </summary>
    /// <returns>Возвращает IEnumerator для управления выполнением корутины.</returns>
    IEnumerator LoadAndDisplayPoints()
    {
        // Читаем все строки из файла
        string[] lines = System.IO.File.ReadAllLines(filePath);
        int count = 0;

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Разделяем строку на части и обрабатываем
            var values = line.Split(' ');

            if (values.Length < 5)
            {
                Debug.LogWarning("Недостаточно значений в строке: " + line);
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

                // Проверяем, нужно ли сделать паузу для отображения
                if (count >= pointsPerFrame)
                {
                    count = 0;
                    yield return null; // Ждём один кадр, чтобы Unity обновила сцену
                }
            }
            else
            {
                Debug.LogError("Ошибка формата в строке: " + line);
            }
        }
    }

    /// <summary>
    /// Вычисляет координаты точки на основе входных параметров.
    /// </summary>
    /// <param name="_x">Координата X.</param>
    /// <param name="_y">Координата Y.</param>
    /// <param name="_z">Координата Z.</param>
    /// <param name="_r">Радиус.</param>
    /// <param name="_phi">Угол.</param>
    /// <returns>Координаты векторной точки.</returns>
    Vector3 CalcCoords(float _x, float _y, float _z, float _r, float _phi)
    {
        float x = _x + _r * Mathf.Cos(_phi);
        float y = _y + _r * Mathf.Sin(_phi);
        float z = _z;
        return new Vector3(x, y, z);
    }

    /// <summary>
    /// Создает точку в заданной позиции.
    /// </summary>
    /// <param name="position">Позиция, в которой будет создана точка.</param>
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
