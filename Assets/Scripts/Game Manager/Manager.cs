using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static CanvasGroup cg;
    public static GameObject playerObject;
    public static GameObject playerTalkingTarget;
    public List<GameObject> coupleList;
    public List<Transform> coupleSpawnPoints;
    public List<string> possibleInterestsList;
    public static string interestSaveLocation = AppDomain.CurrentDomain.DynamicDirectory + "interestList.txt";

    public float timeLimit; // in seconds
    public float displayTime; // used to display a countdown;

    private void Awake()
    {
        //FindCanvas();
        possibleInterestsList = new List<string>();
        LoadInterests(); // Check file for possible interests
        timeLimit = 180;
    }


    // Start is called before the first frame update
    void Start()
    {
        GetAllLovers();
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

    private void GetAllLovers()
    {
        coupleList = new List<GameObject>();
        GameObject[] temp = FindObjectsOfType<GameObject>();
        for(int i = 0; i < temp.Length; i++)
        {
            if (temp[i].tag == "Lover")
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
            // play not broken hearts
        }
        else
        {
            // Bad Match
            playerObject.GetComponent<Controls>().talkingTarget.GetComponent<Lovers>().walkOff = true;
            playerObject.GetComponent<Controls>().followingTarget.GetComponent<Lovers>().walkOff = false;
            // Play Broken hearts
        }
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

    private void LoadInterests()
    {
        try
        {
            string[] possibleInterests = System.IO.File.ReadAllLines(interestSaveLocation);

            for(int i = 0; i < possibleInterests.Length; i++)
            {
                possibleInterestsList.Add(possibleInterests[i]);
            }
            
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
    }

    public static void TurnOnUI(int Case)
    {
        Button[] buttons = cg.GetComponents<Button>();
        // 1 is no follower, 2 is yes follower
        cg.alpha = 1f;
        Controls.canMove = false;
        switch(Case)
        {
            case 1:

                for(int i = 0; i < buttons.Length; i++)
                {
                   if(buttons[i].GetComponent<Text>().text != "Walk Away")
                    {
                        buttons[i].GetComponent<Text>().text = "Follow";
                    }
                }
                break;
            case 2:
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i].GetComponent<Text>().text != "Walk Away")
                    {
                        buttons[i].GetComponent<Text>().text = "Match";
                    }
                }
                break;
        }

    }

    public static void Choose(string choice)
    {
        switch(choice)
        {
            case "Match": // Match Lovers
                {
                    CheckMatch();

                    break;
                }
            case "Walk Away": // Walk Away, don't match Lovers
                {
                    //Intentionally left blank :(
                    break;
                }
            case "Follow":
                {
                    playerObject.GetComponent<Controls>().followingTarget = playerObject.GetComponent<Controls>().talkingTarget;
                    break;
                }

        }
        Controls.canMove = true;
        TurnOffUI();
    }
}
