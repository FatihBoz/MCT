using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class NPCMover : MonoBehaviour
{

    public Vector3 targetPosition;

    public Path path;
    public float speed = 2;
    public float nextWaypointDistance = .5f;

    private int currentWaypoint = 0;

    public bool reachedEndOfPath = false;

    private Seeker seeker;

    public bool pathPending;

    public bool isStopped;

    public Vector2 velocity;

    public bool IsMoving { get;private set; }

    //   private Rigidbody2D rb;
    private Path oldPath;

    void Start()
    {
      //  rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        velocity = Vector2.zero;
        oldPath = null;

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
            oldPath = path;
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
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            velocity = Vector2.zero;
            IsMoving = false;
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }


        IsMoving = true;
        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
        velocity = dir * speed;
        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
        transform.position += (Vector3)velocity * Time.deltaTime;


        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
    public void SetIsMoving(bool isMoving)
    {
        IsMoving = isMoving;
    }
    
}
