using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunButton : SpriteButton {
	public float increaseAmount = 0.1f;
	public AudioClip soundOnPress;
	public override void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			Press();
		}
	}
	// Update is called once per frame
	public override void Press () {
		AreaManager.instance.travelModifier += increaseAmount;
		if(soundOnPress != null){
			AudioManager.PlaySound(soundOnPress);
		}
		//Debug.Log("Increase Run Amount to: " + AreaManager.instance.travelData.travelModifier);
	}
}
