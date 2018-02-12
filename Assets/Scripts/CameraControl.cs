using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    [Range(0, 1)]
    public float damping;
    public GameObject player;
    private Vector3 val;
    private Vector3 target;


    // Use this for initialization
    void Start () {
        val = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        target.x = player.transform.position.x;
        target.y = player.transform.position.y;
        target.z = player.transform.position.z - 10;

        Vector3 pos = transform.position;

        val.x = target.x - pos.x;
        val.y = target.y - pos.y;
        val.z = target.z - pos.z;

        pos += val * damping;
        transform.position = pos;
	}
}
