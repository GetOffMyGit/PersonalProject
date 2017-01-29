using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public Transform target;
    float speed = 0.05f;
    Vector3[] path;
    int targetIndex;

	// Use this for initialization
	void Start () {
	}

    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PathFindingManager.RequestPath(transform.position, target.position, OnPathFound);
        }
    }

    void OnPathFound(Vector3[] newPath, bool isSuccess)
    {
        Debug.Log("PATH FOUND: " + isSuccess);
        if(isSuccess)
        {
            path = newPath;
            StartCoroutine(FollowPath());
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while(true)
        {
            if(transform.position == currentWaypoint)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    targetIndex = 0;
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed);
            yield return null;
        }
    }
}
