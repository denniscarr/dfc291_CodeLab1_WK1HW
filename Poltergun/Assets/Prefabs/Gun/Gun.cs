using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	// Used to time the interval in between shots
	public float shotTimer = 0.2f;
	public float elapsedTime = 0.0f;
	bool readyToShoot = true;

	// Kickback force
	public float recoilForce = 1f;
	public float bulletForce = 1f;

	public GameObject gunTip;
	public GameObject muzzleFlash;
	public GameObject wallShot;

	public float maxAngularVel = 7f;

	Rigidbody2D rb;
	AudioSource audioSource;


	void Start() {
		rb = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
	}


	void FixedUpdate() {

		// Limit angular velocity
		float angVel = Mathf.Clamp(rb.angularVelocity, -maxAngularVel, maxAngularVel);
		rb.angularVelocity = angVel;
	}


	void Update()
	{
		// See if shot timer has elapsed
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= shotTimer) {
			readyToShoot = true;
		}

		// Get input
		if (Input.GetButtonDown("Shoot")) {
			Shoot();
		}
	}


	void Shoot()
	{
		if (!readyToShoot) return;

		// Play audio
		audioSource.Play();

		// Show muzzle flash
		Instantiate(muzzleFlash, gunTip.transform.position, Quaternion.identity);

		// Fire bullet
		RaycastHit2D hit = Physics2D.Raycast(gunTip.transform.position, gunTip.transform.TransformDirection(Vector2.left), 100f);
		Debug.DrawRay(gunTip.transform.position, gunTip.transform.InverseTransformDirection(Vector2.left), Color.black);
		Instantiate(wallShot, hit.point, Quaternion.identity);

		// Apply force to gun
		Vector2 newVel = transform.InverseTransformDirection(rb.velocity);
		newVel += Vector2.right*recoilForce;
		newVel = transform.TransformDirection(newVel);
		rb.velocity = newVel;

		if (hit.collider == null) return;

		// Apply force to object hit
		if (hit.transform.tag == "Interactive Object")
		{
			hit.rigidbody.AddForceAtPosition(((Vector2) hit.point - (Vector2) gunTip.transform.position)*recoilForce, hit.point, ForceMode2D.Impulse);
			hit.transform.GetComponent<InteractiveObject>().GetShot();
		}

		// Reset timer
		elapsedTime = 0.0f;
		readyToShoot = false;
	}
}
