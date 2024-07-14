using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GrandpaMovementScript : MonoBehaviour
{
    public NavMeshAgent grandpa;
    public List<GameObject> targetDestinations;
    public static int destinationCheckpoint = 0;
    public int nextDestination;
    public float waitDuration = 3f;
    public float timer = 0f;
    public bool alive = true;
    public CheckpointManagerScript checkpoint;

    public Animator animator;

    public GameObject deathScreen;

    void Start()
    {
        nextDestination = destinationCheckpoint;
        targetDestinations[nextDestination].GetComponentInChildren<MeshRenderer>().enabled = true;
        grandpa.SetDestination(targetDestinations[nextDestination].transform.position);
    }

    void Update()
    {
        if(grandpa.velocity != Vector3.zero)
                {
                    animator.SetBool("Iswalking", true);
                }
                else if(grandpa.velocity == Vector3.zero)
                {
                    animator.SetBool("Iswalking", false);
                }
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
            targetDestinations[nextDestination].GetComponentInChildren<MeshRenderer>().enabled = false;
            nextDestination++;
            targetDestinations[nextDestination].GetComponentInChildren<MeshRenderer>().enabled = true;
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

    public void Dead(int scenario)
    {
        if (scenario == 3)
        {
            gameObject.SetActive(false);
        }
        if(scenario == 1 || scenario == 2)
        {
            gameObject.transform.localScale = new Vector3(1f, 0.01f, 1f);
        }
        deathScreen.GetComponent<DeathAnimationScript>().Dead(scenario);
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        alive = false;
        Debug.Log("Died!");
    }

    public void Respawn()
    {
        alive = true;
        gameObject.SetActive(true);
        gameObject.transform.localScale = Vector3.one;
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
