using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightScript : MonoBehaviour
{
    public CarSpawnerScript spawner;
    public float stopDuration = 10f;
    public GameObject redLightWalking;
    public GameObject greenLightWalking;
    public GameObject redLightCars;
    public GameObject greenLightCars;

    public void StopCars()
    {
        if (spawner.active == true){
            spawner.RedLight();
            redLightWalking.SetActive(false);
            greenLightWalking.SetActive(true);
            redLightCars.SetActive(true);
            greenLightCars.SetActive(false);
}
    }
}
