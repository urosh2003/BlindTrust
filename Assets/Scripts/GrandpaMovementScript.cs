using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GrandpaMovementScript : MonoBehaviour
{
    public NavMeshAgent grandpa;
    public List<GameObject> targetDestinations;
    public static int nextDestination = 0;
    public float waitDuration = 3f;
    public float timer = 0f;
    public bool alive = true;
    public CheckpointManagerScript checkpoint;


    void Start()
    {
        grandpa.SetDestination(targetDestinations[nextDestination].transform.position);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitDuration && alive)
        {
            Continue();
        }
        if (grandpa.transform.position.x == targetDestinations[nextDestination].transform.position.x 
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

    public void Continue()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }

    void End()
    {
        return;
    }

    public void Dead()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        alive = false;
        Debug.Log("Died!");

        checkpoint.Respawn();
    }

    public void Respawn()
    {
        alive = true;
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        Continue();
    }

    public void Stop()
    {
        Debug.Log("Stopped");
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        timer = 0;
        Debug.Log("Stopped finished");

        //Continue();
    }
}
