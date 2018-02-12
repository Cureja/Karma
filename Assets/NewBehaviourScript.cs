using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    public AudioClip sMusic = null;
    // Use this for initialization
    void Start () {
        SoundManager.Play(sMusic);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
