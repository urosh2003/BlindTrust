using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerScript : MonoBehaviour
{
    public GameObject car;
    public float spawnRate = 5f;
    public float timeElapsed = 0f;
    public Vector3 targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        var createdCar = Instantiate(car, transform.position, Quaternion.identity);
        createdCar.GetComponent<carScript>().directionVector = targetRotation;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > spawnRate)
        {
            var createdCar = Instantiate(car, transform.position, transform.rotation);
            createdCar.GetComponent<carScript>().directionVector = targetRotation;
            timeElapsed = 0f;
        }
    }
}
