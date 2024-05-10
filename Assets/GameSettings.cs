using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;
    public float DistanceBetweenObjects;
    public int SpawnCountOnInnerZone;
    public int SpawnCountOnMiddleZone;
    public int SpawnCountOnOuterZone;

    void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
        }
    }
}
