using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerScript : MonoBehaviour
{
    public GameObject car;
    public float spawnRate = 5f;
    public float timeElapsed = 0f;
    public Vector3 targetRotation;
    public bool active = false;
    public GameObject finalCar;
    public float faster = 2f;
    public GameObject carDestination;
    public float despawnDistance = 10f;


    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > spawnRate)
            {
                var createdCar = Instantiate(car, transform.position, transform.rotation);
                createdCar.GetComponent<carScript>().directionVector = targetRotation;
                createdCar.GetComponent<carScript>().spawner = this;
                createdCar.GetComponent<carScript>().thresholdDistance = despawnDistance;
                timeElapsed = 0f;
            }
        }
    }

    public void RedLight()
    {
        var createdCar = Instantiate(finalCar, transform.position, transform.rotation);
        createdCar.GetComponent<FinalCarScript>().directionVector = targetRotation;
        createdCar.GetComponent<FinalCarScript>().destination = carDestination;
        active = false;
    }
}
