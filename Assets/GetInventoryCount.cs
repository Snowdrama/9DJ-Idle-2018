using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInventoryCount : MonoBehaviour {
	public ItemType type;
	TextMesh tm;
	// Use this for initialization
	void Start () {
		tm = this.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Player.instance.playerData.items.ContainsKey(type)){
			tm.text = "" + Player.instance.playerData.items[type].count;
		} else {
			tm.text = "0";
		}
	}
}
