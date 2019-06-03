using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorekeeper : MonoBehaviour
{

    private Text text;
    private string score;

    private void Awake()
    {
        text = GetComponent<Text>();
        score = Manager.Score.ToString();
        text.text = $"Score: {score}/5";
    }

}
