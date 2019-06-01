using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveNPC : GenericNPCMovement
{
    [SerializeField]
    private Collider2D thisCollider2D;
    [SerializeField]
    List<Collider2D> savedCollider2Ds;

    private void Start()
    {
        savedCollider2Ds = new List<Collider2D>();
    }

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
            waitUntil = Time.time + waitTimeLength;
        }

        if(collision.collider.CompareTag("PassiveNPC") || collision.collider.CompareTag("DrunkNPC"))
        {
            savedCollider2Ds.Add(collision.collider);
            Physics2D.IgnoreCollision(collision.collider, thisCollider2D, true);

        }


       
    }

}
