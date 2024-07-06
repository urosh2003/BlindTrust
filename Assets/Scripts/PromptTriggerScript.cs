using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PromptTriggerScript : MonoBehaviour
{
    public GameObject prompPanel;
    public string promptText;
    public string promptTitle;
    public Sprite promptSprite;
    public AudioClip promptSound;
    public AudioSource audioSource;
    public float displayForSeconds;
    private bool displayed = false;
    private float timeElapsed = 0f;
    public bool triggerOnGrandpa = true;
    public bool triggerOnPlayer = false;


    private void Update()
    {
        if (displayed && displayForSeconds!=0 && timeElapsed<displayForSeconds)
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed >= displayForSeconds)
            {
                prompPanel.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grandpa") && triggerOnGrandpa && !displayed)
        {
            displayed = true;
            prompPanel.SetActive(true);

            prompPanel.transform.Find("PromptSprite").gameObject.GetComponent<Image>().sprite = promptSprite;
            prompPanel.transform.Find("PromptText").gameObject.GetComponent<TextMeshProUGUI>().text = promptText;
            prompPanel.transform.Find("PromptTitle").gameObject.GetComponent<TextMeshProUGUI>().text = promptTitle;

            audioSource.clip = promptSound;
            audioSource.Play();

        } else if (other.gameObject.CompareTag("Player") && triggerOnPlayer && !displayed)
        {
            displayed = true;
            prompPanel.SetActive(true);

            prompPanel.transform.Find("PromptSprite").gameObject.GetComponent<Image>().sprite = promptSprite;
            prompPanel.transform.Find("PromptText").gameObject.GetComponent<TextMeshProUGUI>().text = promptText;
            prompPanel.transform.Find("PromptTitle").gameObject.GetComponent<TextMeshProUGUI>().text = promptTitle;

            audioSource.clip = promptSound;
            audioSource.Play();

        }
    }
}
