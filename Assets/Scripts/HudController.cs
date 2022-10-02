using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HudController : MonoBehaviour
{
    [SerializeField] GameObject OptionsPanel;
    Slider masterVolumeSlider;
    Slider musicVolumeSlider;
    Slider sfxVolumeSlider;

    private void Awake()
    {
        masterVolumeSlider = OptionsPanel.transform.GetChild(0).GetComponent<Slider>();
        musicVolumeSlider = OptionsPanel.transform.GetChild(1).GetComponent<Slider>();
        sfxVolumeSlider = OptionsPanel.transform.GetChild(2).GetComponent<Slider>();
        OptionsPanel.SetActive(false);
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

    public void Options()
    {
        OptionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        OptionsPanel.SetActive(false);
    }
    public void credits()
    {

    }
}
