using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CheckpointManagerScript : MonoBehaviour
{
    public List<GameObject> checkpoints;
    public static int currentCheckpoint = 0;
    public GameObject pointer;
    public GameObject player;
    public GameObject grandpa;
   

    public void Respawn()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void Start()
    {
        player.transform.position = checkpoints[currentCheckpoint].transform.position;
        grandpa.transform.position = checkpoints[currentCheckpoint].transform.position + Vector3.forward;
        pointer.transform.position = checkpoints[currentCheckpoint].transform.position;

        player.GetComponent<PlayerMovementScript>().Respawn();
        grandpa.GetComponent<GrandpaMovementScript>().Respawn();
    }
}
