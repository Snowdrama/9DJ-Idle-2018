using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionBar : MonoBehaviour {
	public float percentage;
	public string percentageText;
	public TextMesh completionText;
	public Material percentageMaterial;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		percentage = AreaManager.instance.travelData.distanceTraveled/AreaManager.instance.currentArea.areaLength;
		percentageMaterial.SetFloat("_Cutoff", percentage);
		percentageText = "Completion: " + ((percentage * 100).ToString("0.00")) + "%";
		completionText.text = percentageText;
	}
}
