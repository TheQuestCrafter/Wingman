using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private Text text;
    private string score;

    private void Awake()
    {
        text = GetComponent<Text>();
        
    }

    private void Update()
    {
        score = Manager.Score.ToString();
        text.text = $"{score}/5";
    }
}
