using UnityEngine;
using System.Collections;

public class Beamer : Enemy {

	public GameObject		beamInstance;

	private float			rotationSpeed;
	private float			rotationTimer;
	private float			rotationDuration;
	
	private bool			firstRotation = true;

	private bool			fadedIn = false;

	private int				fireDelay = 1;

	// Use this for initialization
	void Start () 
	{
		hitpoints 				= PlayerPrefs.GetInt("BeamerHitpoints", 20);
		rotationSpeed			= PlayerPrefs.GetFloat("BeamerRotationSpeed", 20f);
		rotationDuration		= PlayerPrefs.GetInt("BeamerRotationDuration", 1);
		
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
				fadedIn = true;
			}
		}
		
		if(!fadingIn)
		{
			rotationTimer += Time.deltaTime;
			
			if(rotationTimer < rotationDuration && !stop)
			{
				 transform.Rotate(0,0,rotationSpeed * Time.deltaTime);
			}
			
			if(rotationTimer >= rotationDuration && !stop)
			{
				stop =  true;
				
				StartCoroutine(FireBeam());
				
				if(firstRotation)
				{
					rotationDuration = rotationDuration * 2;
					firstRotation = false;
				}
				
				rotationSpeed = -rotationSpeed;
			}
		}
		
		Die();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("playerBullet") && !fadingIn)
		{
			hitpoints -= other.GetComponent<PlayerShot>().damage;
		}
	}

	private IEnumerator FireBeam()
	{
		beamInstance.GetComponent<Beam>().fire = true;

		yield return new WaitForSeconds(fireDelay);

		beamInstance.GetComponent<Beam>().fire = false;

		rotationTimer = 0;

		stop = false;
	}
}
