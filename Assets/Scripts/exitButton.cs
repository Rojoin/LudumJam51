
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitButton : MonoBehaviour
{
    private AudioManager audio;
    void Start()
    {
        audio = FindObjectOfType<AudioManager>();
    }
   public void exitApp()
    {
        Application.Quit();
    }
   public void playGame()
   {
       SceneManager.LoadScene(1);
 audio.StopMusic();
   }
}
