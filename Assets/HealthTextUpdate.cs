using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTextUpdate : MonoBehaviour {
	public TextMesh textMesh;
	// Use this for initialization
	void Start () {
		textMesh = this.GetComponent<TextMesh>();
	}
	
	
	// Update is called once per frame
	void Update () {
		string updateString = "";
		
		switch(Player.instance.playerData.healthState){
			case HealthState.Healthy:
				updateString = "Healthy";
				break;
			case HealthState.Good:
				updateString = "Good";
				break;
			case HealthState.Average:
				updateString = "Average";
				break;
			case HealthState.Hurting:
				updateString = "Hurting";
				break;
			case HealthState.Dying:
				updateString = "Dying";
				break;
		}
		textMesh.text = "Health: " + updateString;
	}
}
