using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HudController : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject CreditsPanel;
    [SerializeField] GameObject Resolution;

    Slider masterVolumeSlider;
    Slider musicVolumeSlider;
    Slider sfxVolumeSlider;


    private void Awake()
    {
        masterVolumeSlider = OptionsPanel.transform.GetChild(0).GetComponent<Slider>();
        musicVolumeSlider = OptionsPanel.transform.GetChild(1).GetComponent<Slider>();
        sfxVolumeSlider = OptionsPanel.transform.GetChild(2).GetComponent<Slider>();

        OptionsPanel.SetActive(false);
        if (GameManager.instance.ActiveScene == 0)
        {
            MainMenuPanel.SetActive(true);
            CreditsPanel.SetActive(false);
        }
        if (GameManager.instance.ActiveScene != 0)
            PausePanel.SetActive(false);
    }
    private void Start()
    {
        masterVolumeSlider.value = GameManager.instance.MasterVolume();
        musicVolumeSlider.value = GameManager.instance.MusicVolume();
        sfxVolumeSlider.value = GameManager.instance.SfxVolume();
    }
    private void Update()
    {
        if (OptionsPanel.activeInHierarchy)
        {
            GameManager.instance.SetVolume(masterVolumeSlider.value, "Master");
            GameManager.instance.SetVolume(musicVolumeSlider.value, "Music");
            GameManager.instance.SetVolume(sfxVolumeSlider.value, "Sfx");
        }
    }
    public void OptionsToggle()
    {
        if (!OptionsPanel.activeInHierarchy)
            OptionsPanel.SetActive(true);
        else
            OptionsPanel.SetActive(false);
    }

    public void Exit()
    {
        GameManager.instance.QuitGame();
    }
    public void PausePanelToggle()
    {
        if (PausePanel.activeInHierarchy)
            PausePanel.SetActive(false);
        else
            PausePanel.SetActive(true);
    }
    public void creditsToggle()
    {
        if (!CreditsPanel.activeInHierarchy)
            CreditsPanel.SetActive(true);
        else
            CreditsPanel.SetActive(false);
    }
}
