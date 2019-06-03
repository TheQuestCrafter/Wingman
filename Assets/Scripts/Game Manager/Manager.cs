using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static CanvasGroup cg;

    public static GameObject playerObject;
    public static GameObject playerTalkingTarget;
    public List<GameObject> coupleList;
    public List<Transform> coupleSpawnPoints;
    public List<string> possibleInterestsList;
    public List<string> possiblePunsList;
    public static string interestSaveLocation = AppDomain.CurrentDomain.DynamicDirectory + "interestList.txt";
    public static string punsSaveLocation = AppDomain.CurrentDomain.DynamicDirectory + "punsList.txt";
    public static int Score, maxScore;

    [SerializeField]
    private GameObject option1;
    [SerializeField]
    private EventSystem gameEventSystem;

    static Button[] buttons; 

    public float timeLimit; // in seconds
    public float displayTime; // used to display a countdown;

    private void Awake()
    {
        FindCanvas();
        possibleInterestsList = new List<string>();
        LoadFiles(); // Check file for possible interests
        timeLimit = 180;
        buttons = cg.GetComponentsInChildren<Button>();
        cg.alpha = 0;
        maxScore = 5;
        
    }


    // Start is called before the first frame update
    void Start()
    {
        FindAll();
        gameEventSystem = FindObjectOfType<EventSystem>();
        gameEventSystem.SetSelectedGameObject(option1);
    }

    // Update is called once per frame
    void Update()
    {
        displayTime = timeLimit - Time.time;
    }

    private void FindCanvas()
    {
        CanvasGroup[] temp = FindObjectsOfType<CanvasGroup>();
        cg = temp[0];
    }

    private void FindAll()
    {
        coupleList = new List<GameObject>();
        GameObject[] temp = FindObjectsOfType<GameObject>();
        for(int i = 0; i < temp.Length; i++)
        {
            if (temp[i].tag == "Player")
            {
                playerObject = temp[i];
            }
            else if (temp[i].tag == "Lover")
            {
                coupleList.Add(temp[i]);
            }
            else if(temp[i].tag == "LSpawnLocation")
            {
                coupleSpawnPoints.Add(temp[i].transform);
            }

        }
        DetermineLoverCoupleInterest();

        for(int i = 0; i < coupleList.Count; i++)
        {
            int tempIndex = UnityEngine.Random.Range(0, coupleList.Count);
            GameObject tempLover = coupleList[tempIndex];
            coupleList[tempIndex] = coupleList[i];
            coupleList[i] = tempLover;
        }

        for(int i = 0; i < coupleList.Count; i++)
        {
            coupleList[i].GetComponent<Lovers>().player = Manager.playerObject;
            coupleList[i].transform.position = coupleSpawnPoints[i].position;
        }
        
    }
    

    public static void CheckMatch()
    {
        // good match = send both a true bool
        // bad match = send one a true and send the other a false

        if(playerObject.GetComponent<Controls>().talkingTarget.GetComponent<Lovers>().interest ==
            playerObject.GetComponent<Controls>().followingTarget.GetComponent<Lovers>().interest)
        {
            // Good Match
            playerObject.GetComponent<Controls>().talkingTarget.GetComponent<Lovers>().walkOff = true;
            playerObject.GetComponent<Controls>().followingTarget.GetComponent<Lovers>().walkOff = true;

            Score++;
            EndGame();
            // play not broken hearts
        }
        else
        {
            // Bad Match
            playerObject.GetComponent<Controls>().talkingTarget.GetComponent<Lovers>().walkOff = true;
            playerObject.GetComponent<Controls>().followingTarget.GetComponent<Lovers>().walkOff = false;
            EndGame();
            // Play Broken hearts
        }

        playerObject.GetComponent<Controls>().talkingTarget.GetComponent<Lovers>().currentState = Lovers.LoverStates.WalkAway;
        playerObject.GetComponent<Controls>().followingTarget.GetComponent<Lovers>().currentState = Lovers.LoverStates.WalkAway;

        playerObject.GetComponent<Controls>().talkingTarget = null;
        playerObject.GetComponent<Controls>().followingTarget = null;
    }

    private void DetermineLoverCoupleInterest()
    {
        for(int i = 0; i < coupleList.Count; i++)
        {
            string sharedInterest = possibleInterestsList[UnityEngine.Random.Range(0, possibleInterestsList.Count)];
            possibleInterestsList.Remove(sharedInterest);
            if (i%2 == 0)
            {
                coupleList[i].GetComponent<Lovers>().interest = sharedInterest;
                coupleList[i+1].GetComponent<Lovers>().interest = sharedInterest;
            }
        }

        

    }

    private void LoadFiles()
    {
        try
        {
            string[] possibleInterests = System.IO.File.ReadAllLines(interestSaveLocation);
            //string[] possiblePuns = System.IO.File.ReadAllLines(punsSaveLocation);

            for (int i = 0; i < possibleInterests.Length; i++)
            {
                possibleInterestsList.Add(possibleInterests[i]);
            }

            //for (int i = 0; i < possiblePuns.Length; i++)
            //{
            //    possiblePunsList.Add(possibleInterests[i]);
            //}

        }
        catch(Exception e)
        {
            //return new string[1];
        }
    }

    // Only used for testing
    private void SaveInterests(string[] interests)
    {
        System.IO.File.WriteAllLines(interestSaveLocation, interests);

    }

    public static void TurnOffUI()
    {
        cg.alpha = 0f;
        playerObject.GetComponent<Controls>().talkingTarget = null;
        playerTalkingTarget = null;

        playerObject.GetComponent<Controls>().nearbyTargets.Clear();
    }

    public static void TurnOnUI(int Case)
    {
        
        // 1 is no follower, 2 is yes follower
        cg.alpha = 1f;
        Controls.canMove = false;

        

        ChangeUIText();
        

        switch(Case)
        {
            case 1:

                for(int i = 0; i < buttons.Length; i++)
                {
                   if(buttons[i].GetComponentInChildren<Text>().text != "Walk Away")
                   {
                        buttons[i].GetComponentInChildren<Text>().text = "Follow";
                   }
                }
                break;
            case 2:
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i].GetComponentInChildren<Text>().text != "Walk Away")
                    {
                        buttons[i].GetComponentInChildren<Text>().text = "Match";
                    }
                }
                break;
        }

    }

    private static void ChangeUIText()
    {
        Text[] temp = cg.GetComponentsInChildren<Text>();
        string Dialogue = "";
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].name == "DialogueText")
            {
                
                char[] chars = temp[i].text.ToCharArray();
                for(int j = 0; j < chars.Length; j++)
                {
                    Dialogue += chars[j];
                    if(chars[j] == ':')
                    {
                        Dialogue += " ";
                        j = chars.Length + 1;
                        temp[i].text = Dialogue + playerTalkingTarget.GetComponent<Lovers>().interest;
                    }
                }
            }
        }        
    }

    public void Button1()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].GetComponentInChildren<Text>().text == "Follow")
            {
                playerObject.GetComponent<Controls>().followingTarget = playerObject.GetComponent<Controls>().talkingTarget;
                playerObject.GetComponent<Controls>().talkingTarget = null;
                break;

            }
            else if (buttons[i].GetComponentInChildren<Text>().text == "Match")
            {
                CheckMatch();
                break;
            }
        }

        Controls.canMove = true;
        TurnOffUI();
    }

    public void WalkAway()
    {
        Controls.canMove = true;
        TurnOffUI();
    }

    static void EndGame()
    {
        if(Score == maxScore)
        {
            //YOU WIN
        }
        else
        {
            //YOU LOSE
        }
    }
    
}
