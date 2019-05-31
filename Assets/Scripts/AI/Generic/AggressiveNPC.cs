using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveNPC : GenericNPCMovement
{ 

    // Update is called once per frame
    void Update()
    {
        base.Movement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
  
        if (collision.collider.CompareTag("Obstacle") || collision.collider.CompareTag("Player"))
        {
            speed = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        speed = changeSpeed;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle") || collision.collider.CompareTag("Player"))
        {
            speed = 0;
        }
    }
}
