using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class NPCMover : MonoBehaviour
{

    public Vector3 targetPosition;

    public Path path;
    public float speed = 2;
    public float nextWaypointDistance = .3f;

    private int currentWaypoint = 0;

    public bool reachedEndOfPath = false;

    private Seeker seeker;

    public bool pathPending;

    public bool isStopped;

    public Vector2 velocity;

    public bool IsMoving { get;private set; }

    //   private Rigidbody2D rb;

    void Start()
    {
      //  rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        velocity = Vector2.zero;

    }

    public void CreatePath(Vector3 pathVector)
    {
        pathPending = true;
        targetPosition = pathVector;
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);

    }
    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            currentWaypoint = 0;
            pathPending = false;
            path = p;
        }
    }

    public void Update()
    {
        if (isStopped)
        {
            velocity = Vector2.zero;
            return;
        }

        if (path==null)
        {
            IsMoving = false;
            return;
        }

        reachedEndOfPath = false;

        float distanceToWaypoint;

        while (true)
        {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    IsMoving = false;
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        // Direction to the next waypoint
        // Normalize it so that it has a length of 1 world unit
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        // Multiply the direction by our desired speed to get a velocity
        velocity = dir * speed;

        // Move the agent using the CharacterController component
        // Note that SimpleMove takes a velocity in meters/second, so we should not multiply by Time.deltaTime

        IsMoving = true;
        // If you are writing a 2D game you should remove the CharacterController code above and instead move the transform directly by uncommenting the next line
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
    public void SetIsMoving(bool isMoving)
    {
        IsMoving = isMoving;
    }
    
}
