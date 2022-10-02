using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIInGame : MonoBehaviour
{
    public Text Text;
    private int score;
    private AudioManger audioManager;

    [SerializeField] private Example input;

    void Start()
    {
        Text = GetComponentInChildren<Text>();
        input = FindObjectOfType<Example>();
        audioManager= FindObjectOfType<AudioManger>();
        audioManager.SelectAudio(1, 1);
    }

    void Update()
    {

        score = input.Input;
        Text.text = "score: " + score;
       

    }
}
