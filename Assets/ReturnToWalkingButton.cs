using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToWalkingButton : SpriteButton {
	// Update is called once per frame
	public override void Press () {
		Debug.Log("Returning to walking scene");
		AreaManager.instance.nextAreaState = AreaState.Walking;
	}
}
