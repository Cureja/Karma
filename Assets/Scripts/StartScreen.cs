using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
