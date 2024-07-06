using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carScript : MonoBehaviour
{
    public Transform car;
    public float carSpeed = 1;
    public Vector3 directionVector;
    public CarSpawnerScript spawner;
    public float thresholdDistance = 10f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawner.timeElapsed > 0)
        {
            car.position += directionVector * Time.deltaTime * carSpeed;
            if(Vector3.Distance(car.transform.position, spawner.transform.position) > thresholdDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Car Hit");

        if (other.gameObject.CompareTag("Grandpa"))
        {
            Debug.Log("Dead Grandpa");

            var grandpaScript = other.gameObject.GetComponent<GrandpaMovementScript>();
            if (grandpaScript != null)
            {
                grandpaScript.Dead(2);
                spawner.GetComponent<CarSpawnerScript>().active = false;
            }
            else
            {
                Debug.LogError("GrandpaMovementScript component not found on grandpa.");
            }
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            var playerScript = other.gameObject.GetComponent<PlayerMovementScript>();
            if (playerScript != null)
            {
                playerScript.DeadPlayer();
            }
            else
            {
                Debug.LogError("PlayerMovementScript component not found on player.");
            }
        }
    }
}
