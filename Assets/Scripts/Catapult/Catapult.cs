using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour {

    [SerializeField]
    private GameObject catapultStone;
    [SerializeField]
    private GameObject catapultStoneInvisible;
    private float xForceMin = -120;
    private float xForceMax = -20;
    private float yForceMin = 100;
    private float yForceMax = 140;
    private float xForce = 0;
    private float yForce = 0;

    float fireTimer = 0;
    public float fireDelay = 2;
    float autoFireTimer = 0;
    public float autoFireDelay = 3;

    bool isStoneFiring = false;

    void Start () {

	}

	void Update () {
        if (isStoneFiring)
        {
            if (fireTimer < fireDelay)
            {
                fireTimer += Time.deltaTime;
            }
            if (fireTimer >= fireDelay)
            {
                fireTimer = 0;
                LaunchStone(false);
                isStoneFiring = false;
            }
        }
        else
        {
            if (autoFireTimer < autoFireDelay)
            {
                autoFireTimer += Time.deltaTime;
            }
            if (autoFireTimer >= autoFireDelay)
            {
                autoFireTimer = 0;
                SetForce();
                LaunchStone(true);
                isStoneFiring = true;
            }
        }
	}

    void SetForce()
    {
        xForce = Random.Range(xForceMin, xForceMax);
        yForce = Random.Range(yForceMin, yForceMax);
    }

    void LaunchStone(bool isInvisible)
    {
        GameObject newStone;
        if (isInvisible)
        {
            newStone = Instantiate(catapultStoneInvisible, new Vector3(transform.position.x, transform.position.y, 15), Quaternion.identity);
        }
        else
        {
            newStone = Instantiate(catapultStone, new Vector3(transform.position.x, transform.position.y, 12), Quaternion.identity);
        }
        newStone.GetComponent<Rigidbody>().AddForce(new Vector2(xForce, yForce));
        newStone.GetComponent<CatapultStone>().SetInvisible(isInvisible);
    }
}
