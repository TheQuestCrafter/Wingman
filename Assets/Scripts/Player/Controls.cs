using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameObject loadingScreen;
    public Rigidbody2D rb2d;
    public CircleCollider2D cc2d;
    public Animator myAnimator;
    public static bool canMove; // if in a loading screen or talking, don't let the player move

    public float hMove, vMove;
    public Vector2 direction;
    public Vector2 directionNormalized;
    public float movementSpeed;

    private Vector3 velocity = Vector3.zero;
    private float movementSmoothing = 0.05f;
    private int loadingScreenCheck;

    public GameObject followingTarget; // Who is following the player.
    public GameObject talkingTarget; // Who the player is talking to. Will be most recently entered "Lover".
    public List<GameObject> nearbyTargets;
    public bool spaceDown;

    private void Awake()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        cc2d = this.gameObject.GetComponent<CircleCollider2D>();
        myAnimator = this.gameObject.GetComponent<Animator>();
        movementSpeed = 2;
        loadingScreenCheck = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (loadingScreen == null && loadingScreenCheck == 0)
        {
            canMove = true;
            loadingScreenCheck++;
        }            

        if(canMove)
            MovePlayer();

    }

    private void FixedUpdate()
    {
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

        UpdateAnimator();
        //rb2d.velocity = direction * movementSpeed;

    }

    private void UpdateAnimator()
    {
        if(direction.x == 0 && direction.y == 0)
        {
            myAnimator.SetInteger("Direction", 0);
        }
        else if (direction.x == 0 && direction.y == -1)
        {
            myAnimator.SetInteger("Direction", 1);
        }
        else if (direction.x == 0 && direction.y == 1)
        {
            myAnimator.SetInteger("Direction", 2);
        }
        else if (direction.x == 1 && direction.y == 0)
        {
            myAnimator.SetInteger("Direction", 3);
        }
        else if (direction.x == -1 && direction.y == 0)
        {
            myAnimator.SetInteger("Direction", 4);
        }
        else if (direction.x == 1 && direction.y == -1)
        {
            myAnimator.SetInteger("Direction", 5);
        }
        else if (direction.x == 1 && direction.y == 1)
        {
            myAnimator.SetInteger("Direction", 6);
        }
        else if (direction.x == -1 && direction.y == -1)
        {
            myAnimator.SetInteger("Direction", 7);
        }
        else if (direction.x == -1 && direction.y == 1)
        {
            myAnimator.SetInteger("Direction", 8);
        }
    }

    private void UpdateClosestTalkingTarget()
    {
        float localMin = 10000;
        foreach(GameObject go in nearbyTargets)
        {
            if(Vector3.Distance(go.transform.position, this.gameObject.transform.position) <= localMin)
            {
                localMin = Vector3.Distance(go.transform.position, this.gameObject.transform.position);
                talkingTarget = go;
            }
        }

    }

    private void MatchSelected()
    {
        Manager.CheckMatch();
    }

    private void Talk()
    {
        // Send the talking target to the GameManager
        // Game Manager will handle the dialogue
        Manager.playerTalkingTarget = this.talkingTarget;
        canMove = false;


        if (followingTarget != null) // Match? Dialogue
        {
            Manager.TurnOnUI(2);
        }
        else // follow dialogue
        {
            Manager.TurnOnUI(1);
        }
    }

    private void CheckTalkButton()
    {
        spaceDown = Input.GetKeyDown("e");

        if(spaceDown)
        {
            UpdateClosestTalkingTarget();
            if (talkingTarget != null && talkingTarget != followingTarget) // talking target is not blank
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
            nearbyTargets.Add(collision.gameObject);
            //if(nearbyTargets.Count == 1)
            //{
            //    talkingTarget = collision.gameObject;
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lover") // ADD TAG  
        {
            nearbyTargets.Remove(collision.gameObject);
            
            //if(nearbyTargets.Count > 0)
            //{
            //    talkingTarget = nearbyTargets[nearbyTargets.Count - 1]; // assign to most recently entered target
            //}
        }
    }
}
