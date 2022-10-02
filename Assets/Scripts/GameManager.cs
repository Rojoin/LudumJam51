using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] float masterVolume;
    public float MasterVolume()
    {
        return masterVolume;
    }

    [SerializeField] float musicVolume;
    public float MusicVolume()
    {
        return musicVolume;
    } 
    [SerializeField] float sfxVolume;
    public float SfxVolume()
    {
        return sfxVolume;
    }
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            FindObjectOfType<HudController>().Options();
    }

    public void SetVolume(float volume, string volumeType)
    {
        switch (volumeType)
        {
            case "Master":
                masterVolume = volume;
                break;
            case "Music":
                musicVolume = volume;
                break;
            case "Sfx":
                sfxVolume = volume;
                break;
        }
    }
    public void newScene(string newSceneName)
    {
        SceneManager.LoadScene(newSceneName);
    }
    public void QuitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
