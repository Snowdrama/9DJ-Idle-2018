using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventButtonToggle : MonoBehaviour {
	public GameObject eventButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(AreaManager.instance.hasEvent){
			eventButton.SetActive(true);
		} else {
			eventButton.SetActive(false);
		}
	}
}
