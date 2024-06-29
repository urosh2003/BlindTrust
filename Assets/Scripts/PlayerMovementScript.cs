using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
                        playerCaught = false;
                        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
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

                    if (Physics.Raycast(ray, out hitPoint))
                    {
                        targetDest.transform.position = hitPoint.point;
                        player.SetDestination(hitPoint.point);
                    }
                }
            }
        }
    }

    public void DeadPlayer()
    {
        alive = false;
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;

        Debug.Log("Player died");

    }

    public void CaughtInATrap()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        clickPanel.SetActive(true);
        playerCaught = true;
        timeElapsed = 0f;
        clicks = 0;
    }
}
