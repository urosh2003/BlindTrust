using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightScript : MonoBehaviour
{
    public CarSpawnerScript spawner;
    public float stopDuration = 10f;

    public void StopCars()
    {
        spawner.RedLight();
    }
}
