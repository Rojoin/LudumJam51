using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    HudController hudController;
    [SerializeField] float masterVolume;

    public int ActiveScene;
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
        if (instance != null && instance != this)
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
        ActiveScene = SceneManager.GetActiveScene().buildIndex;
        hudController = FindObjectOfType<HudController>().GetComponent<HudController>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseToggle();
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

    void PauseToggle()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            hudController.PausePanelToggle();
        }
        else
        {
            Time.timeScale = 1;
            hudController.PausePanelToggle();
        }
    }
}
