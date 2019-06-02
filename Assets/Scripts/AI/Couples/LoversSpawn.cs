using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoversSpawn : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Lovers;

    void Awake()
    {
        
    }

    void Spawn()
    {
        foreach(GameObject l in Lovers)
        {
            Instantiate(l, this.transform.position, this.transform.rotation);
        }
    }
}
