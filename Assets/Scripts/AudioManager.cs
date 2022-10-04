using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    [SerializeField] private AudioClip[] audios;
    [SerializeField] private AudioClip[] musics;
    [SerializeField] private AudioSource controlAudio;
    [SerializeField] AudioMixer mixer;


    const string MASTER_VOLUME = "MasterVolume";
    const string MUSIC_VOLUME = "MusicVolume";
    const string SFX_VOLUME = "SfxVolume";
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
    private void Start()
    {
        controlAudio = GetComponent<AudioSource>();
        audioManager.PlayMusic(GameManager.instance.ActiveScene, GameManager.instance.MusicVolume());
    }
    public void PlayMusic(int indice, float volumen)
    {
        controlAudio.PlayOneShot(musics[indice], volumen);
        controlAudio.loop = true;
    }
    public void SetMasterVolume(float value)
    {
        mixer.SetFloat(MASTER_VOLUME, Mathf.Log10(value) * 20);
    }
    public void SetMusicVolume(float value)
    {
        mixer.SetFloat(MUSIC_VOLUME, Mathf.Log10(value) * 20);
    }
    void SetSfxVolume(float value)
    {
        mixer.SetFloat(SFX_VOLUME, Mathf.Log10(value) * 20);
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
