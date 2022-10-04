using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager audioManager;

    [SerializeField] private AudioClip[] audios;

    [SerializeField] private AudioClip[] musics;

    [SerializeField] private AudioSource controlAudio;


    private void Start()
    {
        controlAudio = GetComponent<AudioSource>();
        audioManager.PlayMusic(GameManager.instance.ActiveScene, GameManager.instance.MusicVolume());
        
    }
    private void Awake()
    {

        if (audioManager != null && audioManager != this)
        {
            audioManager.PlayMusic(GameManager.instance.ActiveScene, GameManager.instance.MusicVolume());
            Destroy(gameObject);
        }
        else
        {
            audioManager = this;
            DontDestroyOnLoad(this);
        }
    }
    public void PlayMusic(int indice, float volumen)
    {

        controlAudio.PlayOneShot(musics[indice],volumen );
        Debug.Log(volumen);
        controlAudio.loop = true;
    }

    public void StopMusic ()
    {
        controlAudio.Stop();
        controlAudio.loop = false;
    }
    public void SelectAudio(int indice, float volumen)
    {
        controlAudio.PlayOneShot(audios[indice], volumen);
    }
}
