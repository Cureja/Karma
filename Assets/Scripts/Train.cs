using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {
	public GameObject slug;
	public float cooldown;
	public int numSlugsSpawned;
	private float timeCounter;
	private float nextTime;

	private Rigidbody2D physics;
	private int numOfStampedes;
	public int secondsBetweenWaves;

    public GameObject locator11;
    public GameObject locator12;

    public GameObject locator21;
    public GameObject locator22;

    private Vector2 pos11;
    private Vector2 pos12;

    private Vector2 pos21;
    private Vector2 pos22;

    private Vector3 vel;

    private bool moving;
    private bool startMoving;
    private int selectedRoute;

	// Use this for initialization
	void Start () {
        vel = new Vector3();

        pos11 = locator11.transform.position;
        pos12 = locator12.transform.position;
        pos21 = locator21.transform.position;
        pos22 = locator22.transform.position;

        moving = false;
        startMoving = false;

        secondsBetweenWaves = 1;

        StartCoroutine(Wait());
    }

    void Update() {

		vel = new Vector3 ();

        if (moving)
		{
            if (selectedRoute == 1)
            {
				transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(pos11.y, pos11.x));
                if (startMoving)
                {
                    startMoving = false;
                    transform.position = pos11;
                }

                vel = (pos12 - pos11).normalized*20;

                if (Mathf.Abs(transform.position.x - pos12.x) < 1 && Mathf.Abs(transform.position.y - pos12.y) < 1)
                {
                    moving = false;
                }
            }
            else if (selectedRoute == 2)
            {
				transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(pos21.y, pos21.x));
                if (startMoving)
                {
                    startMoving = false;
                    transform.position = pos21;
                }

                vel = (pos22 - pos21).normalized*20;

                if (Mathf.Abs(transform.position.x - pos22.x) < 1 && Mathf.Abs(transform.position.y - pos22.y) < 1)
                {
                    moving = false;
                }
            }

            transform.position += vel * Time.deltaTime;
        }

		spawnSlugs (numSlugsSpawned);
    }

	void spawnSlugs(int n) {
		timeCounter += Time.deltaTime;
		if (timeCounter > nextTime && transform.position.y < 20 && transform.position.y > -59) {
			nextTime = timeCounter + Random.Range (5.0f,10.0f);
			for (int i = 0; i < n; i++) {
				Instantiate (slug, transform.position, transform.rotation);
			}
		}
	}

    IEnumerator Wait() {
		yield return new WaitForSeconds(Random.Range (10.0f,20.0f));
		//print ("FSSDFSJDF");
        if (!moving && !startMoving)
        {   
			selectedRoute = 1;
			//print (moving +" stuff "+startMoving);
            moving = true;
            startMoving = true;
        }

        StartCoroutine(Wait());

        // stampede ();
    }
}