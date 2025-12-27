using cakeslice;
using System.Collections;
using System.Linq;
using UnityEngine;

public class HighlightScript : MonoBehaviour
{
    public Outline outline;
    public GameObject player;
    public GameObject interactButton;
    public bool buttonClose = false;
    public bool disabled=false;

    public float distance = 2f;
    // Start is called before the first frame update

    private void Start()
    {
        interactButton = Resources.FindObjectsOfTypeAll<GameObject>()
        .FirstOrDefault(obj => obj.name == "InteractButton");
        //interactButton.SetActive(false);
    }

    private void Update()
    {
        if (!disabled)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) < distance)
            {
                Debug.Log("abc");
                if (!gameObject.CompareTag("Trap") || (player.GetComponent<PlayerRaycastScript>().holding && gameObject.CompareTag("Trap")))
                {
                    outline.color = 1;
                    //interactButton.SetActive(true);
                    buttonClose = true;
                }
            }
            else
            {
                if (buttonClose == true)
                {
                    //interactButton.SetActive(false);
                    buttonClose = false;
                }
                outline.color = 0;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //outline.color = 1;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //outline.color = 0;
        }
    }
    public void Disable()
    {
        disabled = true;
        outline.eraseRenderer = true;
        if (buttonClose == true)
        {
            StartCoroutine(WaitForFeedback());
        }
    }
    public void Enable()
    {
        outline.eraseRenderer = false;
        disabled = false;
    }

    IEnumerator WaitForFeedback()
    {
        yield return new WaitForSeconds(0.2f);
        //interactButton.SetActive(false);
        buttonClose = false;
    }
}
