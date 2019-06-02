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
    GameObject location;

    [SerializeField]
    List<GameObject> maxNPC;

    
    [SerializeField]
    List<GameObject> aggressiveNPC;
    [SerializeField]
    List<GameObject> drunkNPC;
    [SerializeField]
    List<GameObject> passiveNPC;
    [SerializeField]
    private int maxNumDrunkNPC, maxNumAggressiveNPC;

    private int maxDrunkNPC, maxAggressiveNPC;

    void Start()
    {
        maxAggressiveNPC = 0;
        maxDrunkNPC = 0;
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
                int randomAggressiveNPC = (int)Random.Range(1f, 5f);
                int randomDrunkNPC = (int)Random.Range(1f, 4f);
                int randomPassiveNPC = (int)Random.Range(1f, 11f);


                if (randomNum == 1 && maxDrunkNPC < maxNumDrunkNPC)
                {

                    GameObject dNPC = Instantiate(drunkNPC[randomDrunkNPC], this.transform.position, this.transform.rotation);
                    dNPC.transform.parent = this.transform;
                    dNPC.GetComponent<DrunkNPC>().fieldOfLocation = this.location;
                    //Instantiate(dNPC, this.transform.position, this.transform.rotation);
                    maxNPC.Add(dNPC);
                    maxDrunkNPC++;
                }
                if (randomNum > 1 && randomNum <= 4 && maxAggressiveNPC < maxNumAggressiveNPC)
                {
                    GameObject aNPC = Instantiate(aggressiveNPC[randomAggressiveNPC], this.transform.position, this.transform.rotation);
                    aNPC.transform.parent = this.transform;
                    aNPC.GetComponent<AggressiveNPC>().fieldOfLocation = this.location;
                    //Instantiate(aNPC, this.transform.position, this.transform.rotation);
                    maxNPC.Add(aNPC);
                    maxAggressiveNPC++;
    
                }
                if (randomNum > 4 && randomNum <= 10)
                {
                    GameObject pNPC = Instantiate(passiveNPC[randomPassiveNPC], this.transform.position, this.transform.rotation);
                    pNPC.transform.parent = this.transform;
                    pNPC.GetComponent<PassiveNPC>().fieldOfLocation = this.location;
                    //Instantiate(pNPC, this.transform.position, this.transform.rotation);
                    maxNPC.Add(pNPC);
                    
                }
                spawnUntil = Time.time + spawnTimeLength;
            }
        }
    }
}
