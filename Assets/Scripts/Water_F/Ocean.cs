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

    private int screenWidth = 250;
    private int screenHeight = 20;

    GameObject[] waterArray;
    WaterPixel[] waterPixelArray;

    private float dropoff = 0.9f;

    private float wavePower = 4;
    private float velocityMax = 8;

    private int passDirection = 1;

    public bool DisableInput; // for main menu

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
        if (Time.timeScale < 1) { return; }
        CheckDrag();
        SmoothWave();
    }

    private void CheckDrag()
    {
        if (this.DisableInput)
        {
            return;
        }

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

                Vector3 launchVector = startPosition - newPosition;
                float angle = Mathf.Atan2(launchVector.y, launchVector.x);

                launchAnimation.transform.eulerAngles = new Vector3(0, 0, angle * 180 / Mathf.PI + 90);
                launchAnimation.GetComponent<LaunchAnimation>().SetEndPosition(startPosition);
                Destroy(targetingBar);
                float force = (startPosition.y - newPosition.y) * wavePower;
                float forceX = startPosition.x - newPosition.x;

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
        newWater.transform.localScale = new Vector3(0.1f, screenHeight, 20);
        newWater.transform.parent = waterParent;
        newWater.name = ("Water_" + xPos);
        waterArray[xPos] = newWater;
        waterPixelArray[xPos] = new WaterPixel();
        waterPixelArray[xPos].Setup(newWater);
    }
	
    void AddForce(int startingPixel, float force)
    {
        if (waterPixelArray[startingPixel].GetVelocityMagnitude() > velocityMax)
        {
            force *= 0.1f;
        }
            
        for (int i = 0; i <= screenWidth; i++)
        {
            //if (i > 30) { return; }
            if (startingPixel + i <= screenWidth)
            {
                waterPixelArray[startingPixel + i].AddForce(force);
            }
            if (startingPixel - i >= 0 && i != 0)
            {
                waterPixelArray[startingPixel - i].AddForce(force);
            }
            force *= dropoff;
        }
    }

    void SmoothWave()
    {

        for (int i = 0; i <= screenWidth - 4; i++)
        {
            if (i + passDirection >= 0 && i + passDirection <= screenWidth) { 
                if (waterArray[i].transform.position.y > waterArray[i + passDirection].transform.position.y + 0.01f ||
                    waterArray[i].transform.position.y < waterArray[i + passDirection].transform.position.y - 0.01f)
                {
                    float averageY = (waterArray[i].transform.position.y + waterArray[i + passDirection].transform.position.y) / 2;
                    waterArray[i + passDirection].transform.position = new Vector3(waterArray[i + passDirection].transform.position.x, averageY, waterArray[i + passDirection].transform.position.z);
                }
            }
        }
        passDirection *= -1;
    }
}
