using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAutoRunBar : MonoBehaviour {
	public GameObject autoRunBar;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Player.instance.playerData.autoRun){
			autoRunBar.SetActive(true);
		} else {
			autoRunBar.SetActive(false);
		}
	}
}
