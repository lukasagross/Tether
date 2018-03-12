using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] Waypoints;
    public float speed = 2;

    private Vector2 oldpos;
    private Vector2 movedistance;
    public int CurrentPoint = 0;

    void Start(){

        oldpos = transform.position;
    }

    void Update()
    {

        movedistance = (Vector2)transform.position - oldpos;
        oldpos = transform.position;

        if ((transform.position.x != Waypoints[CurrentPoint].transform.position.x) || (transform.position.y != Waypoints[CurrentPoint].transform.position.y))
        {
            transform.position = Vector3.MoveTowards(transform.position, Waypoints[CurrentPoint].transform.position, speed * Time.deltaTime);
        }

        if ((transform.position.x == Waypoints[CurrentPoint].transform.position.x) && (transform.position.y == Waypoints[CurrentPoint].transform.position.y))
        {
            CurrentPoint += 1;
        }
        if (CurrentPoint >= Waypoints.Length)
        {
            CurrentPoint = 0;
        }
    }

    public Vector2 GetMoveDistance()
    {
        return movedistance;
    }
}
