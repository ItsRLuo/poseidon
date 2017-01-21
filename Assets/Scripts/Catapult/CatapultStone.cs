using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultStone : MonoBehaviour {

    [SerializeField]
    private GameObject dot;
    private GameObject arc;
    float dotTimer = 0;
    float dotDelay = .04f;
    float destroyTimer = 0;
    float destroyDelay = 3f;
    bool isInvisible = false;

    private void Start()
    {
        arc = new GameObject("Arc");
    }

    void Update () {
        if (isInvisible)
        {
            if (dotTimer < dotDelay)
            {
                dotTimer += Time.deltaTime;
            }
            if (dotTimer >= dotDelay)
            {
                dotTimer = 0;
                GameObject newDot = Instantiate(dot, transform.position, Quaternion.identity);
                newDot.transform.parent = arc.transform;
                newDot.transform.LookAt(transform.position + new Vector3(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y, 0));
            }
        }

        if (destroyTimer < destroyDelay)
        {
            destroyTimer += Time.deltaTime;
        }
        if (destroyTimer >= destroyDelay)
        {
            Destroy(arc);
            Destroy(gameObject);
        }
    }

    public void SetInvisible(bool newIsInvisible)
    {
        isInvisible = newIsInvisible;
        if (isInvisible)
        {
            destroyDelay *= 2;
        }
    }
}
