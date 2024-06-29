using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GrandpaMovementScript : MonoBehaviour
{
    public NavMeshAgent grandpa;
    public List<GameObject> targetDestinations;
    private int nextDestination = -1;

    void Start()
    {
        GoToNextDestination();
    }

    void Update()
    {
        if(grandpa.transform.position.x == targetDestinations[nextDestination].transform.position.x 
            && grandpa.transform.position.z == targetDestinations[nextDestination].transform.position.z)
        {
            GoToNextDestination();
        }
    }

    void GoToNextDestination()
    {
        if (nextDestination == targetDestinations.Count - 1)
        {
            End();
        }
        else
        {
            nextDestination++;
            grandpa.SetDestination(targetDestinations[nextDestination].transform.position);
        }
    }

    void End()
    {
        return;
    }
}
