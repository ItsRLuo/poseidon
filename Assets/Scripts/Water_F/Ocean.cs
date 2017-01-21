using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour {

    [SerializeField]
    private GameObject waterPixel;
    private Transform waterParent;
    public Camera mainCamera;

    [SerializeField]
    private GameObject targetingBarPrefab;
    [SerializeField]
    private GameObject launchAnimationPrefab;
    private GameObject targetingBar;

    private int screenWidth = 180;
    private int screenHeight = 20;

    GameObject[] waterArray;
    WaterPixel[] waterPixelArray;

    private float dropoff = 0.98f;

    void Start()
    {
        waterParent = new GameObject("_WaterParent").transform;
        waterArray = new GameObject[screenWidth + 1];
        waterPixelArray = new WaterPixel[screenWidth + 1];
        for (int i = 0; i <= screenWidth; i++)
        {
            PlaceWater(i);
        }
    }

    private void Update()
    {
        CheckDrag();
        return;
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 targetPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            int targetX = (int)targetPoint.x * 10;
            float targetY = targetPoint.y;

            if (targetX >= 0 && targetX <= screenWidth)
            {
                AddForce(targetX, 30);
            }
        }
    }

    private void CheckDrag()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    MouseEvent("Down", touch.fingerId, touch.position);
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    MouseEvent("Up", touch.fingerId, touch.position);
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    MouseEvent("Move", touch.fingerId, touch.position);
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                MouseEvent("Down", 0, Input.mousePosition);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                MouseEvent("Up", 0, Input.mousePosition);
            }
            else if (Input.GetButton("Fire1"))
            {
                MouseEvent("Move", 0, Input.mousePosition);
            }
        }
    }

    void MouseEvent(string type, int fingerId, Vector2 touchPosition)
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(touchPosition);
        Vector3 screenPosition = touchPosition;
        Vector3 newPosition = new Vector3(mousePosition.x, mousePosition.y, -2);

        if (type == "Down")
        {
            if (targetingBar == null)
            {
                targetingBar = GameObject.Instantiate(targetingBarPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                Vector3[] positions = new Vector3[] { newPosition, newPosition };
                targetingBar.GetComponent<LineRenderer>().SetPositions(positions);
            }
        }
        else if (type == "Up")
        {
            if (targetingBar != null)
            {
                Vector3 startPosition = targetingBar.GetComponent<LineRenderer>().GetPosition(0);
                GameObject launchAnimation = GameObject.Instantiate(launchAnimationPrefab, newPosition, Quaternion.identity) as GameObject;
                launchAnimation.GetComponent<LaunchAnimation>().SetEndPosition(startPosition);
                Destroy(targetingBar);
                float force = (startPosition.y - newPosition.y) * 4;
                //if (startPosition.y < newPosition.y)
                //{
                //    force *= -1;
                //}
                AddForce((int)startPosition.x * 10, force);
            }

        }
        else if (type == "Move")
        {
            if (targetingBar != null)
            { 
                targetingBar.GetComponent<LineRenderer>().SetPosition(1, newPosition);
            }
        }
    }

    void PlaceWater(int xPos)
    {
        GameObject newWater = GameObject.Instantiate(waterPixel, new Vector3(xPos * 0.1f, 0, 20), Quaternion.identity) as GameObject;
        newWater.transform.localScale = new Vector3(0.1f, screenHeight, 40);
        newWater.transform.parent = waterParent;
        newWater.name = ("Water_" + xPos);
        waterArray[xPos] = newWater;
        waterPixelArray[xPos] = new WaterPixel();
        waterPixelArray[xPos].Setup(newWater);
    }
	
    void AddForce(int startingPixel, float force)
    {
        if (waterPixelArray[startingPixel].GetVelocityMagnitude() > 15)
        {
            force *= 0.1f;
        }
            
        for (int i = 0; i <= screenWidth; i++)
        {

            if (startingPixel + i <= screenWidth)
            {
                waterPixelArray[startingPixel + i].AddForce(force);
            }
            if (startingPixel - i >= 0 && i != 0)
            {
                waterPixelArray[startingPixel - i].AddForce(force);
            }
            //force = force - Mathf.Pow(i * 0.03f, 2) / 5;
            force *= dropoff;
        }
    }
}
