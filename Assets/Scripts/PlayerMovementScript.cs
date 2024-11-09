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
    private bool held;

    public Animator animator;
    public GameObject deathScreen;
    public PlayerInput input;

    public GameObject mesh;
    public float moveSpeed = 100;
    public bool moving = false;

    private void Start()
    {
        camera.orthographicSize /= camera.aspect;
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (alive)
        {

        if (moving)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            if (playerCaught)
            {
                timeElapsed += Time.deltaTime;
                if (timeElapsed > clickingDuration)
                {
                    DeadPlayer();
                }
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
                {
                        clicks += 1;
                    if (clicks >= clicksRequired)
                    {
                        clickPanel.SetActive(false);
                        gameObject.transform.Find("Ker Animacija").gameObject.SetActive(true);

                        playerCaught = false;
                        //gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                        input.ActivateInput();
                        clicks = 0;
                        if (held)
                        {
                            gameObject.GetComponent<PlayerRaycastScript>().heldObject.SetActive(true);

                        }
                        held = false;
                    }
                }
            }
        }
    }
    public void DeadPlayer()
    {
        Time.timeScale = 0;
        alive = false;
        clickPanel.GetComponent<ButtonMashAnimationScript>().Func_StopUIAnim();
        clickPanel.SetActive(false);

        //gameObject.GetComponent<NavMeshAgent>().isStopped = true;

        Debug.Log("Player died");

        deathScreen.SetActive(true);

    }

    public void Respawn()
    {

        clickPanel.SetActive(false);
        playerCaught = false;
        alive = true;
        //gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }

    public void CaughtInATrap()
    {
        moving = false;
        animator.SetBool("Iswalking", false);

        //gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        gameObject.transform.Find("Ker Animacija").gameObject.SetActive(false);
        if(gameObject.GetComponent<PlayerRaycastScript>().holding)
        {
            gameObject.GetComponent<PlayerRaycastScript>().heldObject.SetActive(false);
            held = true;
        }
        clickPanel.SetActive(true);
        clickPanel.GetComponent<ButtonMashAnimationScript>().Func_PlayUIAnim();
        playerCaught = true;
        input.DeactivateInput();
        timeElapsed = 0f;
        clicks = 0;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            animator.SetBool("Iswalking", false);
            moving = false;
            return;
        }
        animator.SetBool("Iswalking", true);
        //Define the speed at which the object moves.

        var inputVector = context.ReadValue<Vector2>();
        var movementVector = new Vector3(inputVector.x, 0, inputVector.y);
        Debug.Log(movementVector);
        //Get the value of the Horizontal input axis.


        Debug.Log(inputVector);

        transform.rotation = Quaternion.LookRotation(movementVector) * Quaternion.Euler(0,45,0);
        //Move the object to XYZ coordinates defined as horizontalInput, 0, and verticalInput respectively.
        moving = true;

    }
}
