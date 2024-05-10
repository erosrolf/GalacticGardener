using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;
    public float DistanceBetweenObjects;
    public float DistanceToRedPoint;

    void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
        }
    }
}
