using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	[SerializeField]
	private GameCamera gameCamera;
	[SerializeField]
	private int totalEnemies;
	[SerializeField]
	private GameObject youWin;
	//[SerializeField]
	//private GameObject player;
	[SerializeField]
	private GameObject deadScreen;
	[SerializeField]
	private GameObject deadText;

	private int deadEnemies;

	public int DeadEnemies { get{ return deadEnemies; } set { deadEnemies = value; } } 

	void Start () 
	{
		//Switches to the mech
		gameCamera.SwitchFollowTarget(1);
	}

	void Update () 
	{
		if(deadEnemies == totalEnemies)
		{
			youWin.active = true;
		}

		if (Input.GetKeyDown(KeyCode.Return)) 
		{  
	    	Application.LoadLevel (0);  
	  	}  
	  //	if(player.AmIDead)
	  //	{
	  //		deadScreen.SetActive(true);
	  //		deadText.SetActive(true);
	  //	}
	}
}
