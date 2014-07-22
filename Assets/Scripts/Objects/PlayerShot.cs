using UnityEngine;
using System.Collections;

public class PlayerShot : MonoBehaviour {
	
	public GameObject	playerConnection;
	
	public int			damage;

	private Vector3		moveVector;
	public	float		moveSpeed;

	// Use this for initialization
	void Start () 
	{
		damage 			= PlayerPrefs.GetInt("playerShotDamage", 5);
		moveSpeed		= PlayerPrefs.GetFloat("playerShotMoveSpeed", 10f);

		moveVector = new Vector3(0,moveSpeed,0);
		rigidbody.AddForce(moveVector,ForceMode.VelocityChange);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(transform.position.y > 6.5f)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Enemy"))
		{
			Destroy(gameObject);
		}
	}
}
