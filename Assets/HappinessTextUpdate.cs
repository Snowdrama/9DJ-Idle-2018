using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessTextUpdate : MonoBehaviour {
	public TextMesh textMesh;
	// Use this for initialization
	void Start () {
		textMesh = this.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		string updateString = "";
		
		switch(Player.instance.playerData.happinessState){
			case HappinessState.VeryHappy:
				updateString = "Very Happy";
				break;
			case HappinessState.Happy:
				updateString = "Happy";
				break;
			case HappinessState.Meh:
				updateString = "Meh";
				break;
			case HappinessState.Unhappy:
				updateString = "Unhappy";
				break;
			case HappinessState.VeryUnhappy:
				updateString = "Very Unhappy";
				break;
		}
		textMesh.text = "Happiness: " + updateString;
	}
}
