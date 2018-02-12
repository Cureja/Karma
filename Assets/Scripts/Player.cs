using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	//public Stage stage;
    private Rigidbody2D physics;

	bool isRightArm = true;

	public Sprite idleFrame;
	public Sprite leftFrame;
	public Sprite rightFrame;
	public Sprite shotgunFrame;

	public GameObject projectile;

	public int health;

    public float cooldown;

	public float sgCooldown;

    private float timeCounter;
    private float nextTime;

	private int shotgunAngle = 30;

    public AudioClip ProjectileHit = null;

    public AudioClip SlugHit = null;

    public AudioClip BirdHit = null;

    // Use this for initialization
    void Start () {
        physics = gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        physics.drag = 20;
    }

    void Update()
    {
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.forward, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        // then find the point along that ray that meets that distance.  This will be the point
        // to look at.
        float hitdist = 0.0f;
		timeCounter += Time.deltaTime;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            float angle = Mathf.Rad2Deg*Mathf.Atan2(targetPoint.y - transform.position.y,targetPoint.x - transform.position.x);
			if (timeCounter - nextTime + cooldown > .2) {
				gameObject.GetComponent<SpriteRenderer> ().sprite = idleFrame;
			} 
            physics.rotation = angle;
            if (Input.GetMouseButton(0))
            {
				if (timeCounter > nextTime) {
                    nextTime = timeCounter + cooldown;
					Vector2 recoil = new Vector2 (0, 1000).Rotate (angle + 90);
					physics.AddForce (recoil);

					if (isRightArm) {
						gameObject.GetComponent<SpriteRenderer> ().sprite = rightFrame;
						isRightArm = false;
					} else {
						gameObject.GetComponent<SpriteRenderer> ().sprite = leftFrame;
						isRightArm = true;
					}

					Instantiate (projectile, transform.position, transform.rotation);
				}
            }
			else if (Input.GetMouseButton(1))
			{
				if (timeCounter > nextTime)
				{
                    gameObject.GetComponent<SpriteRenderer> ().sprite = shotgunFrame;
					nextTime = timeCounter + sgCooldown;
					Vector2 recoil = new Vector2(0, 2000).Rotate(angle + 90);
					physics.AddForce(recoil);

					Quaternion r = transform.rotation;

					var a = new Vector2 (1, 0).Rotate (physics.rotation - shotgunAngle);
					var b = new Vector2 (1, 0).Rotate (physics.rotation);
					var c = new Vector2 (1, 0).Rotate (physics.rotation + shotgunAngle);

					Instantiate (projectile, transform.position + new Vector3(a.x,a.y,0), Quaternion.Euler(r.eulerAngles.x, r.eulerAngles.y, r.eulerAngles.z - shotgunAngle));
					Instantiate (projectile, transform.position + new Vector3(b.x,b.y,0), transform.rotation);
					Instantiate (projectile, transform.position + new Vector3(c.x,c.y,0), Quaternion.Euler(r.eulerAngles.x, r.eulerAngles.y, r.eulerAngles.z + shotgunAngle));

				}
			}
            
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyProjectile" || other.gameObject.tag == "Birdman") {
            if(other.gameObject.tag == "EnemyProjectile")
            {
                SoundManager.PlaySFX(ProjectileHit);
            } else if(other.gameObject.tag == "Enemy") {
                SoundManager.PlaySFX(SlugHit);
            } else if (other.gameObject.tag == "Birdman")
            {
                SoundManager.PlaySFX(BirdHit);
            }

            Destroy (other.gameObject);
			//stage.numEnemies--;
			health--;

			if (health <= 0) {
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
			}
		}
	}


}