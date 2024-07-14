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
    public GameObject promptPanel;

    public void Respawn()
    {
        Time.timeScale = 1;
        promptPanel.SetActive(false);
        Debug.Log("Respawned");
        SceneManager.LoadScene("SampleScene 2");
        Time.timeScale = 1;
        StartCoroutine(InitializeAfterSceneLoad());

    }

    void Start()
    {
        promptPanel.SetActive(false);

        player.transform.position = checkpoints[currentCheckpoint].transform.position;
        grandpa.transform.position = checkpoints[currentCheckpoint].transform.position + Vector3.forward;
        pointer.transform.position = checkpoints[currentCheckpoint].transform.position;

        player.GetComponent<PlayerMovementScript>().Respawn();
        grandpa.GetComponent<GrandpaMovementScript>().Respawn();

        Time.timeScale = 1;
    }

    IEnumerator InitializeAfterSceneLoad()
    {
        // Wait for the end of frame to ensure the scene is fully loaded
        yield return new WaitForEndOfFrame();

        player.transform.position = checkpoints[currentCheckpoint].transform.position;
        grandpa.transform.position = checkpoints[currentCheckpoint].transform.position + Vector3.forward;
        pointer.transform.position = checkpoints[currentCheckpoint].transform.position;

        player.GetComponent<PlayerMovementScript>().Respawn();
        grandpa.GetComponent<GrandpaMovementScript>().Respawn();
        promptPanel.SetActive(false);


        Time.timeScale = 1;
    }
}
