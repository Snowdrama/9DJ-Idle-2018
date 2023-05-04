using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SouthButton : SpriteButton {
	public override void Update(){
		if(Input.GetKeyDown(KeyCode.S)){
			Press();
		}
	}
	public override void Press(){
		AreaManager.ChooseDirection(PathDirection.South);
	}
}
