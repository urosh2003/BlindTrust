using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerRaycastScript : MonoBehaviour
{
    public float castRadius;
    public bool holding;
    public Camera camera;
    public GameObject heldObject;
    public Transform player;
    public float hitRange = 10000;
    public float barkCooldown = 10f;
    public float timeElapsed = -1f;
    public float areaAroundPlayerRadius = 5f;

    public float interactCooldown = 1f;
    public float interactTimeElapsed = -1f;
    public List<AudioClip> barks;
    private AudioSource audioSource;

    [SerializeField]
    private GameObject CooldownIcon;

    // Start is called before the first frame update
    void Start()
    {
        CooldownIcon.GetComponent<SpellCooldown>().cooldownTime = barkCooldown;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (holding) {
            heldObject.transform.position = player.position + Vector3.up * 3;
            heldObject.transform.localScale = Vector3.one * 2;
        }

        if(timeElapsed >= 0)
        {
            timeElapsed += Time.deltaTime;
        }
        if(timeElapsed > barkCooldown)
        {
            timeElapsed = -1;
        }
        if(interactTimeElapsed >= 0)
        {
            interactTimeElapsed += Time.deltaTime;
        }
        if(interactTimeElapsed > interactCooldown)
        {
            interactTimeElapsed = -1;
        }
    }

    public void Bark(InputAction.CallbackContext context)
    {
        if (context.performed && timeElapsed < 0f)
        {
            CooldownIcon.GetComponent<SpellCooldown>().UseSpell();
            Debug.Log("Woof");
            int randomNum = Random.Range(0, barks.Count);
            audioSource.clip = barks[randomNum];
            audioSource.Play();


            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer

            if (Physics.SphereCast(transform.position + new Vector3(0,6,0), castRadius, transform.TransformDirection(Vector3.forward), out hit, hitRange))
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
                Collider[] colliders = Physics.OverlapSphere(transform.position + new Vector3(0,2,0), areaAroundPlayerRadius);
                if (colliders.Length > 0)
                {
                    foreach (var other in colliders)
                    {
                        Debug.Log(GetComponent<Collider>().name);

                        if (other.gameObject.CompareTag("Grandpa"))
                        {
                            other.gameObject.GetComponent<GrandpaMovementScript>().Stop();

                        }
                        else if (other.gameObject.CompareTag("RunAway"))
                        {
                            other.gameObject.GetComponent<RunAwayGroup>().BarkedAt();
                        }


                    }
                }
            }

            timeElapsed = 0;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && interactTimeElapsed < 0)
        {
            Debug.Log("Interact");


            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.SphereCast(transform.position + new Vector3(0,6,0), castRadius, transform.TransformDirection(Vector3.forward), out hit, hitRange))
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
                        heldObject.SetActive(false);
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
                    heldObject = hitObject;
                }
            }
            else
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position + new Vector3(0,2,0), areaAroundPlayerRadius);
                if (colliders.Length > 0)
                {
                    foreach(var other in colliders)
                    {
                        if (!holding)
                        {
                            if (other.gameObject.CompareTag("PickUp"))
                            {
                                Debug.Log("Did Hit Pickup");
                                holding = true;
                                heldObject = other.gameObject;
                                break;
                            }
                            else if (other.gameObject.CompareTag("TrafficLight"))
                            {
                                other.gameObject.GetComponent<TrafficLightScript>().StopCars();
                            }
                        }
                        else
                        {
                            if (other.gameObject.CompareTag("Trap"))
                            {
                                holding = false;
                                heldObject.SetActive(false);
                                heldObject = null;
                                other.gameObject.GetComponent<TrapScript>().DisableTrap();
                                Debug.Log("Disabled Trap");
                                break;
                            }
                        }
                    }
                }
            }   
            interactTimeElapsed = 0;   
        }
    }

}
