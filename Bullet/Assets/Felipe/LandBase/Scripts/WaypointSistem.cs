using UnityEngine;
using System.Collections;

public class WaypointSistem : PlatformController {

    public Vector3[] localWaypoints;
    Vector3[] globalWaypoint;

    Vector3 globalWaypointPos;

    // Use this for initialization
    void Start () {
        
        globalWaypoint = new Vector3[localWaypoints.Length];
        for (int i = 0; i < localWaypoints.Length; i++) {
            globalWaypoint[i] = localWaypoints[i] + transform.position;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos() {
        if (localWaypoints != null) {
            Gizmos.color = Color.blue;
            float size = .5f;
            for (int i = 0; i < localWaypoints.Length; i++)
                globalWaypointPos = localWaypoints[i] + transform.position;
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
            {   



            }
        }
    }
}
