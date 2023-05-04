using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessBarUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float happinessPercent = Player.instance.playerData.currentHappiness/100;
		this.GetComponent<Renderer>().material.SetFloat("_Cutoff", happinessPercent);
	}
}
