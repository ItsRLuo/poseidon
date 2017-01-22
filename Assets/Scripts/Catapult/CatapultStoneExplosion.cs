using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultStoneExplosion : MonoBehaviour {

    float destroyTimer = 0;
    float destroyDelay = 1f;

    bool isDangerous = true;
    bool isDestroyed = false;

    public AudioClip explosion;
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (explosion != null && audioSource != null)
        {
            audioSource.clip = explosion;
            audioSource.Play();
        }
    }

    private void Update()
    {
        if (destroyTimer < destroyDelay * 2)
        {
            destroyTimer += Time.deltaTime;
        }
        if (isDangerous == true && destroyTimer >= destroyDelay)
        {
            isDangerous = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;

        }
        if (isDestroyed == false && destroyTimer >= destroyDelay * 2)
        {
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        BoatController boatController = other.gameObject.GetComponent<BoatController>();
        if (boatController != null)
        {
            boatController.Explode(transform.position);
        }
    }

}
