using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray 			mouseRay = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit		mouseHit;
			
			if(Physics.Raycast(mouseRay, out mouseHit))
			{
				if(mouseHit.collider.CompareTag("button"))
				{
					mouseHit.collider.gameObject.GetComponent<WaveButton>().Clicked();
				}
			}
		}
	}
}
