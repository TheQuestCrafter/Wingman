using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinReset : MonoBehaviour
{

    void Awake()
    {
        Manager.Score = 0;
        Destroy(FindObjectOfType<Manager>().gameObject);
    }

}
