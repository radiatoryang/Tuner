using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public string		game;
	public string		debug;
	public string		wave;
	
	private bool		showTune = false;
	private bool		showWave = false;

	void Start()
	{

	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel(game);
		}

		if(Input.GetKeyUp(KeyCode.Q))  
		{
			Application.Quit();
		}
		
		if(Input.GetKeyDown(KeyCode.T))
		{
			showTune = true;
		}
		
		if(Input.GetKeyDown(KeyCode.W))
		{
			showWave = true;
		}
	}

	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width/2 - (85/2),15,85,30),"Veranderzeug");
		GUI.Label(new Rect(0,0,80,50),Screen.width + "x" + Screen.height);
		
		
		if(GUI.Button(new Rect(Screen.width/2 - 230,600,75,30),"Start"))
		{
			Application.LoadLevel(game);
		}
		
		if(showWave)
		{
			if(GUI.Button(new Rect(Screen.width/2 + 155,560,75,30),"Waves"))
			{
				Application.LoadLevel(wave);
			}	
		}
		
		if(showTune)
		{
			if(GUI.Button(new Rect(Screen.width/2 + 155,600,75,30),"Tuning"))
			{
				Application.LoadLevel(debug);
			}
		}
	}

	void OnApplicationQuit()
	{
		PlayerPrefs.DeleteAll();
	}
}
