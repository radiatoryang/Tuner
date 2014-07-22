using UnityEngine;
using System.Collections;

public class TuneMenu : MonoBehaviour {
	
	private float		playerShotDelay;
	private float		playerMoveSpeed;
	private int			playerHitpoints;
	private float		playerShotSpeed;
	private int			playerShotDamage;

	private float		enemyMoveRange;
	private int			enemyVolleySize;
	private float		enemyShotDelay;
	private float		enemyShotMoveSpeed;
	private int			enemyShotDamage;

	private int			beamerHitpoints;
	private float		beamerRotSpeed;
	private int 		beamerRotDuration;
	private int			beamerDamage;
	
	private int 		chargerHitpoints;
	private float		chargerChargeDelay;
	private float		chargerChargeSpeed;
	private float		chargerTrackSpeed;
	private float		chargerDrag;

	private	int			page = 1;

	void Start()
	{
		playerShotDelay			= PlayerPrefs.GetFloat("playerShotDelay", 0.1f);
		playerMoveSpeed 		= PlayerPrefs.GetFloat("playerMoveSpeed", 1.5f);
		playerHitpoints			= PlayerPrefs.GetInt("playerHitpoints", 1);
		playerShotSpeed			= PlayerPrefs.GetFloat("playerShotMoveSpeed", 10f);
		playerShotDamage		= PlayerPrefs.GetInt("playerShotDamage", 5);
		
		enemyMoveRange			= PlayerPrefs.GetFloat("enemyMoveRange", 0.15f);
		enemyVolleySize			= PlayerPrefs.GetInt("enemyVolleySize", 3);
		enemyShotDelay			= PlayerPrefs.GetFloat("enemyShotDelay", 0.25f);
		enemyShotMoveSpeed		= PlayerPrefs.GetFloat("VolleyShotSpeed", 3f);
		enemyShotDamage			= PlayerPrefs.GetInt("VolleyShotDamage", 1);

		beamerHitpoints 		= PlayerPrefs.GetInt("BeamerHitpoints", 20);
		beamerRotSpeed 			= PlayerPrefs.GetFloat("BeamerRotationSpeed", 20);
		beamerRotDuration		= PlayerPrefs.GetInt("BeamerRotationDuration", 1);
		beamerDamage 			= PlayerPrefs.GetInt("BeamerDamage", 10);
		
		chargerHitpoints		= PlayerPrefs.GetInt("ChargerHitpoints", 40);
		chargerChargeDelay 		= PlayerPrefs.GetFloat("ChargerChargeDelay", 150);
		chargerChargeSpeed 		= PlayerPrefs.GetFloat("ChargerChargeSpeed", 15f);
		chargerTrackSpeed 		= PlayerPrefs.GetFloat("ChargerTrackSpeed", 0.4f);
		chargerDrag				= PlayerPrefs.GetFloat("ChargerDrag", 12);
		
	}

	void OnGUI()
	{
		GUI.Label(new Rect(5,0,80,50),Screen.width + "x" + Screen.height);
		
		switch(page)
		{
		case 1:

			GUI.Label(new Rect(Screen.width/2 - 25,5,50,30), "Player");
			
			//PLAYER SHOT DELAY (0.1)
			GUI.Label(new Rect(Screen.width/2 - 230,30,120,30),"Shot Delay");
			playerShotDelay = GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,50,75,15),playerShotDelay,0,1);
			GUI.Label(new Rect(Screen.width/2 - 150,45,20,50), string.Format("{0:0.0}", playerShotDelay));
			
			//PLAYER MOVE SPEED (1.5)
			GUI.Label(new Rect(Screen.width/2 - 230,60,150,30),"Move Speed");
			playerMoveSpeed = GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,80,75,15),playerMoveSpeed,0,5f);
			GUI.Label(new Rect(Screen.width/2 - 150,75,30,50), string.Format("{0:0.0}", playerMoveSpeed));
			
			//PLAYER HITPOINTS (1)
			GUI.Label(new Rect(Screen.width/2 - 230,90,150,30),"Hitpoints");
			playerHitpoints = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,110,75,15),playerHitpoints,0,25));
			GUI.Label(new Rect(Screen.width/2 - 150,105,40,50), playerHitpoints.ToString());

			//PLAYER SHOT MOVE SPEED (10)
			GUI.Label(new Rect(Screen.width/2 - 230,120,150,30),"Shot Move Speed");
			playerShotSpeed = GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,140,75,15),playerShotSpeed,0,20f);
			GUI.Label(new Rect(Screen.width/2 - 150,135,30,50), string.Format("{0:0.0}", playerShotSpeed));

			//PLAYER SHOT DAMAGE (5)
			GUI.Label(new Rect(Screen.width/2 - 230,150,150,30),"Shot Damage");
			playerShotDamage = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,170,75,15),playerShotDamage,0,100));
			GUI.Label(new Rect(Screen.width/2 - 150,165,40,50), playerShotDamage.ToString());

			break;
			
		case 2:
			
			GUI.Label(new Rect(Screen.width/2 - 25,4,50,30), "Volley");
			
			//ENEMY MOVE RANGE (0.15)
			GUI.Label(new Rect(Screen.width/2 - 230,30,170,30),"Move Range");
			enemyMoveRange = GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,50,75,15),enemyMoveRange,0,0.5f);
			GUI.Label(new Rect(Screen.width/2 - 150,45,30,50), string.Format("{0:0.0}", enemyMoveRange));
						
			//ENEMY VOLLEY SIZE (3)
			GUI.Label(new Rect(Screen.width/2 - 230,60,150,30),"Volley Size");
			enemyVolleySize = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,80,75,15),enemyVolleySize,0,50));
			GUI.Label(new Rect(Screen.width/2 - 150,75,30,50), enemyVolleySize.ToString());
			
			//ENEMY SHOT DELAY (0.25)
			GUI.Label(new Rect(Screen.width/2 - 230,90,170,30),"Shot Delay");
			enemyShotDelay = GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,110,75,15),enemyShotDelay,0,1f);
			GUI.Label(new Rect(Screen.width/2 - 150,105,30,50), string.Format("{0:0.0}", enemyShotDelay));

			//ENEMY SHOT SPEED (3)
			GUI.Label(new Rect(Screen.width/2 - 230,120,170,30),"Shot Speed");
			enemyShotMoveSpeed = GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,140,75,15),enemyShotMoveSpeed,1,10f);
			GUI.Label(new Rect(Screen.width/2 - 150,135,30,50), string.Format("{0:0.0}", enemyShotMoveSpeed));

			//ENEMY SHOT DAMAGE (1)
			GUI.Label(new Rect(Screen.width/2 - 230,150,170,30),"Shot Damage");
			enemyShotDamage = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,170,75,15),enemyShotDamage,0,100f));
			GUI.Label(new Rect(Screen.width/2 - 150,165,30,50), string.Format("{0:0.0}", enemyShotDamage));
			
			break;

		case 3:
			
			GUI.Label(new Rect(Screen.width/2 - 25,4,50,30), "Beamer");
			
			//BEAMER HITPOINTS (20)
			GUI.Label(new Rect(Screen.width/2 - 230,30,170,300),"Hitpoints");
			beamerHitpoints = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,50,75,15),beamerHitpoints,0,100));
			GUI.Label(new Rect(Screen.width/2 - 150,45,30,50), beamerHitpoints.ToString());

			//BEAMER ROTATION SPEED (20)
			GUI.Label(new Rect(Screen.width/2 - 230,60,170,30),"Rotation Speed");
			beamerRotSpeed = GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,80,75,15),beamerRotSpeed,0,15f);
			GUI.Label(new Rect(Screen.width/2 - 150,75,30,50), string.Format("{0:0.0}", beamerRotSpeed));

			//BEAMER ROTATION DURATION (1)
			GUI.Label(new Rect(Screen.width/2 - 230,90,170,30),"Rotation Duration");
			beamerRotDuration = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,110,75,15),beamerRotDuration,0,1800));
			GUI.Label(new Rect(Screen.width/2 - 150,105,50,50), string.Format("{0:0.0}", beamerRotDuration));

			//BEAM DAMAGE (10)
			GUI.Label(new Rect(Screen.width/2 - 230,120,180,300),"Beam Damage");
			beamerDamage = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,140,75,15),beamerDamage,0,100));
			GUI.Label(new Rect(Screen.width/2 - 150,135,30,50), beamerDamage.ToString());
			
			break;
			
		case 4:
			
			GUI.Label(new Rect(Screen.width/2 - 25,4,50,30), "Charger");
			
			//CHARGER HITPOINTS (40)
			GUI.Label(new Rect(Screen.width/2 - 230,30,170,300),"Hitpoints");
			chargerHitpoints = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,50,75,15),chargerHitpoints,0,100));
			GUI.Label(new Rect(Screen.width/2 - 150,45,30,50), chargerHitpoints.ToString());
			
			//CHARGER CHARGE DELAY (150)
			GUI.Label(new Rect(Screen.width/2 - 230,60,170,30),"Charge Delay");
			chargerChargeDelay = GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,80,75,15), chargerChargeDelay,0,300);
			GUI.Label(new Rect(Screen.width/2 - 150,75,40,50), string.Format("{0:0.0}", chargerChargeDelay));
			
			//CHARGER CHARGE SPEED (15)
			GUI.Label(new Rect(Screen.width/2 - 230,90,170,30),"Charge Speed");
			chargerChargeSpeed = GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,110,75,15), chargerChargeSpeed,0,50);
			GUI.Label(new Rect(Screen.width/2 - 150,105,30,50), string.Format("{0:0.0}", chargerChargeSpeed));
			
			//CHARGER TRACK SPEED (0.4f)
			GUI.Label(new Rect(Screen.width/2 - 230,120,170,30),"Track Speed");
			chargerTrackSpeed = GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,140,75,15), chargerTrackSpeed,0,1f);
			GUI.Label(new Rect(Screen.width/2 - 150,135,30,50), string.Format("{0:0.0}", chargerTrackSpeed));
			
			//CHARGER DRAG (12)
			GUI.Label(new Rect(Screen.width/2 - 230,150,170,30),"Drag");
			chargerDrag = GUI.HorizontalSlider(new Rect(Screen.width/2 - 230,170,75,15), chargerDrag,0,20f);
			GUI.Label(new Rect(Screen.width/2 - 150,165,30,50), string.Format("{0:0.0}", chargerDrag));
			
			break;
			
		}

		if(GUI.Button(new Rect(Screen.width/2 - 230,565,30,30),"<") && page > 1)
		{
			page--;
		}

		if(GUI.Button(new Rect(Screen.width/2 + 200,565,30,30),">"))
		{
			page++;
		}

		GUI.Label(new Rect(Screen.width/2 - 45,565,70,60), "Page " + page + " of 4");

		if(GUI.Button(new Rect(Screen.width/2 - 230,600,75,30),"Main Menu"))
		{
			Application.LoadLevel("mainMenu");
		}

		if(GUI.Button(new Rect(Screen.width/2 + 155,600,75,30),"Save"))
		{
			PlayerPrefs.SetFloat("playerShotDelay", playerShotDelay);
			PlayerPrefs.SetFloat("playerMoveSpeed", playerMoveSpeed);
			PlayerPrefs.SetInt("playerHitpoints", playerHitpoints);
			PlayerPrefs.SetFloat("playerShotMoveSpeed", playerShotSpeed);
			PlayerPrefs.SetInt("playerShotDamage", playerShotDamage);
			
			PlayerPrefs.SetFloat("enemyMoveRange", enemyMoveRange);
			PlayerPrefs.SetInt("enemyVolleySize", enemyVolleySize);
			PlayerPrefs.SetFloat("enemyShotDelay", enemyShotDelay);
			PlayerPrefs.SetFloat("VolleyShotSpeed", enemyShotMoveSpeed);
			PlayerPrefs.SetInt("VolleyShotDamage", enemyShotDamage);

			PlayerPrefs.SetInt("BeamerHitpoints", beamerHitpoints);
			PlayerPrefs.SetFloat("BeamerRotationSpeed", beamerRotSpeed);
			PlayerPrefs.SetFloat("BeamerRotationDuration", beamerRotDuration);
			PlayerPrefs.SetInt("BeamerDamage", beamerDamage);

			PlayerPrefs.SetInt("ChargerHitpoints", chargerHitpoints);
			PlayerPrefs.SetFloat("ChargerChargeDelay", chargerChargeDelay);
			PlayerPrefs.SetFloat("ChargerChargeSpeed", chargerChargeSpeed);
			PlayerPrefs.SetFloat("ChargerTrackSpeed", chargerTrackSpeed);
			PlayerPrefs.SetFloat("ChargerDrag", chargerDrag);
		}
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.RightArrow))   
		{
			page++;
		}

		if(Input.GetKeyUp(KeyCode.Q))  
		{
			Application.Quit();
		}
	}

	void OnApplicationQuit()
	{
		PlayerPrefs.DeleteAll();
	}
}
