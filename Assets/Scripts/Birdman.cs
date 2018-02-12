using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birdman : MonoBehaviour {
	public float cooldown;
	public GameObject projectile;
	public GameObject target;
	Rigidbody2D physics;

	public float followPeriod = 1.5f;
	float seekTime;
	public float randomDirectionTimer = 2f;
	public float randomVariance = 1f;
	float randomTime;

	private float timeCounter;
	private float nextTime;

	void Start () {
		target = GameObject.Find ("Player");
		physics = gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
		physics.drag = 10;
		seekTime = 0;
		randomTime = 0;
	}

	void Update()
	{
		shootProjectile ();
		//If there are no objects obstructing the view of the Enemy to its target, move towards the target
		if (Physics2D.Linecast (transform.position, target.transform.position, 8).rigidbody == null) {
			seekTime = followPeriod;
			moveTowardsPlayer ();

			//Buffer period for the Enemy after it loses vision of the target
		} else if (seekTime > 0) {
			seekTime -= Time.deltaTime;
			moveTowardsPlayer ();

			//Once the buffer period has ended, make sure that the current idle movement has had sufficient time to move the Enemy
		} else if (randomTime > 0) {
			randomTime -= Time.deltaTime;

			//Move for a random amount of [0, 2] seconds in a random direction of [-360, 360] degrees
		} else {
			randomTime = randomDirectionTimer + randomVariance*Random.Range(-1,1);
			idleMovement ();
		}
			
		transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(physics.velocity.y, physics.velocity.x));
	}

	void moveTowardsPlayer() {
		Vector2 push = target.transform.position - transform.position;
		physics.AddForce(push.normalized * 20);
	}

	void idleMovement() {
		Vector2 push = new Vector2 (0, 7).Rotate(Mathf.Atan2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
		physics.AddForce(push);
	}

	void shootProjectile() {
		timeCounter += Time.deltaTime;
		if (timeCounter > nextTime) {
			nextTime = timeCounter + cooldown;
			Instantiate (projectile, transform.position, transform.rotation);
		}
	}
}
