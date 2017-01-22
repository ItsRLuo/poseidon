using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDamageDetector : MonoBehaviour
{
    public GameplayScene GameplayScene;

    public void Start()
    { }

    public void Update()
    { }

    public void OnTriggerEnter(Collider collider)
    {
        BoatController bc = collider.gameObject.GetComponent<BoatController>();
        if (bc != null && !bc.Sinking)
        {
            this.GameplayScene.AddDamage(bc.Points / 500.0f);
        }
    }
}
