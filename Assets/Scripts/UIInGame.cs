using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIInGame : MonoBehaviour
{
    public Text Text;
    private int score;

    [SerializeField] private Example input;

    void Start()
    {
        Text = GetComponentInChildren<Text>();
        input = FindObjectOfType<Example>();
    }

    void Update()
    {

        score = input.Input;
        Text.text = "score: " + score;
       

    }
}
