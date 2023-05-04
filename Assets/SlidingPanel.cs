using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPanel : MonoBehaviour {
	public bool showingPanel;

	public Vector3 hiddenPosition;
	public Vector3 showPosition;
	[Range(1,5)]
	public int slideSpeed = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(showingPanel){
			this.transform.position = Vector3.MoveTowards(this.transform.position, showPosition, Time.deltaTime * Vector3.Distance(this.transform.position, showPosition) * slideSpeed);
		} else {
			this.transform.position = Vector3.MoveTowards(this.transform.position, hiddenPosition, Time.deltaTime * Vector3.Distance(this.transform.position, hiddenPosition) * slideSpeed);
		}
	}
}
