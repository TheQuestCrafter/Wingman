using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveNPC : GenericNPCMovement
{

    [SerializeField]
    private Collider2D thisCollider2D;
    [SerializeField]
    List<Collider2D> savedCollider2Ds;

    // Update is called once per frame
    void Update()
    {
        base.Movement();

        savedCollider2Ds = new List<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
  
        if (collision.collider.CompareTag("Obstacle") || collision.collider.CompareTag("Player"))
        {
            speed = 0;
            if(Time.time > waitUntil)
            {
                base.Movement();
            }

            waitUntil = Time.time + waitTimeLength;
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
            waitUntil = Time.time + waitTimeLength;
        }
       
    }
}
