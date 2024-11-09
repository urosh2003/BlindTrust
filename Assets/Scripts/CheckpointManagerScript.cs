using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CheckpointManagerScript : MonoBehaviour
{
    public List<GameObject> checkpoints;
    public static int currentCheckpoint = 0;
    public GameObject player;
    public GameObject grandpa;
   

    public void Respawn()
    {
        Time.timeScale = 1;

        Debug.Log("Respawned");
        SceneManager.LoadScene("Level");
        Time.timeScale = 1;
        StartCoroutine(InitializeAfterSceneLoad());

    }

    void Start()
    {
        /*
        player.transform.position = checkpoints[currentCheckpoint].transform.position;
        grandpa.transform.position = checkpoints[currentCheckpoint].transform.position + Vector3.forward;
        */
        player.GetComponent<PlayerMovementScript>().Respawn();
        grandpa.GetComponent<GrandpaMovementScript>().Respawn();

        Time.timeScale = 1;
    }

    IEnumerator InitializeAfterSceneLoad()
    {
        // Wait for the end of frame to ensure the scene is fully loaded
        yield return new WaitForEndOfFrame();
        /*
        player.transform.position = checkpoints[currentCheckpoint].transform.position;
        grandpa.transform.position = checkpoints[currentCheckpoint].transform.position + Vector3.forward;
        */
        player.GetComponent<PlayerMovementScript>().Respawn();
        grandpa.GetComponent<GrandpaMovementScript>().Respawn();

        Time.timeScale = 1;
    }
}
