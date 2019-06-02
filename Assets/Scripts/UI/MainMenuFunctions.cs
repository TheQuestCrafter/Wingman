using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{

    [SerializeField]
    private int sceneNum;

    public void StartGame()
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void ExitGame()
    {
        System.Environment.Exit(0);
    }

}
