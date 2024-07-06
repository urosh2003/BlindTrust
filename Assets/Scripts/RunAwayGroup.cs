using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayGroup : MonoBehaviour
{
    public List<GameObject> runaways;
    public int GroupSquirlBandit;

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
                if( GroupSquirlBandit == 1)
                {
                    grandpaScript.Dead(4);
                }
                if (GroupSquirlBandit == 2)
                {
                    grandpaScript.Dead(5);
                }
                if (GroupSquirlBandit == 3)
                {
                    grandpaScript.Dead(0);
                }
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
