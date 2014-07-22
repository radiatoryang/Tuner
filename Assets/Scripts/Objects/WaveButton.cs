using UnityEngine;
using System.Collections;

public class WaveButton : MonoBehaviour {

	public Material			neutral;
	public Material			volley;
	public Material			beamer;
	public Material			charger;

	public GameObject		manager;

	public string			enemyType = "neutral";

	private bool			clicked = false;

	// Use this for initialization
	void Start () 
	{
		renderer.material = neutral;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(clicked)
		{
			switch(enemyType)
			{
			case "neutral": 	renderer.material = volley; enemyType = "volley"; clicked = false; break;

			case "volley": 		renderer.material = beamer; enemyType = "beamer"; clicked = false; break;

			case "beamer": 		renderer.material = charger; enemyType = "charger"; clicked = false; break;

			case "charger": 	renderer.material = neutral; enemyType = "neutral"; clicked = false; break;
			}
		}
	}
	
	public void Clicked()
	{
		clicked = true;
	}
}
