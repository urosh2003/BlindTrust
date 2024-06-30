using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerRaycastScript : MonoBehaviour
{
    public float castRadius;
    public bool holding;
    public GameObject heldObject;
    public Transform player;
    public float hitRange = 10000;
    public float barkCooldown = 10f;
    public float timeElapsed = -1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeElapsed > 0)
        {
            timeElapsed += Time.deltaTime;
        }
        if(timeElapsed > 10)
        {
            timeElapsed = -1;
        }
    }

    public void Bark(InputAction.CallbackContext context)
    {
        if (context.performed && timeElapsed < 0f)
        {
            Debug.Log("Woof");

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.SphereCast(transform.position, castRadius, transform.TransformDirection(Vector3.forward), out hit, hitRange))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (hitObject.CompareTag("Grandpa"))
                {
                    Debug.Log("Did Hit Grandpa");
                    hitObject.GetComponent<GrandpaMovementScript>().Stop();
                }
                else if (hitObject.CompareTag("RunAway"))
                {
                    hitObject.GetComponent<RunAwayGroup>().BarkedAt();
                }
            }
            else
            {
                Debug.Log("Did not Hit");
            }

            timeElapsed = 0;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Interact");


            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.SphereCast(transform.position, castRadius, transform.TransformDirection(Vector3.forward), out hit, hitRange))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (hitObject.CompareTag("Grandpa"))
                {
                    Debug.Log("Did Hit Grandpa");
                }
                else if (hitObject.CompareTag("TrafficLight"))
                {
                    hitObject.GetComponent<TrafficLightScript>().StopCars();
                    Debug.Log("Disabled Trap");
                }
                if (hitObject.CompareTag("Trap"))
                {
                    if (holding)
                    {
                        holding = false;
                        heldObject = null;
                        hitObject.GetComponent<TrapScript>().DisableTrap();
                        Debug.Log("Disabled Trap");
                    }
                    Debug.Log("Did Hit Trap");
                }
                else if (hitObject.CompareTag("PickUp"))
                {
                    Debug.Log("Did Hit Pickup");
                    holding = true;
                    hitObject.SetActive(false);
                    heldObject = hitObject;
                }
                else
                {
                    if (holding)
                    {
                        holding = false;
                        heldObject.transform.position = player.position + player.rotation * Vector3.forward * 2f;
                        heldObject.SetActive(true);
                        heldObject = null;
                    }
                }
            }
            else
            {
                if (holding)
                {
                    holding = false;
                    heldObject.transform.position = player.position + player.rotation * Vector3.forward * 2f;
                    heldObject.SetActive(true);
                    heldObject = null;
                }
                Debug.Log("Did not Hit");
            }      
        }
    }
}
