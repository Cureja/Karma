using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }
	}
}
