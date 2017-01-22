using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDamageDetector : MonoBehaviour
{
    public GameplayScene GameplayScene;
    public GameObject ExplosionPrefab;
    private SoundManager soundManager;

    public void Start()
    {
        soundManager = GameObject.FindObjectOfType<SoundManager>();
    }

    public void Update()
    { }

	IEnumerator AnimateExplosion(BoatController bc) {

		yield return new WaitForSeconds(1.2f);

        if (bc != null && bc.Sinking) yield break;

		var explosion = GameObject.Instantiate<GameObject>(ExplosionPrefab);
        explosion.transform.position = bc.transform.position;
        this.GameplayScene.AddDamage(bc.Points / 500.0f);
        CameraShake.Shake(0.25f, bc.Points / 200.0f);

        soundManager.PlayTakeDamage();
    }

    public void OnTriggerEnter(Collider collider)
    {
        BoatController bc = collider.gameObject.GetComponent<BoatController>();

        if (bc != null && !bc.Sinking) {
			StartCoroutine(AnimateExplosion(bc));
        }
    }
}
