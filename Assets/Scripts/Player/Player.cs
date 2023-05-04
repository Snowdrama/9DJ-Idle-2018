using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  travelSpeed = 1 unit per second
  area
 */

public enum HappinessState{
	VeryHappy = 80,
	Happy = 60,
	Meh = 40,
	Unhappy = 20,
	VeryUnhappy = 0
}


public enum HungerState{
	Stuffed = 80,
	Full = 60,
	Satisfied = 40,
	Hungry = 20,
	Starving = 0
}

public enum HealthState{
	Healthy = 80,
	Good = 60,
	Average = 40,
	Hurting = 20,
	Dying = 0
}

[System.Serializable]
public struct PlayerData{
	public int strength;
	//This is used for saving and loading.
	public List<Item> itemList;
	//This is used for actuall in game useage
	public Dictionary<ItemType, Item>items;

	public bool autoRun;
	public float autoRunTime;

	[Range(0.0f, 100.0f)]	
	public float health;
	public HealthState healthState;

	[Range(0.0f, 100.0f)]	
	public float currentHappiness;
	public HappinessState happinessState;

	[Range(0.0f, 100.0f)]	
	public float currentHunger;
	public HungerState hungerState;
}

public enum PlayerState{
	Walking,
	NewArea,
	Waiting,
	InEvent
}

public class Player : MonoBehaviour {
	public static Player instance;
	public PlayerData playerData;
	public PlayerState state;
	public float maxAutoRunTime = 300;
	public SpriteRenderer playerSprite;
	public float candyHappinessValue = 40;
	public float foodHungerValue = 40;
	public float potionValue = 40;
	public bool hungerAlertPlayed;
	public bool healthAlertPlayed;
	public AudioClip healthDanger;
	public AudioClip hungerDanger;
	// Use this for initialization
	void Start () {
		if(instance != null){
			Destroy(this.gameObject);
		} else{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
			playerData = SaveManager.LoadPlayerData();
			playerData.items = new Dictionary<ItemType, Item>();
			TimeData timeData = SaveManager.LoadTimeData();
			long elapsedSeconds = (DateTime.Now.Ticks - timeData.saveTime)/10000000;
			playerData.currentHappiness -= elapsedSeconds/1800.0f;// 1 for every half hour that passed 
			Debug.Log("Happiness went down: " + (elapsedSeconds/1800.0f));// to lose all happiness it would take 200 hours of ofline time
			
			foreach(Item i in playerData.itemList){
				playerData.items.Add(i.itemType, i);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(playerData.autoRun){
			playerData.autoRunTime -= Time.deltaTime;
			AreaManager.instance.travelModifier = 1;
		} 
		if(playerData.autoRunTime <= 0){
			playerData.autoRun = false;
			playerData.autoRunTime = 0;
		}

		//HAPPINESS
		foreach(HappinessState t in Enum.GetValues(typeof(HappinessState))){
			if(playerData.currentHappiness > (int)t){
				playerData.happinessState = t;
			}
		}
		playerData.currentHappiness -= Time.deltaTime/60; //1 per minute
		if(playerData.currentHappiness > 100){
			playerData.currentHappiness = 100;
		} else if(playerData.currentHappiness < 0){
			playerData.currentHappiness = 0;
		}

		//HUNGER
		foreach(HungerState hs in Enum.GetValues(typeof(HungerState))){
			if(playerData.currentHunger > (int)hs){
				playerData.hungerState = hs;
			}
		}
		playerData.currentHunger -= Time.deltaTime/60; //1 per minute
		if(playerData.currentHunger > 100){
			playerData.currentHunger = 100;
		} else if(playerData.currentHunger < 0){
			playerData.currentHunger = 0;
		}


		//Health
		foreach(HealthState hs in Enum.GetValues(typeof(HealthState))){
			if(playerData.health > (int)hs){
				playerData.healthState = hs;
			}
		}
		if(playerData.health > 100){
			playerData.health = 100;
		} else if(playerData.health < 0){
			playerData.health = 0;
		}


		//used for expression changes
		switch(playerData.happinessState){
			case HappinessState.VeryHappy:
			break;
			case HappinessState.Happy:
			break;
			case HappinessState.Meh:
			break;
			case HappinessState.Unhappy:
			break;
			case HappinessState.VeryUnhappy:
			break;
		}
		
		switch(Player.instance.playerData.healthState){
			case HealthState.Healthy:
			break;
			case HealthState.Good:
			break;
			case HealthState.Average:
			break;
			case HealthState.Hurting:
			//Some sound effect for anything below dying
			if(!healthAlertPlayed && Mathf.Floor(Time.time % 10) == 0){
				healthAlertPlayed = true;
				AudioManager.PlaySound(healthDanger);
			}
			if(Mathf.Floor(Time.time % 10) != 0){
				healthAlertPlayed = false;
			}
			break;
			case HealthState.Dying:
			if(!healthAlertPlayed && Mathf.Floor(Time.time % 2) == 0){
				healthAlertPlayed = true;
				AudioManager.PlaySound(healthDanger);
			}
			if(Mathf.Floor(Time.time % 2) != 0){
				healthAlertPlayed = false;
			}
			break;
		}
		
		switch(Player.instance.playerData.hungerState){
			case HungerState.Stuffed:
			break;
			case HungerState.Full:
			break;
			case HungerState.Satisfied:
			break;
			case HungerState.Hungry:
			Player.instance.playerData.health -= Time.deltaTime/60;
			Player.instance.playerData.currentHappiness -= Time.deltaTime/60;
			if(!hungerAlertPlayed && Mathf.Floor(Time.time % 30) == 0){
				hungerAlertPlayed = true;
				AudioManager.PlaySound(hungerDanger);
			}
			if(Mathf.Floor(Time.time % 30) != 0){
				hungerAlertPlayed = false;
			}
			break;
			case HungerState.Starving:
			Player.instance.playerData.health -= Time.deltaTime;
			Player.instance.playerData.currentHappiness -= Time.deltaTime;
			if(!hungerAlertPlayed && Mathf.Floor(Time.time % 10) == 0){
				hungerAlertPlayed = true;
				AudioManager.PlaySound(hungerDanger);
			}
			if(Mathf.Floor(Time.time % 10) != 0){
				hungerAlertPlayed = false;
			}
			//Some sound effect for starving
			break;
		}
	}

	public static void PrepForSave(){
		List<ItemType> types = new List<ItemType>(instance.playerData.items.Keys);
		instance.playerData.itemList = new List<Item>();
		foreach(ItemType ty in types){
			instance.playerData.itemList.Add(instance.playerData.items[ty]);
		}
	}

	public void AddEffect(ItemType type){
		switch(type){
			case ItemType.AutoRun:
				playerData.autoRun = true;
				playerData.autoRunTime = maxAutoRunTime;
				break;
			case ItemType.Candy:
				playerData.currentHappiness += candyHappinessValue;
				break;
			case ItemType.Food:
				playerData.currentHunger += foodHungerValue;
				break;
			case ItemType.Potion:
				playerData.health += potionValue;
				break;
		}
		//Save the game so the use of the item is auto saved
		SaveManager.SaveGame();
	}
}
