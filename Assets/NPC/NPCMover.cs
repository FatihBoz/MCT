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
            pathPending = false;
            path = p;

            currentWaypoint = 0;
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
            velocity = Vector2.zero;
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
          //  rb.velocity=Vector2.zero;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }    


            Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
        velocity = dir * speed;

        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
       
        transform.position += (Vector3)velocity * Time.deltaTime;
        //   rb.velocity = velocity;

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
    
}
