using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TrapScript : MonoBehaviour
{
    public bool trapEnabled = true;

    void OnTriggerEnter(Collider other)
    {
        if (trapEnabled)
        {
            Debug.Log("Dead");
            if (other.gameObject.CompareTag("Grandpa"))
            {
                //grandpa.SetDestination(grandpa.transform.position);

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
                //grandpa.SetDestination(grandpa.transform.position);

                Debug.Log("Dead player");

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
        else
        {
            Debug.Log("TrapDisabled");
        }
    }

    public void DisableTrap()
    {
        trapEnabled = false;
    }
}
