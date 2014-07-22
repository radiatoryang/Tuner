using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {

	public bool 	fire = false;

	public int		damage;

	// Use this for initialization
	void Start () 
	{
		damage = PlayerPrefs.GetInt("BeamerDamage", 10);

		gameObject.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(fire)
		{
			gameObject.renderer.enabled = true;
			gameObject.collider.enabled = true;
			gameObject.tag = "Enemy Beam";
		}
		else
		{
			gameObject.renderer.enabled = false;
			gameObject.collider.enabled = false;
			gameObject.tag = "Untagged";
		}
	}
}
