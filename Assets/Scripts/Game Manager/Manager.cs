using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject[] npcList = new GameObject[20];
    public GameObject[] coupleList = new GameObject[10];
    public string[] possibleInterests;
    public static string interestSaveLocation = AppDomain.CurrentDomain.DynamicDirectory + "interestList.txt";

    private void Awake()
    {
        possibleInterests = readInterests();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string[] readInterests()
    {
        try
        {
            string[] interests = System.IO.File.ReadAllLines(interestSaveLocation);

            return interests;
        }
        catch(Exception e)
        {
            return new string[1];
        }
    }

    // Only used for testing
    private void saveInterests(string[] interests)
    {
        System.IO.File.WriteAllLines(interestSaveLocation, interests);

    }
}
