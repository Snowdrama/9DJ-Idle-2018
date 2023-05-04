using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class TextMeshSortingLayer : MonoBehaviour {
	public string sortingLayerName = "";
	public int sortingOrder = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Renderer>().sortingLayerName = sortingLayerName;
		this.GetComponent<Renderer>().sortingOrder = sortingOrder;
	}
}
