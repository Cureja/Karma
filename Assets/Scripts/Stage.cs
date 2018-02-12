using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {
	public int numEnemies;

	public int enemiesPerWave = 5;
	public float secondsBetweenWaves = 10f;
	public GameObject enemy;
    public AudioClip bgMusic = null;

    // Use this for initialization
    void Start () {
		numEnemies = 0;
		spawnEnemies (enemiesPerWave);
        if (bgMusic != null) {
            SoundManager.Play(bgMusic,true);
            SoundManager.SetVolume(.75f);
        }
    }

	//Spawn n amount of enemies
	void spawnEnemies(int n) {
		Vector3 zOffset = new Vector3 (0, 0, 5);
		for (int i = 0; i < n; i++) {
			Instantiate (enemy, transform.position - zOffset, transform.rotation);
			numEnemies++;
		}
		StartCoroutine (Example());
	}

	IEnumerator Example() {
		yield return new WaitForSeconds (secondsBetweenWaves);
		spawnEnemies (enemiesPerWave);
	}
}
