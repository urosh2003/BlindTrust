using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mrmljanjescript : MonoBehaviour
{
    public List<AudioClip> audios;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grandpa"))
        {
            int randomNum = Random.Range(0, audios.Count);
            audioSource.clip = audios[randomNum];
            audioSource.Play();
        }
    }
}
