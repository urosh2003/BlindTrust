using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCarScript : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent car;
    public float carSpeed = 1;
    public Vector3 directionVector;
    public GameObject destination;

    // Start is called before the first frame update
    void Start()
    {
        car.SetDestination(destination.transform.position);
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
                grandpaScript.Dead();
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
