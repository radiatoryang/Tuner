using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveMenu : MonoBehaviour {

	public GameObject			buttonClone;

	public Transform[]			buttonLocs = new Transform[13];

	private List<GameObject>	buttonList = new List<GameObject>();

	public Camera				sceneCamera;
	private Vector3				initialCameraLoc;

	private	int					page = 0;
	private int					pageShift = 10;
	private int					waveNumber = 0;

	private bool				createWave = false;
	
	void Start()
	{
		initialCameraLoc = sceneCamera.transform.position;

		for(int i  = 0;i < buttonLocs.Length;i++)
		{
			GameObject buttonInstance;
			buttonInstance = Instantiate(buttonClone, buttonLocs[i].position + new Vector3(waveNumber * pageShift,0,0), Quaternion.identity) as GameObject;

			buttonList.Add(buttonInstance);
		}
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(5,0,80,50),Screen.width + "x" + Screen.height);

		if(GUI.Button(new Rect(Screen.width/2 - 230,565,30,30),"<") && page > 0)
		{
			page--;
		}
		
		if(GUI.Button(new Rect(Screen.width/2 + 200,565,30,30),">"))
		{
			page++;

			if(page > waveNumber)
			{
				createWave = true;
			}
		}
		
		GUI.Label(new Rect(Screen.width/2 - 25,600,50,30), "Wave " + (page + 1));
		
		if(GUI.Button(new Rect(Screen.width/2 - 230,600,75,30),"Main Menu"))
		{
			Application.LoadLevel("mainMenu");
		}
		
		if(GUI.Button(new Rect(Screen.width/2 + 155,600,75,30),"Save"))
		{
			int[] waveArray = new int[buttonList.Count];

			for(int i = 0;i < waveArray.Length;i++)
			{
				if(buttonList[i].GetComponent<WaveButton>().enemyType == "neutral")
				{
					waveArray[i] = 0;
				}
				if(buttonList[i].GetComponent<WaveButton>().enemyType == "volley")
				{
					waveArray[i] = 1;
				}
				if(buttonList[i].GetComponent<WaveButton>().enemyType == "beamer")
				{
					waveArray[i] = 2;
				}
				if(buttonList[i].GetComponent<WaveButton>().enemyType == "charger")
				{
					waveArray[i] = 3;
				}
			}

			PlayerPrefsX.SetIntArray("waveArray",waveArray);
			PlayerPrefs.SetInt("waveCounter",waveNumber + 1);
		}
	}
	
	void Update()
	{
		sceneCamera.transform.position = initialCameraLoc + new Vector3(page * pageShift,0,0);

		if(createWave)
		{
			waveNumber++;

			for(int i  = 0;i < buttonLocs.Length;i++)
			{
				GameObject buttonInstance;
				buttonInstance = Instantiate(buttonClone, buttonLocs[i].position + new Vector3(waveNumber * pageShift,0,0), Quaternion.identity) as GameObject;

				buttonList.Add(buttonInstance);
			}

			createWave = false;
		}
	}
	
	void OnApplicationQuit()
	{
		PlayerPrefs.DeleteAll();
	}
}
