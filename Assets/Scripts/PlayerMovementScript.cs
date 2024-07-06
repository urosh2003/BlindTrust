using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    public Camera camera;
    public NavMeshAgent player;
    public GameObject targetDest;
    public bool playerCaught = false;
    public int clicksRequired = 5;
    public int clicks = 0;
    public float clickingDuration = 2f;
    public float timeElapsed = 0f;
    public GameObject clickPanel;
    public bool alive = true;
    public CheckpointManagerScript checkpoint;

    public Animator animator;
    public GameObject deathScreen;
    public PlayerInput input;


    void Update()
    {
        if (alive)
        {
            if (playerCaught)
            {
                timeElapsed += Time.deltaTime;
                if (timeElapsed > clickingDuration)
                {
                    DeadPlayer();
                }
                if(Input.GetKeyDown("space"))
                {
                    clicks += 1;
                    if (clicks >= clicksRequired)
                    {
                        clickPanel.SetActive(false);
                        gameObject.transform.Find("Ker Animacija").gameObject.SetActive(true);

                        playerCaught = false;
                        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                        input.ActivateInput();
                        clicks = 0;
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (!playerCaught)
                {
                    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitPoint;

                    if (Physics.Raycast(ray, out hitPoint, Mathf.Infinity, LayerMask.GetMask("Walkable")))
                    {
                        targetDest.transform.position = hitPoint.point + new Vector3(0,0.3f,0);
                        player.ResetPath();
                        player.SetDestination(hitPoint.point);
                    }

                }

                
        
            }
            if(player.velocity != Vector3.zero)
                {
                    animator.SetBool("Iswalking", true);
                }
                else if(player.velocity == Vector3.zero)
                {
                    animator.SetBool("Iswalking", false);
                }
        }
    }

    public void DeadPlayer()
    {
        Time.timeScale = 0;
        alive = false;
        clickPanel.GetComponent<ButtonMashAnimationScript>().Func_StopUIAnim();
        clickPanel.SetActive(false);

        gameObject.GetComponent<NavMeshAgent>().isStopped = true;

        Debug.Log("Player died");

        deathScreen.SetActive(true);

    }

    public void Respawn()
    {

        clickPanel.SetActive(false);
        playerCaught = false;
        alive = true;
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }

    public void CaughtInATrap()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        gameObject.transform.Find("Ker Animacija").gameObject.SetActive(false);
        clickPanel.SetActive(true);
        clickPanel.GetComponent<ButtonMashAnimationScript>().Func_PlayUIAnim();
        playerCaught = true;
        input.DeactivateInput();
        timeElapsed = 0f;
        clicks = 0;
    }
}
