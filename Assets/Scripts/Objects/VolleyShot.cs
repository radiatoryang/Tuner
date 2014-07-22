using UnityEngine;
using System.Collections;

public class VolleyShot : MonoBehaviour {

	private Vector3			moveVector;
	public float			moveSpeed;
	
	public int				damage;
	
	// Use this for initialization
	void Awake () 
	{
		moveSpeed		= PlayerPrefs.GetFloat("VolleyShotSpeed", 3);
		damage			= PlayerPrefs.GetInt("VolleyShotDamage", 1);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(transform.position.y < -10)
		{
			Destroy(gameObject);
		}

		transform.Rotate(0, 0, 800 * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
	}
	
	public void moveTowards(Vector3 target)
	{
		rigidbody.velocity = (target - transform.position).normalized * moveSpeed;
	}
}
