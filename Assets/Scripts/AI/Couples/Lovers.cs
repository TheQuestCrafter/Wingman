using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lovers : MonoBehaviour
{
    enum LoverStates
    {
        LookingForLover,
        FollowingForLover,
        WalkAway
    }

    [SerializeField]
    LoverStates currentState;
    [SerializeField]
    Vector2 distanceFromPlayer;
    [SerializeField]
    float patience;
    [SerializeField]
    float speed;
    [SerializeField]
    public GameObject player;

    Camera cam;
    public string interest;
    public bool walkUp;

    private Vector2 playerLocation;
    private Plane[] cameraPlanes;
    private Vector3 velocity = Vector3.zero;
    private float movementSmoothing = 0.05f;
    private Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        currentState = LoverStates.LookingForLover;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        switch(currentState)
        {
            case (LoverStates.LookingForLover):
                UpdateLookingForLover();
                break;
            case (LoverStates.FollowingForLover):
                UpdateFollowingForLover();
                break;
            case (LoverStates.WalkAway):
                UpdateWalkAway();
                break;
        }
    }

    private void UpdateLookingForLover()
    {
        if(player.GetComponent<Controls>().talkingTarget.Equals(this.gameObject))
        {
            currentState = LoverStates.FollowingForLover;
        }
    }

    private void UpdateFollowingForLover()
    {
        playerLocation = player.GetComponent<Controls>().direction;
        MovementForLover();
        if(patience <= 0)
        {
            currentState = LoverStates.WalkAway;
        }
    }

    private void UpdateWalkAway()
    {
        WalkAway();
    }

    private void MovementForLover()
    {
        if (playerLocation.x == 0 && playerLocation.y == 1) // Up
        {
            distanceFromPlayer.x = 0;
            distanceFromPlayer.y = -distanceFromPlayer.y;
            this.transform.position = Vector2.MoveTowards(this.transform.position, DistanceFromPlayer(distanceFromPlayer, player.transform.position), speed * Time.deltaTime);
        }
        else if (playerLocation.x == 0 && playerLocation.y == -1) // Down
        {
            distanceFromPlayer.x = 0;
            this.transform.position = Vector2.MoveTowards(this.transform.position, DistanceFromPlayer(distanceFromPlayer, player.transform.position), speed * Time.deltaTime);

        }
        else if (playerLocation.x == -1 && playerLocation.y == 0) //Left
        {
            distanceFromPlayer.x = -distanceFromPlayer.x;
            distanceFromPlayer.y = 0;
            this.transform.position = Vector2.MoveTowards(this.transform.position, DistanceFromPlayer(distanceFromPlayer, player.transform.position), speed * Time.deltaTime);
        }
        else if (playerLocation.x == 1 && playerLocation.y == 0) // Right 
        {
            distanceFromPlayer.y = 0;
            this.transform.position = Vector2.MoveTowards(this.transform.position, DistanceFromPlayer(distanceFromPlayer, player.transform.position), speed * Time.deltaTime);
        }
        else if (playerLocation.x == 1 && playerLocation.y == 1) // Up Right
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, DistanceFromPlayer(distanceFromPlayer, player.transform.position), speed * Time.deltaTime);
        }
        else if (playerLocation.x == -1 && playerLocation.y == 1) // Up Left
        {
            distanceFromPlayer.x = -distanceFromPlayer.x;
            this.transform.position = Vector2.MoveTowards(this.transform.position, DistanceFromPlayer(distanceFromPlayer, player.transform.position), speed * Time.deltaTime);
        }
        else if (playerLocation.x == 1 && playerLocation.y == -1) // Down Right 
        {
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
        PatienceDecrease();
    }

    private Vector2 DistanceFromPlayer(Vector2 distance,  Vector2 playerLocation)
    {
        Vector2 targetDistance = new Vector2();

        targetDistance.x += playerLocation.x + distance.x;

        targetDistance.y += playerLocation.y + distance.y;

        Debug.Log("X: " + targetDistance.x + " Y: " + targetDistance.y);

        return targetDistance;
    }

    private void WalkAway()
    {
        if(walkUp)
        {

        }
        else if(!walkUp)
        {

        }

        DestroySelf();
    }

    private void DestroySelf()
    {
        cameraPlanes = GeometryUtility.CalculateFrustumPlanes(cam);

        if(!GeometryUtility.TestPlanesAABB(cameraPlanes, collider.bounds))
        {
            Destroy(this, .5f);
        }
    }

    private void PatienceDecrease()
    {
        patience -= patience * .05f;
    }

    public string ReturnInterest()
    {
        return interest;
    }
}
