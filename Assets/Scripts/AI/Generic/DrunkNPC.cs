using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkNPC : GenericNPCMovement
{
    [SerializeField]
    LayerMask playerMask;

    [SerializeField]
    GameObject player;

    private Vector2 playerLocation;
    [SerializeField]
    private Vector2 distanceFromPlayer;

    [SerializeField]
    Collider2D[] playerCheck;

    [SerializeField]
    private Collider2D thisCollider2D;
    [SerializeField]
    List<Collider2D> savedCollider2Ds;

    [SerializeField]
    float radius;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        savedCollider2Ds = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerCheck = Physics2D.OverlapCircleAll(this.transform.position, radius, playerMask);
        if(playerCheck.Length != 0)
        {
            Debug.Log("Chase");
            this.destinationLocation = player.transform.position;
            speed = 1.4f;
            Movement();
        }
        else
        {
            base.Movement();
        }
    }

    public override void Movement()
    {
        playerLocation = player.GetComponent<Controls>().direction;
        if (playerLocation.x == 0 && playerLocation.y == 1) // Up
        {
            distanceFromPlayer.x = 0;
            
            this.transform.position = Vector2.MoveTowards(this.transform.position, DistanceFromPlayer(distanceFromPlayer, player.transform.position), speed * Time.deltaTime);
        }
        else if (playerLocation.x == 0 && playerLocation.y == -1) // Down
        {
            distanceFromPlayer.x = 0;
            distanceFromPlayer.y = -distanceFromPlayer.y;
            this.transform.position = Vector2.MoveTowards(this.transform.position, DistanceFromPlayer(distanceFromPlayer, player.transform.position), speed * Time.deltaTime);

        }
        else if (playerLocation.x == -1 && playerLocation.y == 0) //Left
        {
           
            distanceFromPlayer.y = 0;
            this.transform.position = Vector2.MoveTowards(this.transform.position, DistanceFromPlayer(distanceFromPlayer, player.transform.position), speed * Time.deltaTime);
        }
        else if (playerLocation.x == 1 && playerLocation.y == 0) // Right 
        {
            distanceFromPlayer.x = -distanceFromPlayer.x;
            distanceFromPlayer.y = 0;
            this.transform.position = Vector2.MoveTowards(this.transform.position, DistanceFromPlayer(distanceFromPlayer, player.transform.position), speed * Time.deltaTime);
        }
        else if (playerLocation.x == 1 && playerLocation.y == 1) // Up Right
        {
            distanceFromPlayer.x = 1;
            distanceFromPlayer.y = 1;
            this.transform.position = Vector2.MoveTowards(this.transform.position, DistanceFromPlayer(distanceFromPlayer, player.transform.position), speed * Time.deltaTime);
        }
        else if (playerLocation.x == -1 && playerLocation.y == 1) // Up Left
        {
            distanceFromPlayer.x = -distanceFromPlayer.x;
            distanceFromPlayer.y = 1;
            this.transform.position = Vector2.MoveTowards(this.transform.position, DistanceFromPlayer(distanceFromPlayer, player.transform.position), speed * Time.deltaTime);
        }
        else if (playerLocation.x == 1 && playerLocation.y == -1) // Down Right 
        {
            distanceFromPlayer.x = 1;
            distanceFromPlayer.y = -distanceFromPlayer.y;
            this.transform.position = Vector2.MoveTowards(this.transform.position, DistanceFromPlayer(distanceFromPlayer, player.transform.position), speed * Time.deltaTime);
        }
        else if (playerLocation.x == -1 && playerLocation.y == -1) // Down Left
        {
            distanceFromPlayer.x = -distanceFromPlayer.x;
            distanceFromPlayer.y = -distanceFromPlayer.y;
            this.transform.position = Vector2.MoveTowards(this.transform.position, DistanceFromPlayer(distanceFromPlayer, player.transform.position), speed * Time.deltaTime);
        }
        else
        {
            //Player is not moving
        }
    }
    private Vector2 DistanceFromPlayer(Vector2 distance, Vector2 playerLocation)
    {
        Vector2 targetDistance = new Vector2();

        targetDistance.x += playerLocation.x + distance.x;

        targetDistance.y += playerLocation.y + distance.y;

        //Debug.Log("X: " + targetDistance.x + " Y: " + targetDistance.y);

        return targetDistance;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            speed = 0;
            waitUntil = Time.time + waitTimeLength;
        }

        if (collision.collider.CompareTag("PassiveNPC") || collision.collider.CompareTag("DrunkNPC"))
        {
            savedCollider2Ds.Add(collision.collider);
            Physics2D.IgnoreCollision(collision.collider, thisCollider2D, true);

        }


        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, radius);

    }
}
