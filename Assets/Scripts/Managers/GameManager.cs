using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject				playerClone;
	public GameObject				playerInstance;
	
	private bool					playerAlive = true;
	private bool					victorious = false;

	public GameObject				waveManager;
	
	private bool					paused;
	public GameObject				screenConnection;
		
	// Use this for initialization
	void Start () 
	{
		playerInstance = Instantiate(playerClone, new Vector3(0,-3,0),Quaternion.identity) as GameObject;
		playerInstance.GetComponent<Player>().manager = gameObject;

		waveManager = Instantiate(waveManager,Vector3.zero, Quaternion.identity) as GameObject;
		waveManager.GetComponent<WaveManager>().player = playerInstance;
		waveManager.GetComponent<WaveManager>().manager = gameObject;
		
		paused = false;
		Time.timeScale = 1;


	}
	
	// Update is called once per frame
	void Update () 
	{
	
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(paused && playerAlive && !victorious)
			{
				screenConnection.transform.position = new Vector3(999,999,999);
				paused = false;
				
				if(playerAlive)
				{
					playerInstance.GetComponent<Player>().pause();	
				}
				
				Time.timeScale = 1;
			}
			else if(!victorious && playerAlive)
			{
				screenConnection.transform.position = new Vector3(0,0,-3);
				paused = true;
				
				if(playerAlive)
				{
					playerInstance.GetComponent<Player>().pause();
				}
						
				Time.timeScale = 0;
			}
		}

		if(Input.GetKeyUp(KeyCode.Q))  
		{
			Application.Quit();
		}
		
		if(!playerAlive)
		{
			screenConnection.transform.position = new Vector3(0,0,-3);	
		}

		if(victorious)
		{
			screenConnection.transform.position = new Vector3(0,0,-3);
		}
		
		if(Input.GetKeyDown(KeyCode.R))
		{
			Application.LoadLevel("scene01");
		}
	}
	
	void OnGUI()
	{
		if(!playerAlive && !victorious)
		{
			GUI.Label(new Rect(10,5,75,50),"Game Over");

			if(GUI.Button(new Rect(Screen.width/2 - 230,600,100,30),"Main Menu"))
			{
				Application.LoadLevel("mainMenu");
			}
			
			if(GUI.Button(new Rect(Screen.width/2 + 130,600,100,30),"Restart"))
			{
				Application.LoadLevel("scene01");
			}
		}
		
		if(paused)
		{
			GUI.Label(new Rect(Screen.width/2 - (75/2),5,75,30),"Paused");
			
			if(GUI.Button(new Rect(Screen.width/2 - 230,600,75,30),"Main Menu"))
			{
				Application.LoadLevel("mainMenu");
			}
		}

		if(victorious)
		{
			GUI.Label(new Rect(10,5,75,50),"You Win!");
			
			if(GUI.Button(new Rect(Screen.width/2 - 230,600,100,30),"Main Menu"))
			{
				Application.LoadLevel("mainMenu");
			}
			
			if(GUI.Button(new Rect(Screen.width/2 + 130,600,100,30),"Restart"))
			{
				Application.LoadLevel("scene01");
			}
		}
	}
	
	public void playerDead()
	{
		playerAlive = false;
	}
	
	public void playerWins()
	{
		victorious = true;
		playerInstance.GetComponent<Player>().victorious = true;
	}

	void OnApplicationQuit()
	{
		PlayerPrefs.DeleteAll();
	}
}
