using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {

	public GameObject			volleyClone;
	public GameObject			beamerClone;
	public GameObject			chargerClone;

	public List<GameObject>		enemyList = new List<GameObject>();

	public GameObject			player;
	
	public GameObject			manager;

	private int					waveCounter;

	private int[][]				enemyWaves;
	
	private int[][]				defaultWaves = new int[][]
								{
									new int[] {0,0,0,0,0,0,0,2,0,0,0,0,0},
									new int[] {0,0,0,0,0,1,0,1,0,1,0,0,0},
									new int[] {1,0,0,0,1,0,1,0,1,0,0,0,0},
									new int[] {0,0,0,0,0,0,0,2,0,0,0,0,0},
									new int[] {0,2,1,2,0,1,0,0,0,1,0,0,0},
									new int[] {2,0,0,0,2,0,0,2,0,0,0,0,0},
									new int[] {0,0,0,0,0,0,0,3,0,0,0,0,0},
									new int[] {1,0,1,0,1,0,0,3,0,0,0,0,0},
									new int[] {3,0,0,0,3,0,0,3,0,0,0,0,0},
									new int[] {0,2,0,2,0,3,0,0,0,3,0,0,0},
									new int[] {1,2,1,2,1,3,0,1,0,3,0,0,2}
								};

	public Transform[]			spawnLocs = new Transform[13];

	private bool				launchingWave = false;

	// Use this for initialization
	void Start () 
	{
		waveCounter = PlayerPrefs.GetInt("waveCounter", 11);

		enemyWaves = new int[waveCounter][];

		if(PlayerPrefsX.GetIntArray("waveArray").Length <= 0)
		{
			for(int i = 0;i < enemyWaves.Length;i++)
			{
				enemyWaves[i] = defaultWaves[i];
			}
		}
		else
		{
			int[] waveArray = PlayerPrefsX.GetIntArray("waveArray");
			int[][] tempJagged = new int[waveArray.Length/13][];

			int waveMark = 0;

			for(int i = 0;i < tempJagged.Length;i++)
			{
				int[] tempArray = new int[13];

				for(int j = 0;j < tempArray.Length;j++)
				{
					tempArray[j] = waveArray[waveMark];
					waveMark++;
				}

				tempJagged[i] = tempArray;
			}

			for(int k = 0;k < enemyWaves.Length;k++)
			{
				enemyWaves[k] = tempJagged[k];
			}
		}

		waveCounter = 0;

		launchingWave = true;

		StartCoroutine(launchWave());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!launchingWave && enemyList.Count <= 0 && waveCounter < enemyWaves.GetUpperBound(0) + 1)
		{
			launchingWave = true;

			StartCoroutine(launchWave());
		}
		else if(enemyList.Count == 0 && waveCounter == enemyWaves.GetUpperBound(0) + 1)
		{
			manager.GetComponent<GameManager>().playerWins();
			Destroy(gameObject);
		}
	}

	private IEnumerator launchWave()
	{
		yield return new WaitForSeconds(1.5f);

		for(int i = 0;i < spawnLocs.Length;i++)
		{
			switch(enemyWaves[waveCounter][i])
			{
			case 0:
				break;
			case 1:
				GameObject volleyInstance;
				
				volleyInstance = Instantiate(volleyClone,spawnLocs[i].position,Quaternion.identity) as GameObject;
				volleyInstance.GetComponent<Enemy>().target = player.transform;
				volleyInstance.GetComponent<Enemy>().manager = gameObject;
				
				enemyList.Add(volleyInstance);
				
				break;

			case 2:
				GameObject beamerInstance;
				
				beamerInstance = Instantiate(beamerClone,spawnLocs[i].position,Quaternion.identity) as GameObject;
				beamerInstance.GetComponent<Beamer>().target = player.transform;
				beamerInstance.GetComponent<Beamer>().manager = gameObject;
				
				enemyList.Add(beamerInstance);
				
				break;

			case 3:
				GameObject chargerInstance;
				
				chargerInstance = Instantiate(chargerClone,spawnLocs[i].position,Quaternion.identity) as GameObject;
				chargerInstance.GetComponent<Charger>().target = player.transform;
				chargerInstance.GetComponent<Charger>().manager = gameObject;
				
				enemyList.Add(chargerInstance);
				
				break;
			}
		}

		waveCounter++;
		launchingWave = false;
	}

	public void enemyDead(GameObject enemy)
	{
		enemyList.Remove(enemy);
	}
}
