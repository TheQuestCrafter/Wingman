using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Vector2 direction;
    public Vector2 directionNormalized;
    public float movementSpeed;

    private Vector3 velocity = Vector3.zero;
    private float movementSmoothing = 0.05f;
    
    public float hMove, vMove;
    public GameObject talkingTarget; // Who the player is talking to

    private void Awake()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
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
}
