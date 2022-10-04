using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI angryText;
    private int score;
    private int angryOnes;

    void Update()
    {
        score = GameManager.instance.totalHappyClients;
        angryOnes = GameManager.instance.totalAngryClients;
        scoreText.text = "Happy: " + score.ToString();
        angryText.text = "Angry: " + angryOnes.ToString();
    }
}
