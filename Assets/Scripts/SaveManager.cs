using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SaveData{
	public TimeData timeData;
	public AreaData areaData;
	public TravelData travelData;
	public PlayerData playerData;
}

public class SaveManager : MonoBehaviour {
	public static bool dataLoaded = false;
	public static SaveData loadedData;
	

	public static void SaveGame(){
		SaveData sd = new SaveData();
		sd.timeData = TimeManager.instance.timeData;
		sd.timeData.saveTime = DateTime.Now.Ticks;//we save the ticks of the current time this is used to figure out distance since last load
		sd.areaData = AreaManager.instance.areaData;
		sd.travelData = AreaManager.instance.travelData;
		Player.PrepForSave();
		sd.playerData = Player.instance.playerData;
		JsonFileManager.WriteJsonToExternalResource("save.json",JsonUtility.ToJson(sd));
	}

	private static SaveData LoadSaveData(){
		string json = JsonFileManager.LoadJsonAsExternalResource("save.json");
		if(json != null && json != ""){
			loadedData = JsonUtility.FromJson<SaveData>(JsonFileManager.LoadJsonAsExternalResource("save.json"));
			return loadedData;
		} else {
			loadedData = new SaveData();
			loadedData.areaData = new AreaData();
			loadedData.areaData.areaSlug = "path";
			loadedData.timeData = new TimeData();
			loadedData.timeData.currentTime = DateTime.Now.Ticks;
			loadedData.timeData.originalStartTime = DateTime.Now.Ticks;
			loadedData.timeData.saveTime = DateTime.Now.Ticks;
			loadedData.travelData = new TravelData();
			loadedData.travelData.minTravelSpeed = 1;
			loadedData.travelData.maxTravelSpeed = 3;
			loadedData.travelData.currentTravelSpeed = 1;
			loadedData.playerData = new PlayerData();
			loadedData.playerData.health = 100;
			loadedData.playerData.currentHunger = 100;
			loadedData.playerData.currentHappiness = 100;
			loadedData.playerData.autoRun = false;
			loadedData.playerData.autoRunTime = 0;
			loadedData.playerData.itemList = new List<Item>();
			loadedData.playerData.items = new Dictionary<ItemType, Item>();
			return loadedData;
		}
	}

	public static TimeData LoadTimeData(){
		return LoadSaveData().timeData;
	}
	public static AreaData LoadAreaData(){
		return LoadSaveData().areaData;
	}
	public static TravelData LoadTravelData(){
		return LoadSaveData().travelData;
	}
	public static PlayerData LoadPlayerData(){
		return LoadSaveData().playerData;
	}
}
