using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwaySingleScript : MonoBehaviour
{
    public GameObject Destination;
    public UnityEngine.AI.NavMeshAgent Entinty;

    public void BarkedAt()
    {
        Entinty.SetDestination(Destination.transform.position);
    }
}
