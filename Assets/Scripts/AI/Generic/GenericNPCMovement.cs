using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericNPCMovement : MonoBehaviour
{
    [SerializeField]
    public float changeSpeed;
    [SerializeField]
    float waitTimeLength;
    [SerializeField]
    float waitUntil;
    [SerializeField]
    public Vector2 destinationLocation;
    [SerializeField]
    public GameObject fieldOfLocation;

    Rigidbody2D rb2d;

    [SerializeField]
    public float speed;

    void Start()
    {
        destinationLocation = new Vector2(fieldOfLocation.GetComponent<BoxCollider2D>().transform.position.x + Random.Range(-fieldOfLocation.GetComponent<BoxCollider2D>().size.x / 2, fieldOfLocation.GetComponent<BoxCollider2D>().size.x / 2), fieldOfLocation.GetComponent<BoxCollider2D>().transform.position.y + Random.Range(-fieldOfLocation.GetComponent<BoxCollider2D>().size.y / 2, fieldOfLocation.GetComponent<BoxCollider2D>().size.y / 2));
        waitUntil = Time.time + waitTimeLength;
        speed = changeSpeed;
        rb2d = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        Movement();
    }

    public virtual void Movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, destinationLocation, speed * Time.deltaTime);
   
        if (Time.time >= waitUntil)
        {
            //Debug.Log("Moving to Target");

            speed = changeSpeed;
            destinationLocation = new Vector2(fieldOfLocation.GetComponent<BoxCollider2D>().transform.position.x + Random.Range(-fieldOfLocation.GetComponent<BoxCollider2D>().size.x / 2, fieldOfLocation.GetComponent<BoxCollider2D>().size.x / 2), fieldOfLocation.GetComponent<BoxCollider2D>().transform.position.y + Random.Range(-fieldOfLocation.GetComponent<BoxCollider2D>().size.y / 2, fieldOfLocation.GetComponent<BoxCollider2D>().size.y / 2));
           
            waitUntil = Time.time + waitTimeLength;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            speed = 0;
        }
            

    }

}
