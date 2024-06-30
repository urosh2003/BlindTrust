using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayGroup : MonoBehaviour
{
    public List<GameObject> runaways;

    public void BarkedAt()
    {
        foreach(var runaway in runaways)
        {
            runaway.GetComponent<RunAwaySingleScript>().BarkedAt();
        }
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Group Hit");

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
                playerScript.CaughtInATrap();
            }
            else
            {
                Debug.LogError("PlayerMovementScript component not found on player.");
            }
        }
    }
}
