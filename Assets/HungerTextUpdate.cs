using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerTextUpdate : MonoBehaviour {
	public TextMesh textMesh;
	// Use this for initialization
	void Start () {
		textMesh = this.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		string updateString = "";
		
		switch(Player.instance.playerData.hungerState){
			case HungerState.Stuffed:
				updateString = "Stuffed";
				break;
			case HungerState.Full:
				updateString = "Full";
				break;
			case HungerState.Satisfied:
				updateString = "Satisfied";
				break;
			case HungerState.Hungry:
				updateString = "Hungry";
				break;
			case HungerState.Starving:
				updateString = "Starving";
				break;
		}
		textMesh.text = "Hunger: " + updateString;
	}
}
