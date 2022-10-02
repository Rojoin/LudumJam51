using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManger : MonoBehaviour
{
    [SerializeField] private AudioClip[] audios; 

    private AudioSource controlAudio;
    private void Awake()
    {
        controlAudio = GetComponent<AudioSource>();
    }

    public void SelectAudio (int indice, float volumen)
    {
        controlAudio.PlayOneShot(audios[indice], volumen);
    }
}
