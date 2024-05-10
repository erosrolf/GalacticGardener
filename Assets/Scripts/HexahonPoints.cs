using System;
using UnityEngine;

public class HexagonPoints
{
    public static Vector3[] CalculatePoints(float radius)
    {
        Vector3[] points = new Vector3[6];
        for (int i = 0; i < 6; i++)
        {
            float angleDegrees = 60 * i;
            float angleRadians = Mathf.PI / 180 * angleDegrees;
            points[i] = new Vector3(radius * Mathf.Sin(angleRadians), radius * Mathf.Cos(angleRadians), 0);
        }
        return points;
    }
}