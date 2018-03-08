using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] Waypoints;
    public float speed = 2;

    public int CurrentPoint = 0;

    void Update()
    {
        if (transform.position.x != Waypoints[CurrentPoint].transform.position.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, Waypoints[CurrentPoint].transform.position, speed * Time.deltaTime);
        }

        if (transform.position.x == Waypoints[CurrentPoint].transform.position.x)
        {
            CurrentPoint += 1;
        }
        if (CurrentPoint >= Waypoints.Length)
        {
            CurrentPoint = 0;
        }
    }
}
