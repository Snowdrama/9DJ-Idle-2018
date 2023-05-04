using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour {
	public Sprite[] marbleSprites;
	public bool settling = false;
	public float settleTime = 0.0f;
	public float minSettleTime = 0.25f;
	public float settleVelocityMag = 0.01f;
	public bool settled = false;
	public bool counted = true;
	public bool popped = false;
	public MarbleType type;
	Rigidbody2D rigid;
	
	// Use this for initialization
	void Start () {
		type = (MarbleType)UnityEngine.Random.Range(0, ((MarbleType[])Enum.GetValues(typeof(MarbleType))).Length);
		this.GetComponent<SpriteRenderer>().sprite = marbleSprites[(int)type];
		rigid = this.GetComponent<Rigidbody2D>();
		var list = MarbleManager.marbles[type];
		list.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
	
		if(popped){
			MarbleManager.marbles[type].Remove(this);
			Destroy(this.gameObject);
		}
		if(rigid.velocity.magnitude < settleVelocityMag){
			settling = true;
		} else {
			settling = false;
		}



		if(settling){
			if(settleTime <= minSettleTime){
				settleTime += Time.deltaTime;
			} else {
				settled = true;
			}
		} else {
			settleTime = 0;
			settled = false;
		}
	

	}
}
