using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultStoneExplosion : MonoBehaviour {

    float timer = 0;
    float destroyDelay = 2f;
    float dangerDelay = 0.5f;


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
        if (timer < destroyDelay)
        {
            timer += Time.deltaTime;
        }
        if (isDangerous == true && timer >= dangerDelay)
        {
            isDangerous = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;

        }
        if (isDestroyed == false && timer >= destroyDelay)
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
