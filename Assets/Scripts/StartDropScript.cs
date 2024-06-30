using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDropScript : MonoBehaviour
{
    public DropScript drop;
    public float dropDelay = 2f;
    public float timeElapsed = -1f;

    void Update()
    {
        if(timeElapsed >=0)
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed >= dropDelay)
            {
                drop.Drop();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Start Drop Hit");

        if (other.gameObject.CompareTag("Grandpa"))
        {
            timeElapsed = 0f;
        }
    }
}
