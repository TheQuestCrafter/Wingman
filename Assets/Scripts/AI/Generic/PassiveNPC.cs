using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveNPC : GenericNPCMovement
{

    // Update is called once per frame
    void Update()
    {
        base.Movement(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            speed = 0;
        }

        waitUntil = Time.time + waitTimeLength;
    }
}
