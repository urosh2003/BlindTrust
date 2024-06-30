using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour
{

    public Transform droppedThing;
    public bool dropped = false;
    public float velocity = 1f;

    void Start()
    {
        
    }

    public void Update()
    {
        if (dropped)
        {
            droppedThing.transform.position += Vector3.down * Time.deltaTime * velocity;
        }
    }

    public void Drop()
    {
        dropped = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Drop Hit");

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
