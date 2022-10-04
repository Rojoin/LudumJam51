using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    HudController hudController;
    [SerializeField] float masterVolume;
   
    public int ActiveScene;
    [SerializeField] TMP_Dropdown dropdown;
    private bool FullScreen = false;


    public int totalAngryClients;
    public int totalHappyClients;
    [SerializeField] int loseCondition;
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
        Screen.fullScreen = FullScreen;
        ActiveScene = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetActiveScene().name != "mainGame")
        {
        hudController = FindObjectOfType<HudController>().GetComponent<HudController>();
            
        }
        dropdown =  Resources.FindObjectsOfTypeAll<TMP_Dropdown>().FirstOrDefault();
        dropdown.onValueChanged.AddListener(delegate { setScreenRes(); });
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseToggle();

       
        if(totalAngryClients >= loseCondition && SceneManager.GetActiveScene().name == "mainGame")
        {
            totalAngryClients = 0;
            totalHappyClients = 0;
            SceneManager.LoadScene(0);
        }
    }

    public void SetVolume(float volume, string volumeType)
    {
        switch (volumeType)
        {
            case "Master":
                masterVolume = volume ;
                break;
            case "Music":
                musicVolume = volume ;
                break;
            case "Sfx":
                sfxVolume = volume ;
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


    //----------------------------- Screen Resolution And Full Screen -----------------------------
    public void ToggleFullScreen()
    {
        if (!FullScreen)
        {
            FullScreen = true;
            Debug.Log("Full Screen ON");
        }
        else if (FullScreen)
        {
            FullScreen = false;
            Debug.Log("Full Screen OFF");
        }
        Screen.fullScreen = FullScreen;
    }
    public void setScreenRes() 
    {
        //Debug.Log(dropdown.value);
        //Debug.Log(dropdown.options[dropdown.value].text);
        int dropValue = dropdown.value;
        switch (dropValue)
        {
            case 0:
                Screen.SetResolution(1440,1080, FullScreen);
                Debug.Log("Resolution Set to : 1440 x 1080");
                break;
            case 1:
                Screen.SetResolution(1400, 1050, FullScreen);
                Debug.Log("Resolution Set to : 1400 x 1050");
                break;
            case 2:
                Screen.SetResolution(1280, 960, FullScreen);
                Debug.Log("Resolution Set to : 1280 x 960");
                break;
            case 3:
                Screen.SetResolution(1024, 768, FullScreen);
                Debug.Log("Resolution Set to : 1024 x 768");
                break;
            case 4:
                Screen.SetResolution(800, 600, FullScreen);
                Debug.Log("Resolution Set to : 800 x 600");
                break;
            default:
                Debug.Log("Error on Resolution Set");
                break;
        }
    }
    //----------------------------- Screen Resolution And Full Screen end -----------------------------
}
