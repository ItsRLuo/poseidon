using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour {

    [SerializeField]
    private GameObject waterPixel;
    private Transform waterParent;
    public Camera mainCamera;

    private int screenWidth = 180;
    private int screenHeight = 20;

    GameObject[] waterArray;
    WaterPixel[] waterPixelArray;

    private float dropoff = 0.98f;

    void Start () 
    {
        waterParent = new GameObject("_WaterParent").transform;
        waterArray = new GameObject[screenWidth + 1];
        waterPixelArray = new WaterPixel[screenWidth + 1];
        for (int i = 0; i <= screenWidth; i++)
        {
            PlaceWater(i);
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
