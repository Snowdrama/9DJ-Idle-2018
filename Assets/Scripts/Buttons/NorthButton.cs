using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorthButton : SpriteButton {
	public override void Update(){
		if(Input.GetKeyDown(KeyCode.N)){
			Press();
		}
	}
	public override void Press(){
		AreaManager.ChooseDirection(PathDirection.North);
	}
}
