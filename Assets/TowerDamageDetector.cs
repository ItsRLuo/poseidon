using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDamageDetector : MonoBehaviour
{
    public GameplayScene GameplayScene;
    public GameObject ExplosionPrefab;

    public void Start()
    { }

    public void Update()
    { }

	IEnumerator AnimateExplosion(BoatController bc) {
		yield return new WaitForSeconds(1.2f);
		var explosion = GameObject.Instantiate<GameObject>(ExplosionPrefab);
        explosion.transform.position = bc.transform.position;
        this.GameplayScene.AddDamage(bc.Points / 500.0f);

        CameraShake.Shake(0.25f, bc.Points / 200.0f);
	}

    public void OnTriggerEnter(Collider collider)
    {
        BoatController bc = collider.gameObject.GetComponent<BoatController>();

        if (bc != null && !bc.Sinking) {
			StartCoroutine(AnimateExplosion(bc));
        }
    }
}
