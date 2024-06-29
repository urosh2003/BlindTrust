using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementScript : MonoBehaviour
{
    public Camera camera;
    public NavMeshAgent player;
    public GameObject targetDest;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if(Physics.Raycast(ray, out hitPoint))
            {
                targetDest.transform.position = hitPoint.point;
                player.SetDestination(hitPoint.point);
            }
        }        
    }

    public void DeadPlayer()
    {
        Debug.Log("Player died");

    }
}
