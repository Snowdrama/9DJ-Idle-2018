using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TimeData{
	public long originalStartTime; //this is so we can see how long the player has been playing
	public long saveTime;
	public long currentTime;
}

//No need to do Destroy(this) code 
public class TimeManager : MonoBehaviour {
	public static TimeManager instance;
	public TimeData timeData; //this is so we can see how long the player has been playing
	public bool forceRestart;
	public long elapsedTime;
	public bool saving;
	// Use this for initialization
	void Start () {
		if(instance != null){
			Destroy(this.gameObject);
		} else {
			//load the save time data or initialize the time data
			timeData = SaveManager.LoadTimeData();
			if(timeData.originalStartTime == 0){
				//This is the first time the game has launched
				timeData.originalStartTime = DateTime.Now.Ticks;
				timeData.currentTime = DateTime.Now.Ticks;
			}
			instance = this;
			DontDestroyOnLoad(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeData.currentTime = DateTime.Now.Ticks;
		elapsedTime = (timeData.currentTime - timeData.originalStartTime)/10000000;
		if(elapsedTime % 5 == 0){
			if(!saving){
				saving = true;
				//Debug.Log("saving");
				SaveManager.SaveGame();
			}
		} else {
			saving = false;
		}
	}
}
