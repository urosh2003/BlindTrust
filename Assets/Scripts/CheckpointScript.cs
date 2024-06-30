using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public CheckpointManagerScript manager;
    public int checkpointNumber;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grandpa"))
        {
            Debug.Log("CheckPoint!");
            if (checkpointNumber > CheckpointManagerScript.currentCheckpoint)
            {
                Debug.Log("CheckPoint!");
                CheckpointManagerScript.currentCheckpoint += 1;
            }
            
        }
    }
}
