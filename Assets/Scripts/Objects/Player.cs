using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public GameObject			manager;
	public Transform			barrel;
	
	private int					hitpoints = 1;
	public int					maxHitPoints;
	
	private Vector3				moveVector;
	public float				moveSpeed;
	public float				maxMoveSpeed = 5;	
	
	public GameObject			shotClone;
	private float				shotTimer;
	private float				shotDelay;
	
	private bool				paused = false;
	public bool					victorious = false;
	
	// Use this for initialization
	void Start () 
	{
		moveSpeed 		= PlayerPrefs.GetFloat("playerMoveSpeed",1.5f);
		maxHitPoints 	= PlayerPrefs.GetInt("playerHitpoints",1);
		shotDelay		= PlayerPrefs.GetFloat("playerShotDelay", 0.1f);

		hitpoints = maxHitPoints;
		
		shotTimer = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!paused)
		{
			if(Input.GetKey(KeyCode.RightArrow) && transform.position.x <= 3.4f)
			{
				moveVector += new Vector3(moveSpeed,0,0);
			}

			if(Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= -3.4f)
			{
				moveVector -= new Vector3(moveSpeed,0,0);				
			}
			
			if(Input.GetKeyDown(KeyCode.Z) && Time.realtimeSinceStartup > shotTimer + shotDelay)
			{
				GameObject shotInstance;
				Vector3 shotLoc = barrel.position + new Vector3(0,0,0.3f); 
				
				shotInstance = Instantiate(shotClone, shotLoc, Quaternion.identity) as GameObject;
				shotInstance.GetComponent<PlayerShot>().playerConnection = gameObject;
				//shotList.Add(shotInstance);
				
				shotTimer = Time.realtimeSinceStartup;
			}
		}

		if(hitpoints <= 0)
		{
			manager.GetComponent<GameManager>().playerDead();
			
			paused = true;
			transform.position = new Vector3(999,999,999);
		}
	}

	void FixedUpdate()
	{
		if(!paused)
		{
			if(rigidbody.velocity.magnitude > maxMoveSpeed)
			{
				rigidbody.velocity = rigidbody.velocity.normalized * maxMoveSpeed;
			}
			else
			{
				rigidbody.AddForce(moveVector,ForceMode.VelocityChange);
			}
			
			if(transform.position.x >= 3.4f)
			{
				transform.position = new Vector3(3.4f,transform.position.y,transform.position.z);
			}
			if(transform.position.x <= -3.4f)
			{
				transform.position = new Vector3(-3.4f,transform.position.y,transform.position.z);
			}
			
			moveVector = new Vector3(0,0,0);
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("enemyShot") && !victorious)
		{
			hitpoints -= other.GetComponent<VolleyShot>().damage;
		}

		if(other.gameObject.CompareTag("Enemy Beam") && !victorious)
		{
			hitpoints -= other.GetComponent<Beam>().damage;
		}

		if(other.gameObject.CompareTag("Enemy") && !victorious)
		{
			hitpoints -= maxHitPoints;
		}
	}

	public void pause()
	{
		if(paused)
		{
			paused = false;
		}
		else
		{
			paused = true;
		}
	}
}
