using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {
    static CameraScript instance;

    public float zoomSpeed = 0.01f;
    public float moveSpeed = 0.01f;
    public GameObject map;
    public float cameraOffSet;
    public float cameraSpeed;

    Vector3 leftBound;
    Vector3 rightBound;
    Vector3 topBound;
    Vector3 bottomBound;

    //Camera mainCamera;
    Vector3 mapCenter;
    float mapX;
    float mapY;
    float mapZ;

    float yDistance;

    Vector3 bottomBoundPos;
    Vector3 topBoundPos;

    Vector3 focusPoint;
    Vector3 lookDestination;
    bool ableToMoveCamera;

    void Awake()
    {
        instance = this;
        //mainCamera = GetComponent<Camera>();
        Renderer mapRenderer = map.GetComponent<Renderer>();
        mapCenter = mapRenderer.bounds.center;
        mapX = mapRenderer.bounds.size.x;
        mapY = mapRenderer.bounds.size.y;
        mapZ = mapRenderer.bounds.size.z;

        leftBound = mapCenter;
        leftBound.x -= mapX / 2;
        leftBound.y -= mapY / 2;

        rightBound = mapCenter;
        rightBound.x += mapX / 2;
        rightBound.y -= mapY / 2;

        topBound = mapCenter;
        topBound.z += mapZ / 2 + cameraOffSet;
        topBound.y -= mapY / 2;

        bottomBound = mapCenter;
        bottomBound.z -= mapZ / 2 + cameraOffSet;
        bottomBound.y -= mapY / 2;

        yDistance = transform.position.y + Math.Abs(mapCenter.y - mapY / 2);

        focusPoint = new Vector3(transform.position.x, transform.position.y - yDistance, transform.position.z + yDistance);
        
        LookAt(mapCenter);
    }

    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {
        if (lookVecDifference(transform.position, lookDestination))
        {
            ableToMoveCamera = true;
        }

        //if (transform.position == lookDestination)
        //{
        //    ableToMoveCamera = true;
        //}

        if (!ableToMoveCamera)
        {
            transform.position = Vector3.Lerp(instance.transform.position, lookDestination, instance.cameraSpeed * Time.deltaTime);
            //instance.transform.position += difference;
            //CalculateFocusPoint();
        }

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && ableToMoveCamera)
        {

            Vector2 touchPreviousPos = Input.GetTouch(0).deltaPosition;

            Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

            float bottomDistance = bottomLeft.z + bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2);
            float topDistance = topRight.z + topRight.y + Math.Abs(mapCenter.y - mapY / 2);

            float moveX = touchPreviousPos.x;
            float moveZ = touchPreviousPos.y;

            if (Camera.main.transform.position.x <= leftBound.x)
            {
                if (touchPreviousPos.x > 0)
                {
                    moveX = 0;
                }
            }

            if (Camera.main.transform.position.x >= rightBound.x)
            {
                if (touchPreviousPos.x < 0)
                {
                    moveX = 0;
                }
            }

            if (bottomDistance <= bottomBound.z)
            {
                if(touchPreviousPos.y > 0)
                {
                    moveZ = 0;
                }
            }

            if (topDistance >= topBound.z)
            {
                if (touchPreviousPos.y < 0)
                {
                    moveZ = 0;
                }
            }

            transform.Translate(-moveX * moveSpeed, 0, -moveZ * moveSpeed);
        }

        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            Touch touchOne = Input.GetTouch(0);
            Touch touchTwo = Input.GetTouch(1);

            Vector2 touchOnePreviousPos = touchOne.position - touchOne.deltaPosition;
            Vector2 touchTwoPreviousPos = touchTwo.position - touchTwo.deltaPosition;

            float previousTouchMagnitude = (touchOnePreviousPos - touchTwoPreviousPos).magnitude;
            float touchMagnitude = (touchOne.position - touchTwo.position).magnitude;

            float magnitudeDifference = previousTouchMagnitude - touchMagnitude;

            Camera.main.orthographicSize += magnitudeDifference * zoomSpeed;
            Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 2f);
            Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize, 8f);
        }
	}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, -0.1f);
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, 0.1f);
        }
    }

    void LateUpdate()
    {
    }
    
    public static void LookAt(Vector3 lookPos)
    {
        instance.ableToMoveCamera = false;
        instance.CalculateFocusPoint();
        Vector3 difference = new Vector3(lookPos.x - instance.focusPoint.x, 0, lookPos.z - instance.focusPoint.z);
        Vector3 destination = instance.transform.position + difference;

        instance.lookDestination = destination;
    }

    public static bool AbleToMoveCamera()
    {
        return instance.ableToMoveCamera;
    }

    void CalculateFocusPoint()
    {
        focusPoint = new Vector3(transform.position.x, transform.position.y - yDistance, transform.position.z + yDistance);
    }

    bool lookVecDifference(Vector3 startPos, Vector3 endPos)
    {
        return Vector3.SqrMagnitude(startPos - endPos) < 0.001;
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;

        //Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        //Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        //float bottomDistance = bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2);
        //float topDistance = topRight.y + Math.Abs(mapCenter.y - mapY / 2);

        //Gizmos.DrawCube(new Vector3(bottomLeft.x, bottomLeft.y - (bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2)) / 2, bottomLeft.z), new Vector3(0.1f, bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2), 0.1f));
        //Gizmos.DrawCube(new Vector3(bottomLeft.x, bottomLeft.y - (bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2)), bottomLeft.z + (bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2)) / 2), new Vector3(0.1f, 0.1f, (bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2))));

        //Gizmos.DrawCube(new Vector3(bottomLeft.x, bottomLeft.y - bottomDistance, bottomLeft.z + bottomDistance / 2), new Vector3(0.1f, 0.1f, bottomDistance));
        //Gizmos.color = Color.blue;
        //Gizmos.DrawCube(new Vector3(bottomLeft.x, topRight.y - topDistance, topRight.z + topDistance / 2), new Vector3(0.1f, 0.1f, topDistance));

        //Vector3 botPos = new Vector3(0, 0, bottomLeft.z + bottomDistance);
        //Vector3 topPos = new Vector3(0, 0, topRight.z + topDistance);
        //Gizmos.DrawCube(botPos, new Vector3(1f, 1f, 1f));
        //Gizmos.DrawCube(topPos, new Vector3(1f, 1f, 1f));

        //Gizmos.color = Color.white;
        //Gizmos.DrawCube(mapCenter, new Vector3(1f, 1f, 1f));
        //Gizmos.DrawCube(topBound, new Vector3(1f, 1f, 1f));
        //Gizmos.DrawCube(bottomBound, new Vector3(1f, 1f, 1f));

        //float yDistance = transform.position.y + Math.Abs(mapCenter.y - mapY / 2);
        //Vector3 point = new Vector3(transform.position.x, transform.position.y - yDistance, transform.position.z + yDistance);
        

        //Gizmos.DrawWireCube(new Vector3(0, transform.position.y - yDistance / 2, transform.position.z + yDistance / 2), new Vector3(1f, yDistance, yDistance));
        //Gizmos.DrawCube(focusPoint, new Vector3(1f, 1f, 1f));
    }
}
