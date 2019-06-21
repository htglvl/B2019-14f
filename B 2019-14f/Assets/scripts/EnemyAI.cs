using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target, EnemyGFX;
    public float speed = 200f, nextWayPointDistance = 1.2f, repeatRate = 0.5f;
    Path path;
    int currentWaypoint = 0;
    bool reachEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb2D = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, repeatRate);
    }
    void UpdatePath()
    {
        if(seeker.IsDone())
        seeker.StartPath(rb2D.position, target.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachEndOfPath = false;
            return;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb2D.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb2D.AddForce(force);
        float distane = Vector2.Distance(rb2D.position, path.vectorPath[currentWaypoint]);
        if (distane < nextWayPointDistance)
        {
            currentWaypoint++;
        }
        if (rb2D.velocity.x >= 0.01f)
        {
            EnemyGFX.localScale = new Vector3( 1f, 1f, 1f);
        }else if(rb2D.velocity.x <= 0.01f)
        {
            EnemyGFX.localScale = new Vector3( -1f, 1f, 1f);
        }
    }

}
