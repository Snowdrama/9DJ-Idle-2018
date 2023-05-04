using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float healthPercent = Player.instance.playerData.health/100;
		this.GetComponent<Renderer>().material.SetFloat("_Cutoff", healthPercent);
	}
}
