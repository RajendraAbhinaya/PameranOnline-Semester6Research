using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> neighbours;
    public GameObject pointOfInterest;
    [HideInInspector]
    public Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    public Waypoint NextWaypoint(){
        int length = neighbours.Count;
        return neighbours[Random.Range(0, length)];
    }
}
