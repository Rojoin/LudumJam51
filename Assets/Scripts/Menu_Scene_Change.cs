using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Scene_Change : MonoBehaviour
{
    //[SerializeField] private string newSceneName;
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
