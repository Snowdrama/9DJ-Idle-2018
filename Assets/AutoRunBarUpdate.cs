using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRunBarUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float autoRunPercent = Player.instance.playerData.autoRunTime/Player.instance.maxAutoRunTime;
		this.GetComponent<Renderer>().material.SetFloat("_Cutoff", autoRunPercent);
	}
}
