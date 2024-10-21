using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;

public class Visualistation : MonoBehaviour
{
    public GameObject pointPrefab;

    private List<Vector3> points = new List<Vector3>();
    //private TriangulationData triangulationData;

    void Start()
    {
        if (pointPrefab == null)
            Debug.LogError("Point prefab not set!");

        GeneratePoints();
        CreateTriangles();
    }

    private void GeneratePoints()
    {
        for (int i = 0; i < Random.Range(10, 20); i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));

            points.Add(randomPos);

            GameObject pointObj = Instantiate(pointPrefab, randomPos, Quaternion.identity);
            pointObj.transform.parent = this.transform;
        }
    }

    private void CreateTriangles()
    {
        // Convert to P2T format
        var pts = new List<Point>(points.Count);
        foreach (var v in points)
        {
            //pts.Add(new Point((float)v.x, (float)v.z));
        }

        //triangulationData = DelaunayTriangulation.CreateFromPointList(pts);

        // Get all edges from the triangulation
        //List<Edge> edges = triangulationData.GetEdges();

        // Draw lines between these points
        //foreach (var edge in edges)
        {
            LineRenderer line = gameObject.AddComponent<LineRenderer>();
            //line.SetVertexCount(2);
            //line.SetPosition(0, points[edge.B].ToUnityVector());
            //line.SetPosition(1, points[edge.A].ToUnityVector());
            //line.startColor = Color.white;
            //line.endColor = Color.white;
        }
    }
}