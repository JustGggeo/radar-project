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
    public class Triangulation 
    {
        private List<Point> _points;

        public Triangulation(List<Point> points)
        {
            if (points == null || points.Count < 3)
            {
                throw new ArgumentException("Points list must contain at least three points.");
            }

            _points = points;
        }

        public List<Triangle> Triangulate()
        {
            // Упорядочиваем точки по X, затем Y
            _points = _points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();

            List<Triangle> triangles = new List<Triangle>();

            for (int i = 2; i < _points.Count; ++i)
            {
                Point p1 = _points[0];
                Point p2 = _points[i];

                // Получаем точку пересечения с линией x = y
                double xIntersect = p1.X + (p2.Y - p1.Y) / (p2.X - p1.X) * (p2.X - p1.X);

                // Проверка на принадлежность отрезку [x1, x2]
                bool intersects = (p1.X <= xIntersect && xIntersect <= p2.X);
                if (!intersects) continue;

                int j = 1;
                while (j < i)
                {
                    Point p3 = _points[j++];

                    // Проверка на пересечение лучей с вершинами
                    if ((p1.Y > p3.Y) != (p2.Y > p3.Y))
                    {
                        if (p1.X < p3.X)
                        {
                            if (p2.X >= p3.X)
                            {
                                triangles.Add(new Triangle(p1, p3, p2));
                            }
                            else
                            {
                                triangles.Add(new Triangle(p1, p3, p2));
                                break;
                            }
                        }
                        else
                        {
                            if (p2.X >= p3.X)
                            {
                                triangles.Add(new Triangle(p1, p3, p2));
                                break;
                            }
                            else
                            {
                                triangles.Add(new Triangle(p1, p3, p2));
                            }
                        }
                    }
                }
            }

            return triangles;
        }

        public struct Point
        {
            public double X { get; set; }
            public double Y { get; set; }

            public Point(double x, double y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        public struct Triangle
        {
            public Point P1 { get; set; }
            public Point P2 { get; set; }
            public Point P3 { get; set; }

            public Triangle(Point p1, Point p2, Point p3)
            {
                this.P1 = p1;
                this.P2 = p2;
                this.P3 = p3;
            }
        }
    }
    public class RadarDataReceiver 
    {
        double pointx = testing.x;
        double pointy = testing.y;
        double pointz = testing.z;
    }
    /// <summary>
    /// Префаб объекта, который отображается в каждой точке.
    /// </summary>
    public GameObject pointPrefab;

    /// <summary>
    /// Ссылка на компонент приёма данных от локатора.
    /// </summary>
    RadarDataReceiver dataReceiver;

    /// <summary>
    /// Ссылка на компонент триангуляции.
    /// </summary>
    Triangulation triangulation;

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