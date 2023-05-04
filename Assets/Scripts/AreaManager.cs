using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct AreaData{
	public string areaSlug;
}

[System.Serializable]
public struct TravelData{
	[Range(1.0f, 3.0f)]
	public float minTravelSpeed; // Units per second when idle
	[Range(1.0f, 3.0f)]
	public float maxTravelSpeed; //Units per second when running
	[Range(1.0f, 3.0f)]
	public float currentTravelSpeed; //Units per second when running
	public float distanceTraveled; //How many units have been traveled
	public float totalDistanceTraveled; //How many units have been traveled
	public float distanceSinceEvent; //How long has it been since an event?
}

public enum PathDirection{
	North,
	South
}

public enum AreaState{
	Walking, 
	MiniGame,
	Event,
	Battle,
	Inventory,
	Map,
	NewArea,
	Restart,
}

public class AreaManager : MonoBehaviour {
	public static AreaManager instance;
	public static Dictionary<string, Area> areas;
	public List<Area> areaList;
	public List<AreaEvent> standardEvents;

	public Area currentArea;
	public Area nextArea;

	public AreaData areaData;
	public TravelData travelData;
	public AreaState areaState;
	public AreaState nextAreaState;

	public bool hasEvent;
	public bool checkedForEvent;
	public float chanceForEvent = 90.0f;
	public float maxChanceForEvent = 90.0f;
	public float eventTimeout = 0.0f;
	public string eventName;
	[Range(0.1f, 1.0f)]
	public float travelModifier; //Units per second when running
	public float travelModifierScale = 0.1f;

	public AudioClip eventAlert;
	public AudioClip transitionFadeIn;
	// Use this for initialization
	void Awake () {
		if(instance != null){
			Destroy(this.gameObject);
		} else {
			TimeData timeData = SaveManager.LoadTimeData();
			areaData = SaveManager.LoadAreaData();
			travelData = SaveManager.LoadTravelData();
			long elapsedSeconds = (DateTime.Now.Ticks - timeData.saveTime)/10000000;
			Debug.Log("It has been " + elapsedSeconds + " seconds since the last playthrough");
			Debug.Log("The player has traveled: " + (travelData.minTravelSpeed * elapsedSeconds) + " units");
			travelData.distanceTraveled += travelData.minTravelSpeed * elapsedSeconds;
			travelData.totalDistanceTraveled += travelData.minTravelSpeed * elapsedSeconds;
			instance = this;
			areas = new Dictionary<string, Area>();
			foreach(Area a in areaList){
				areas.Add(a.areaSlug, a);
			}
			if(areas.ContainsKey(areaData.areaSlug)){
				currentArea = areas[areaData.areaSlug];
			}
			DontDestroyOnLoad(this);
		}
	}

	void Update(){
		if(nextArea != null && nextArea != currentArea && GameManager.instance.transitionState == TransitionState.FadingOut){
			currentArea = nextArea;
		} else if(currentArea == null){
			currentArea = areas[areaData.areaSlug];
		}
		switch(areaState){
			case AreaState.Walking:
				//travel distance while walking
				//Allows us to have a on click run button meter thing that goes up, hook that up later
				travelData.currentTravelSpeed = Mathf.Lerp(travelData.minTravelSpeed, travelData.maxTravelSpeed, travelModifier);
				float travelAmount = Time.deltaTime * travelData.currentTravelSpeed;
				
				//If we're unhappy or VERY unhappy we don't want to move
				if(Player.instance.playerData.currentHappiness <= (int)HappinessState.Meh){
					Debug.Log("Not moving due to Happiness");
				} else {
					travelData.distanceTraveled += travelAmount;
					travelData.totalDistanceTraveled += travelAmount;
				}
				if(!currentArea.finalArea && travelData.distanceTraveled > currentArea.areaLength){
					nextAreaState = AreaState.NewArea;
				} else if(currentArea.finalArea && travelData.distanceTraveled > currentArea.areaLength){
					nextAreaState = AreaState.Restart;
				}

				travelModifier -= Time.deltaTime * travelModifierScale;
				if(travelModifier < 0){
					travelModifier = 0;
				} else if(travelModifier > 1){
					travelModifier = 1;
				}
				//test to see if an event is going on

				if(!hasEvent){
					if(!checkedForEvent && Mathf.FloorToInt(Time.time % 5) == 0){
						checkedForEvent = true;
						float r = UnityEngine.Random.Range(0.0f, 100.0f);
						if(r > chanceForEvent){
							chanceForEvent = maxChanceForEvent;
							Debug.Log(r + " Was Greather than " + chanceForEvent);
							hasEvent = true;
							AudioManager.PlaySound(eventAlert);
							eventTimeout = 30;
						} else {
							chanceForEvent -= 1.0f;
							Debug.Log(r + " Was Less than " + chanceForEvent);
						}
					}
				} 
				if(Mathf.FloorToInt(Time.time % 5) != 0){
					checkedForEvent = false;
				}
				if(eventTimeout > 0){
					eventTimeout -= Time.deltaTime;
				} else if(eventTimeout <= 0){
					hasEvent = false;
				}
			

			break;
			case AreaState.MiniGame:
				
			break;
			case AreaState.Event:
				eventTimeout = 0;
				hasEvent = false;
			break;
			case AreaState.Battle:

			break;
			case AreaState.Inventory:

			break;
			case AreaState.NewArea:
			break;
		}

		if(nextAreaState != areaState){
			//We're going to a new area!
			switch(nextAreaState){
				case AreaState.Walking:
					//Allows us to have a on click run button meter thing that goes up, hook that up later
					AudioManager.PlaySound(transitionFadeIn);
					GameManager.GoToScene("WalkingScene");
				break;
				case AreaState.MiniGame:
					GameManager.GoToScene("MiniGame");
				break;
				case AreaState.Event:
					//This will eventually be unique scenes for each event
					AudioManager.PlaySound(transitionFadeIn);
					GameManager.GoToScene(eventName);
				break;
				case AreaState.Battle:
					GameManager.GoToScene("Battle");
				break;
				case AreaState.Inventory:
					GameManager.GoToScene("Inventory");
				break;
				case AreaState.Map:
					GameManager.GoToScene("Map");
				break;
				case AreaState.NewArea:
					GameManager.GoToScene("NewArea");
					//Unhide the north/south buttons
				break;
				case AreaState.Restart:
					GameManager.GoToScene("Restart");
				break;
			}
			areaState = nextAreaState;
		}
	}
	public static void StartEvent(string eventName){
		instance.eventName = eventName;
		instance.nextAreaState = AreaState.Event;
	}

	public static void ChooseDirection(PathDirection pd){
		if(pd == PathDirection.North){
			instance.areaData.areaSlug = instance.currentArea.northPath.areaSlug;
			instance.nextArea = areas[instance.areaData.areaSlug]; 
		} else {
			instance.areaData.areaSlug = instance.currentArea.southPath.areaSlug;
			instance.nextArea = areas[instance.areaData.areaSlug]; 
		}
		instance.travelData.distanceTraveled = 0;
		instance.nextAreaState = AreaState.Walking;
	}

	public static void GoToArea(string areaSlug){
		Debug.Log("Trying To Go To area: " + areaSlug);
		if(areas.ContainsKey(areaSlug)){
			instance.areaData.areaSlug = areaSlug;
			instance.nextArea = areas[instance.areaData.areaSlug]; 
			instance.travelData.distanceTraveled = 0;
			instance.nextAreaState = AreaState.Walking;
		} else {
			Debug.Log(areas.ContainsKey(areaSlug));
			Debug.Log("areas dictionary does not contain " + areaSlug);
		}
	}

	// public AreaEvent chooseAnEvent(){
	// 	List<AreaEvent> events = new List<AreaEvent>();
	// 	events.AddRange(standardEvents);
	// 	events.AddRange(currentArea.events);

	// 	int ran = (int)Random.Range(0, events.Count);
	// 	if(events[ran].eventScene != ""){
	// 		return events[ran];
	// 	}
	// 	Debug.LogWarning("Event String is Empty!");
	// 	return events[ran];
	// }
}
