using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {
    public AudioClip sfx = null;
    public AudioClip BirdDeath = null;
    public AudioClip SlugDeath = null;
    // Use this for initialization
    void Start () {
        if (sfx != null) {
            SoundManager.PlaySFX(sfx);
        }
    }
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0.5f, 0, 0);	
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Enemy") {
			Destroy (other.gameObject);
			Destroy (transform.gameObject);
            SoundManager.PlaySFX(SlugDeath);
        } else if (other.gameObject.tag == "Birdman") {
            SoundManager.PlaySFX(BirdDeath);
            Destroy(other.gameObject);
            Destroy(transform.gameObject);
        }
        else if (other.gameObject.tag == "Stage") {
			Destroy (transform.gameObject);
		}
	}
}
