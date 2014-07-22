using UnityEngine;
using System.Collections;

public class Charger : Enemy {

	private bool			charging = false;
	private int 			chargeTimer;
	private float			chargeDelay;
	private float			chargeWait = 1;

	private float			chargeSpeed;
	private float 			trackSpeed;
	private float			maxVelocity = 0.4f;

	private Vector3			moveVector;

	private bool 			fadedIn = false;

	// Use this for initialization
	void Start () 
	{
		hitpoints = 			PlayerPrefs.GetInt("ChargerHitpoints", 40);

		chargeDelay = 			PlayerPrefs.GetFloat("ChargerChargeDelay", 150);
		chargeSpeed = 			PlayerPrefs.GetFloat("ChargerChargeSpeed", 15f);
		trackSpeed = 			PlayerPrefs.GetFloat("ChargerTrackSpeed", 0.4f);
		
		rigidbody.drag =		PlayerPrefs.GetFloat("ChargerDrag", 12); 		
		
		gameObject.renderer.material.color -= new Color(0,0,0,1);
	}
	
	// Update is called once per frame
	void Update () 
	{
		FadeIn();

		if(!fadedIn)
		{
			if(!fadingIn)
			{
				chargeTimer = 0;
				fadedIn = true;
			}
		}

		if(!fadingIn)
		{
			if(!charging)
			{
				if(target.transform.position.x > transform.position.x)
				{
					moveVector += new Vector3(trackSpeed,0,0);
				}
				if(target.transform.position.x < transform.position.x)
				{
					moveVector += new Vector3(-trackSpeed,0,0);
				}
			}
			
			if(chargeTimer >= chargeDelay)
			{
				moveVector = Vector3.zero;
				charging = true;

				StartCoroutine(Charge());
			}
		}
		
		Die();

		if(transform.position.y < -10)
		{
			manager.GetComponent<WaveManager>().enemyDead(gameObject);
			Destroy(gameObject);
		}
	}

	void FixedUpdate()
	{
		if(rigidbody.velocity.magnitude > maxVelocity && !charging)
		{
			rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
		}
		else
		{
			rigidbody.AddForce(moveVector,ForceMode.Impulse);
		}

		chargeTimer++;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("playerBullet") && !fadingIn)
		{
			hitpoints -= other.GetComponent<PlayerShot>().damage;
		}
	}

	private IEnumerator Charge()
	{
		yield return new WaitForSeconds(chargeWait);

		moveVector += new Vector3(0,-chargeSpeed,0);
	}
}
