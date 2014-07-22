using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject		enemyShotClone;	
	public Transform		target;
	
	public GameObject		manager;

	protected int			hitpoints;
	
	protected Vector3		intialPosition;
	protected Vector3		moveTarget;
	protected float			moveRange;
	protected float 		moveSpeed;
	
	protected bool			stop = false;
	protected bool			fadingIn = true;
	protected float			fadeTime = Random.Range(0.3f,0.6f);

	protected int			shotCounter;
	protected float			shotDelay;
	
	// Use this for initialization
	void Start () 
	{
		hitpoints = 10;
		moveSpeed = 5;

		moveRange = 		PlayerPrefs.GetFloat("enemyMoveRange",0.15f);
		shotCounter = 		PlayerPrefs.GetInt("enemyVolleySize", 3);
		shotDelay = 		PlayerPrefs.GetFloat("enemyShotDelay", 0.25f);

		gameObject.renderer.material.color -= new Color(0,0,0,1);
		
		intialPosition = transform.position;

		if(Random.Range(0f,1f) > 0.5)
		{
			moveRange = -moveRange;
		}

		moveTarget = intialPosition + new Vector3(moveRange,0,0);
		

	}
	
	// Update is called once per frame
	void Update () 
	{
		FadeIn();
			
		if(!fadingIn)
		{
			transform.position = Vector3.Lerp(transform.position, moveTarget, moveSpeed * Time.deltaTime);
		}
		
		if(transform.position == moveTarget && !fadingIn)
		{	
			if(moveTarget == intialPosition + new Vector3(moveRange,0,0))
			{
				moveTarget = intialPosition - new Vector3(moveRange,0,0);
				stop = true;
			}
			else
			{
				moveTarget = intialPosition + new Vector3(moveRange,0,0);
				stop = true;
			}
			
			StartCoroutine(fireVolley());
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

	private IEnumerator fireVolley()
	{
		for(int i = 0;i < shotCounter;)
		{
			if(target.position.y < transform.position.y)
			{
				GameObject shotInstance;
				
				shotInstance = Instantiate(enemyShotClone, transform.position, Quaternion.identity) as GameObject;
				shotInstance.GetComponent<VolleyShot>().moveTowards(target.position);
			}
						
			yield return new WaitForSeconds(shotDelay);

			i++;

			if(i == shotCounter)
			{
				stop = false;
			}
		}
	}

	protected void FadeIn()
	{
		if(gameObject.renderer.material.color.a < 1)
		{
			gameObject.renderer.material.color += new Color(0,0,0,fadeTime * Time.deltaTime);
		}
		else
		{
			fadingIn = false;
		}
	}

	protected void Die()
	{
		if(hitpoints <= 0)
		{
			manager.GetComponent<WaveManager>().enemyDead(gameObject);
			Destroy(gameObject);
		}
	}
}
