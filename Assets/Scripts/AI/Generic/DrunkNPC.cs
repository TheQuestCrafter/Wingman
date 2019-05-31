using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkNPC : GenericNPCMovement
{
    [SerializeField]
    LayerMask playerMask;

    [SerializeField]
    GameObject player;

    CircleCollider2D cc2d;
    // Start is called before the first frame update
    void Start()
    {
        cc2d = GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(this.transform.position, cc2d.radius, playerMask))
        {
            this.destinationLocation = player.transform.position;
            speed = 2;
            Movement();
        }
        else
        {
            base.Movement();
        }
    }

    public override void Movement()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, destinationLocation, speed * Time.deltaTime);
    }
}
