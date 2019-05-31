using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericNPCMovement : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float waitTimeLength;
    [SerializeField]
    float waitUntil;
    [SerializeField]
    Vector2 destinationLocation;
    [SerializeField]
    public GameObject fieldOfLocation;

    void Start()
    {
        destinationLocation = new Vector2(fieldOfLocation.GetComponent<BoxCollider2D>().transform.position.x + Random.Range(-fieldOfLocation.GetComponent<BoxCollider2D>().size.x / 2, fieldOfLocation.GetComponent<BoxCollider2D>().size.x / 2), fieldOfLocation.GetComponent<BoxCollider2D>().transform.position.y + Random.Range(-fieldOfLocation.GetComponent<BoxCollider2D>().size.y / 2, fieldOfLocation.GetComponent<BoxCollider2D>().size.y / 2));
        waitUntil = Time.time + waitTimeLength;
    }
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, destinationLocation, speed * Time.deltaTime);
   
        if (Time.time >= waitUntil)
        {
            Debug.Log("Moving to Target");
            destinationLocation = new Vector2(fieldOfLocation.GetComponent<BoxCollider2D>().transform.position.x + Random.Range(-fieldOfLocation.GetComponent<BoxCollider2D>().size.x / 2, fieldOfLocation.GetComponent<BoxCollider2D>().size.x / 2), fieldOfLocation.GetComponent<BoxCollider2D>().transform.position.y + Random.Range(-fieldOfLocation.GetComponent<BoxCollider2D>().size.y / 2, fieldOfLocation.GetComponent<BoxCollider2D>().size.y / 2));
           
            waitUntil = Time.time + waitTimeLength;
        }
    }
}
