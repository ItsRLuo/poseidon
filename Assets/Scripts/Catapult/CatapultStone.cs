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

    [SerializeField]
    private GameObject particleSystem;

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
                newDot.transform.LookAt(transform.position + new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x, gameObject.GetComponent<Rigidbody>().velocity.y, 0));
            }
        }

            if (destroyTimer < destroyDelay * 3)
            {
                destroyTimer += Time.deltaTime;
            }
        {
            if ((isInvisible && destroyTimer >= destroyDelay) || destroyTimer >= destroyDelay * 3)
            {
                Destroy(arc);
                Destroy(gameObject);
            }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.layer = 8;
            gameObject.GetComponent<SphereCollider>().isTrigger = false;
            if (particleSystem != null)
            {
                particleSystem.GetComponent<ParticleSystem>().Stop(); 
            }
        }
    }
}
