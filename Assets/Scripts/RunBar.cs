using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBar : MonoBehaviour {
	public float percentage;
	public Material percentageMaterial;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		percentage = (AreaManager.instance.travelData.currentTravelSpeed - AreaManager.instance.travelData.minTravelSpeed)/(AreaManager.instance.travelData.maxTravelSpeed - AreaManager.instance.travelData.minTravelSpeed);
		percentageMaterial.SetFloat("_Cutoff", percentage);
	}
}
