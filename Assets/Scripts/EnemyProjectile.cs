using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {
	public AudioClip sfx = null;
	// Use this for initialization
	void Start () {
		if (sfx != null) {
			SoundManager.PlaySFX(sfx);
		}
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (0.1f, 0, 0);	
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Stage") {
			Destroy (transform.gameObject);
		}
	}
}
