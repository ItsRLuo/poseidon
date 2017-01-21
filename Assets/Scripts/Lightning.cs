using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private bool triggered;
    private GameObject g;

    public float Duration = 0.25f;
    public Vector3 StartPosition;
    public Vector3 EndPosition;
    public GameObject ActualLightningBolt;

    public void Start()
    {
        this.g = GameObject.Instantiate(this.ActualLightningBolt);
        LightningBoltScript l = this.g.GetComponent<LightningBoltScript>();

        l.StartPosition = this.StartPosition;
        l.EndPosition = this.EndPosition;
    }

    public void Update()
    {
        if (!this.triggered)
        {
            this.triggered = true;
            StartCoroutine(DestroyLightning());
        }
    }

    private IEnumerator DestroyLightning()
    {
        yield return new WaitForSeconds(this.Duration);
        GameObject.Destroy(this.g);
        GameObject.Destroy(this.gameObject);
    }
}
