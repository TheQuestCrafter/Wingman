using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject[] npcList = new GameObject[20];
    public GameObject[] coupleList = new GameObject[10];
    private List<string> possibleInterestsList;
    public static string interestSaveLocation = AppDomain.CurrentDomain.DynamicDirectory + "interestList.txt";

    public float timeLimit; // in seconds
    public float displayTime; // used to display a countdown;

    private void Awake()
    {
        possibleInterestsList = new List<string>();
        loadInterests(); // Check file for possible interests
        timeLimit = 180;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        displayTime = timeLimit - Time.time;
    }

    private void checkMatch()
    {
        // good match = send both a true bool
        // bad match = send one a true and send the other a false
    }

    private void determineLoverCoupleInterest()
    {
        string sharedInterest = possibleInterestsList[UnityEngine.Random.Range(0, possibleInterestsList.Count)];
        possibleInterestsList.Remove(sharedInterest);


        // Spawn two lovers which will be a "couple" and give them the same interest. 
        //coupleList

    }

    private void loadInterests()
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
    private void saveInterests(string[] interests)
    {
        System.IO.File.WriteAllLines(interestSaveLocation, interests);

    }
}
