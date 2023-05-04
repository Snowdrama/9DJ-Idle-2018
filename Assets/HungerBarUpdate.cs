using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerBarUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float hungerPercent = Player.instance.playerData.currentHunger/100;
		this.GetComponent<Renderer>().material.SetFloat("_Cutoff", hungerPercent);
	}
}
