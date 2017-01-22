using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScene : MonoBehaviour
{
    public GameObject LightningPrefab;

    public void Start()
    { }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BoatController[] boats = GameObject.FindObjectsOfType<BoatController>();
            foreach (BoatController boat in boats)
            {
                GameObject go = GameObject.Instantiate(LightningPrefab);
                Lightning l = go.GetComponent<Lightning>();
                var p = boat.transform.position;
                p.x -= 4; // not sure why needed
                p.y -= 1; // not sure why needed
                l.EndPosition = p;
                p.y += 40;
                l.StartPosition = p;
            }
        }
    }
}
