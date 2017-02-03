using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {
    public float zoomSpeed = 0.01f;
    public float moveSpeed = 0.01f;
    public GameObject map;
    public float cameraOffSet;

    Vector3 leftBound;
    Vector3 rightBound;
    Vector3 topBound;
    Vector3 bottomBound;

    //Camera mainCamera;
    Vector3 mapCenter;
    float mapX;
    float mapY;
    float mapZ;

    Vector3 bottomBoundPos;
    Vector3 topBoundPos;

    void Awake()
    {
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

        transform.position = new Vector3(mapCenter.x, transform.position.y, transform.position.z);

        //bottomBoundPos = new Vector3(0, mapCenter.y - mapY / 2, mapCenter.z - mapZ / 2);
        //Vector3 bottomDirection = bottomBoundPos - mapCenter;
        //bottomDirection = Quaternion.Euler(-45, 0, 0) * bottomDirection;
        //bottomBoundPos = bottomDirection + mapCenter;

        //topBoundPos = new Vector3(0, mapCenter.y - mapY / 2, mapCenter.z + mapZ / 2);
        //Vector3 topDirection = topBoundPos - mapCenter;
        //topDirection = Quaternion.Euler(-45, 0, 0) * topDirection;
        //topBoundPos = topDirection + mapCenter;

        //bottomBoundPos = mapCenter - new Vector3(mapX / 2, mapY / 2, mapZ / 2);
        //topBoundPos = mapCenter;
        //topBoundPos.x += mapX / 2;
        //topBoundPos.y -= mapY / 2;
        //topBoundPos.z += mapZ / 2; 
    }

    void Start()
    {
        //calculationPlane = new GameObject();
        //calculationPlane.transform.position = new Vector3(mapCenter.x, mapCenter.y - mapY / 2, mapCenter.z);
        //calculationPlane.AddComponent<MeshRenderer>();
        //calculationPlane.AddComponent<MeshCollider>();
        //calculationPlane.AddComponent<MeshFilter>();
        ////Renderer calculationPlaneRenderer = calculationPlane.GetComponent<Renderer>();
        //calculationPlane.transform.localScale = new Vector3(mapX, 0.1f, mapZ);
        //calculationPlane.transform.rotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.x, new Vector3(1, 0, 0));

        //map.transform.rotation = Quaternion.AngleAxis(-(Camera.main.transform.eulerAngles.x - map.transform.eulerAngles.x), new Vector3(1, 0, 0));

        //bottomBound.transform.RotateAround(mapCenter, new Vector3(1, 0, 0), -45);

        //Instantiate(calculationPlane);
    }
	
	// Update is called once per frame
	void Update () {
        //Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        //Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        //float bottomDistance = bottomLeft.z + bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2);
        //float topDistance = topRight.z + topRight.y + Math.Abs(mapCenter.y - mapY / 2);

        //if(topDistance - bottomDistance > mapZ)
        //{
        //    Debug.Log("BIGGER");
        //} else
        //{
        //    Debug.Log("SMALLER");
        //}

        //float cameraHeight = mainCamera.orthographicSize * 2.0f;
        //float cameraWidth = cameraHeight * (Screen.width / Screen.height);

        //Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        //Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        //float bottomDistance = bottomLeft.z + bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2);
        //float topDistance = topRight.z + topRight.y + Math.Abs(mapCenter.y - mapY / 2);

        ////if (bottomLeft.z >= bottomBound.transform.position.z - mapZ / 4)
        ////{
        ////    Debug.Log("AHHHHHHHHH");
        ////}

        ////if (topRight.z <= (bottomBound.transform.position.z - mapZ / 4))
        ////{
        ////    Debug.Log("UUUUUUUUUUU");
        ////}

        //if (bottomDistance >= mapCenter.z)
        //{
        //    Debug.Log("AHHHHHHHHH: " + bottomDistance + " MAPCENTER : " + mapCenter.z);
        //}

        //if (topDistance <= mapCenter.z)
        //{
        //    Debug.Log("UUUUUUUUUUU");
        //}


        //Vector3 leftBoundScreenPoint = Camera.main.WorldToViewportPoint(leftBound.transform.position);
        //Vector3 rightBoundScreenPoint = Camera.main.WorldToViewportPoint(rightBound.transform.position);
        //Vector3 topBoundScreenPoint = Camera.main.WorldToViewportPoint(topBound.transform.position);
        //Vector3 bottomBoundScreenPoint = Camera.main.WorldToViewportPoint(bottomBound.transform.position);

        //bool leftOnScreen = leftBoundScreenPoint.x > 0 && leftBoundScreenPoint.x < 1 && leftBoundScreenPoint.y > 0 && leftBoundScreenPoint.y < 1 && leftBoundScreenPoint.z > 0;
        //bool rightOnScreen = rightBoundScreenPoint.x > 0 && rightBoundScreenPoint.x < 1 && rightBoundScreenPoint.y > 0 && rightBoundScreenPoint.y < 1 && rightBoundScreenPoint.z > 0;
        //bool topOnScreen = topBoundScreenPoint.x > 0 && topBoundScreenPoint.x < 1 && topBoundScreenPoint.y > 0 && topBoundScreenPoint.y < 1 && topBoundScreenPoint.z > 0;
        //bool bottomOnScreen = bottomBoundScreenPoint.x > 0 && bottomBoundScreenPoint.x < 1 && bottomBoundScreenPoint.y > 0 && bottomBoundScreenPoint.y < 1 && bottomBoundScreenPoint.z > 0;

        //bool leftOnScreen = leftBound.GetComponent<Renderer>().isVisible;
        //bool rightOnScreen = rightBound.GetComponent<Renderer>().isVisible;
        //bool topOnScreen = topBound.GetComponent<Renderer>().isVisible;
        //bool bottomOnScreen = bottomBound.GetComponent<Renderer>().isVisible;

        //bool leftOnScreen = bottomLeft.x >= leftBound.transform.position.x;
        //bool rightOnScreen = rightBound.GetComponent<Renderer>().isVisible;
        //bool topOnScreen = topBound.GetComponent<Renderer>().isVisible;
        //bool bottomOnScreen = bottomBound.GetComponent<Renderer>().isVisible;

        //Debug.Log("LEFT: " + leftOnScreen);
        //Debug.Log("BOTTOMLEFT: " + bottomLeft.x + " LEFTBOUND: " + leftBound.transform.position.x);
        //Debug.Log("LEFT: " + leftOnScreen + " CORDS: " + leftBoundScreenPoint);
        //Debug.Log("RIGHT: " + rightOnScreen);
        //Debug.Log("TOP: " + topOnScreen);
        //Debug.Log("BOTTOM: " + bottomOnScreen);

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            //Vector3 leftBoundScreenPoint = mainCamera.WorldToViewportPoint(leftBound.transform.position);
            //Vector3 rightBoundScreenPoint = mainCamera.WorldToViewportPoint(rightBound.transform.position);
            //Vector3 topBoundScreenPoint = mainCamera.WorldToViewportPoint(topBound.transform.position);
            //Vector3 bottomBoundScreenPoint = mainCamera.WorldToViewportPoint(bottomBound.transform.position);

            //bool leftOnScreen = leftBoundScreenPoint.x > 0 && leftBoundScreenPoint.x < 1 && leftBoundScreenPoint.y > 0 && leftBoundScreenPoint.y < 1 && leftBoundScreenPoint.z > 0;
            //bool rightOnScreen = rightBoundScreenPoint.x > 0 && rightBoundScreenPoint.x < 1 && rightBoundScreenPoint.y > 0 && rightBoundScreenPoint.y < 1 && rightBoundScreenPoint.z > 0;
            //bool topOnScreen = topBoundScreenPoint.x > 0 && topBoundScreenPoint.x < 1 && topBoundScreenPoint.y > 0 && topBoundScreenPoint.y < 1 && topBoundScreenPoint.z > 0;
            //bool bottomOnScreen = bottomBoundScreenPoint.x > 0 && bottomBoundScreenPoint.x < 1 && bottomBoundScreenPoint.y > 0 && bottomBoundScreenPoint.y < 1 && bottomBoundScreenPoint.z > 0;

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

            //if (!leftOnScreen && !rightOnScreen)
            //{
            //    Camera.main.transform.Translate(-touchPreviousPos.x * moveSpeed, -touchPreviousPos.y * moveSpeed, 0);
            //}
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

        //Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        //Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        //float bottomDistance = bottomLeft.z + bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2);
        //float topDistance = topRight.z + topRight.y + Math.Abs(mapCenter.y - mapY / 2);


        //if (bottomDistance <= bottomBound.transform.position.z)
        //{
        //    Debug.Log("BOTTOM");
        //}

        //if (topDistance >= topBound.transform.position.z)
        //{
        //    Debug.Log("TOP");
        //}

        if (Input.GetKey(KeyCode.W))
        {
            //Vector3 pos = mainCamera.transform.position;
            //pos.y += 1;
            //pos.z += (1 * zYRatio);
            //mainCamera.transform.position = pos;
            transform.Translate(0, 0, -0.1f);
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, 0.1f);
        }
    }

    void LateUpdate()
    {
        //float cameraHeight = mainCamera.orthographicSize * 2.0f;
        //float cameraWidth = cameraHeight * (Screen.width / Screen.height);

        //Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        //Vector3 topLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
        //Vector3 bottomRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));
        //Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        //float cameraBoundZDistance = (topLeft.z - bottomLeft.z) * 2;

        //float minX = mapCenter.x - ((mapX - cameraWidth) / 2);
        //float maxX = mapCenter.x + ((mapX - cameraWidth) / 2);

        //float minZ = mapCenter.z - (mapZ - cameraZDistance) / 2;
        //float minZ = mapCenter.z - (mapZ - cameraBoundZDistance) / 2 - cameraBoundZDistance / 2;
        //float maxZ = mapCenter.z + (mapZ - cameraBoundZDistance) / 2;

        //float minZ = mapCenter.z - ((mapZ - cameraHeight) / 2);
        //float maxZ = mapCenter.z - ((mapZ - cameraHeight) / 2);

        //if (cameraWidth >= mapX)
        //{
        //    //CHANGE LATER. TEMP
        //    mainCamera.transform.position = new Vector3(5.7f, 7.13f, -5.27f);
        //} else if (cameraHeight >= mapZ) {
        //} else
        //{
        //    Debug.Log("CAMERA: " + mapX);
        //    Vector3 cameraPos = mainCamera.transform.position;
        //    cameraPos.x = Mathf.Clamp(cameraPos.x, minX, maxX);
        //    cameraPos.z = Mathf.Clamp(cameraPos.z, minZ, maxZ);
        //    mainCamera.transform.position = cameraPos;
        //}
    }

    //Smooth transition to desired position
    public void LookTo(Vector3 lookPos)
    {

    }

    void OnDrawGizmos()
    {
        //float cameraHeight = mainCamera.orthographicSize * 2.0f;
        //float cameraWidth = cameraHeight * (Screen.width / Screen.height);
        
        //Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        //Vector3 topLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
        //Vector3 bottomRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));
        //Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        //float cameraBoundZDistance = (topLeft.z - bottomLeft.z) * 2;
        //float cameraMapYDistance = mainCamera.transform.position.y - map.transform.position.y;
        //float topLeftZDistance = cameraMapYDistance / (float)Math.Tan(cameraAngle);
        //float zOffset = topLeftZDistance - cameraBoundZDistance + topLeft.z - bottomLeft.z;



        //float minZ = mapCenter.z - (mapZ - cameraBoundZDistance) / 2 - cameraBoundZDistance / 2;

        //float upperCameraHeight = topLeft.y - map.transform.position.y + mapY / 2;

        //float maxZ = upperCameraHeight / (float) Math.Tan(cameraAngle);

        Gizmos.color = Color.red;
        //Gizmos.DrawCube(new Vector3(0, 0, minZ), new Vector3(1f, 1f, 1f));
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireCube(calculationPlane.transform.position, calculationPlane.transform.localScale);
        //Vector3 yVector = new Vector3(0, 1, 0);
        //Vector3 yOffSet = yVector * (calculationPlane.transform.localScale.y / 2);
        ////Gizmos.DrawWireCube(new Vector3(0, calculationPlane.transform.position.y - calculationPlane.transform.localScale.y / 2, calculationPlane.transform.position.z - calculationPlane.transform.localScale.z / 2), new Vector3(1f, 1f, 1f));
        //Gizmos.DrawWireCube(new Vector3(calculationPlane.transform.position.x - calculationPlane.GetComponent<MeshFilter>().mesh.bounds.size.x / 2, calculationPlane.transform.position.y - calculationPlane.GetComponent<MeshFilter>().mesh.bounds.size.y / 2, calculationPlane.transform.position.z - calculationPlane.GetComponent<MeshFilter>().mesh.bounds.size.z / 2), new Vector3(1f, 1f, 1f));
        //Debug.Log("DISTANCE: " + calculationPlane.transform.TransformPoint(new Vector3(0, calculationPlane.transform.position.y - calculationPlane.transform.localScale.y / 2, calculationPlane.transform.position.z - calculationPlane.transform.localScale.z / 2)));
        //Debug.Log("SDFSDFSDFD: " + calculationPlane.transform.TransformPoint(new Vector3(calculationPlane.transform.position.x - calculationPlane.GetComponent<MeshFilter>().mesh.bounds.size.x / 2, calculationPlane.transform.position.y - calculationPlane.GetComponent<MeshFilter>().mesh.bounds.size.y / 2, calculationPlane.transform.position.z - calculationPlane.GetComponent<MeshFilter>().mesh.bounds.size.z / 2)));

        //Vector3 corner = new Vector3(0, mapCenter.y - mapY / 2, mapCenter.z - mapZ / 2);
        //Vector3 direction =  corner - mapCenter;
        //direction = Quaternion.Euler(-45, 0, 0) * direction;
        //corner = direction + mapCenter;

        ////Gizmos.DrawCube(mapCenter, new Vector3(1f, 1f, 1f));
        //Gizmos.DrawWireCube(bottomBoundPos, new Vector3(1f, 1f, 1f));
        //Gizmos.DrawWireCube(topBoundPos, new Vector3(1f, 1f, 1f));
        //Debug.Log("SDFDSFSDF: " + corner);
        //Gizmos.DrawCube(bottomBound.transform.position, new Vector3(1f, 1f, 1f));
        //Gizmos.DrawWireCube(bottomBoundPos, new Vector3(1f, 1f, 1f));
        //Gizmos.DrawWireCube(topBoundPos, new Vector3(1f, 1f, 1f));

        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        float bottomDistance = bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2);
        float topDistance = topRight.y + Math.Abs(mapCenter.y - mapY / 2);

        //float z1 = (bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2)) / (float) Math.Tan(45);
        //Debug.Log("MAP: " + mapCenter.y + " OFFSET: " + mapY / 2 + "RESULT: " + (mapCenter.y - mapY / 2));
        ////z1 = bottomLeft.z + z1;
        //Vector3 z1Vec = new Vector3(0, mapCenter.y - mapY / 2, z1);
        //Gizmos.DrawWireCube(z1Vec, new Vector3(1f, 1f, 1f));
        Gizmos.DrawCube(new Vector3(bottomLeft.x, bottomLeft.y - (bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2)) / 2, bottomLeft.z), new Vector3(0.1f, bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2), 0.1f));
        Gizmos.DrawCube(new Vector3(bottomLeft.x, bottomLeft.y - (bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2)), bottomLeft.z + (bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2)) / 2), new Vector3(0.1f, 0.1f, (bottomLeft.y + Math.Abs(mapCenter.y - mapY / 2))));

        Gizmos.DrawCube(new Vector3(bottomLeft.x, bottomLeft.y - bottomDistance, bottomLeft.z + bottomDistance / 2), new Vector3(0.1f, 0.1f, bottomDistance));
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(new Vector3(bottomLeft.x, topRight.y - topDistance, topRight.z + topDistance / 2), new Vector3(0.1f, 0.1f, topDistance));

        Vector3 botPos = new Vector3(0, 0, bottomLeft.z + bottomDistance);
        Vector3 topPos = new Vector3(0, 0, topRight.z + topDistance);
        Gizmos.DrawCube(botPos, new Vector3(1f, 1f, 1f));
        Gizmos.DrawCube(topPos, new Vector3(1f, 1f, 1f));

        Gizmos.color = Color.white;
        Gizmos.DrawCube(mapCenter, new Vector3(1f, 1f, 1f));
        Gizmos.DrawCube(topBound, new Vector3(1f, 1f, 1f));
        Gizmos.DrawCube(bottomBound, new Vector3(1f, 1f, 1f));
    }
}
