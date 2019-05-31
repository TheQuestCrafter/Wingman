using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField]
    float spawnTimeLength;
    [SerializeField]
    float spawnUntil;
    [SerializeField]
    int numberToSpawn;
    [SerializeField]
    int numberOfSpawnLeft;

    List<GameObject> maxNPC;

    [SerializeField]
    GameObject drunkNPC, aggressiveNPC, passiveNPC, location;

    void Start()
    {
        drunkNPC.GetComponent<GenericNPCMovement>().fieldOfLocation = location;
        aggressiveNPC.GetComponent<GenericNPCMovement>().fieldOfLocation = location;
        passiveNPC.GetComponent<GenericNPCMovement>().fieldOfLocation = location;

        maxNPC = new List<GameObject>();
    }

    void Update()
    {
        if(maxNPC.Count < numberToSpawn)
        {
            Spawn();
        }

        
    }

    void Spawn()
    {
        for(int i = 0; i < numberToSpawn; i++)
        {
            if(Time.time > spawnUntil)
            {
                int randomNum = (int)Random.Range(1f, 10f);
                if(randomNum == 1)
                {
                    Instantiate(drunkNPC, this.transform.position, this.transform.rotation);
                    maxNPC.Add(drunkNPC);
                }
                if (randomNum > 1 && randomNum <= 4)
                {
                    Instantiate(aggressiveNPC, this.transform.position, this.transform.rotation);
                    maxNPC.Add(aggressiveNPC);
                }
                if (randomNum > 4 && randomNum <= 10)
                {
                    Instantiate(passiveNPC, this.transform.position, this.transform.rotation);
                    maxNPC.Add(passiveNPC);
                }
                spawnUntil = Time.time + spawnTimeLength;
                numberOfSpawnLeft++;
            }
        }
    }
}
