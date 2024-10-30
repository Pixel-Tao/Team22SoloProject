using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveableObject : MonoBehaviour
{
    public List<Vector3> destinations;
    public float speed;

    protected int currentDestinationIndex = 0;
    protected Vector3 currentMovePosition;


    protected virtual void Start()
    {
    }

    protected virtual void FixedUpdate()
    {
        if (destinations.Count > 0)
        {
            // 목적지를 이동하면서
            currentMovePosition = Vector3.MoveTowards(transform.position, destinations[currentDestinationIndex], speed * Time.fixedDeltaTime);
            transform.position = currentMovePosition;
            if (
                Mathf.Approximately(transform.position.x, destinations[currentDestinationIndex].x) &&
                Mathf.Approximately(transform.position.y, destinations[currentDestinationIndex].y) &&
                Mathf.Approximately(transform.position.z, destinations[currentDestinationIndex].z)
                )
            {
                currentDestinationIndex = (currentDestinationIndex + 1) % destinations.Count;
            }
        }
    }
}
