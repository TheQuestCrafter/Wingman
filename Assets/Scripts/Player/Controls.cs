using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public CircleCollider2D cc2d;

    public float hMove, vMove;
    public Vector2 direction;
    public Vector2 directionNormalized;
    public float movementSpeed;

    private Vector3 velocity = Vector3.zero;
    private float movementSmoothing = 0.05f;
    
    public GameObject talkingTarget; // Who the player is talking to. Will be most recently entered "Lover".
    public List<GameObject> nearbyTargets;
    public bool spaceDown;

    private void Awake()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        cc2d = this.gameObject.GetComponent<CircleCollider2D>();
        movementSpeed = 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
        CheckTalkButton();
    }

    private void MovePlayer()
    {
        if (hMove == 0 && vMove == 0)
            rb2d.velocity = new Vector2();

        // Player movement here
        hMove = Input.GetAxis("Horizontal");
        vMove = Input.GetAxis("Vertical");

        direction = new Vector2(hMove, vMove);
        directionNormalized = direction;
        directionNormalized.Normalize();

        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, (directionNormalized * movementSpeed), ref velocity, movementSmoothing);

        //rb2d.velocity = direction * movementSpeed;

    }

    private void RotatePlayer()
    {
        // Update the sprite here
    }

    private void Talk()
    {

    }

    private void CheckTalkButton()
    {
        spaceDown = Input.GetKeyDown("space");

        if(spaceDown)
        {
            if(talkingTarget != null) // talking target is not blank
            {
                if(nearbyTargets.Contains(talkingTarget)) // talking target is within talking radius
                {
                    Talk();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lover") // ADD TAG  
        {
            talkingTarget = collision.gameObject;
            nearbyTargets.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lover") // ADD TAG  
        {
            nearbyTargets.Remove(collision.gameObject);
            
            if(nearbyTargets.Count > 0)
            {
                talkingTarget = nearbyTargets[nearbyTargets.Count - 1]; // assign to most recently entered target
            }
        }
    }
}
