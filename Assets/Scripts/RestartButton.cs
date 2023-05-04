using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : SpriteButton {
	
	public override void Update(){
		if(Input.GetKeyDown(KeyCode.R)){
			Press();
		}
	}
	// Update is called once per frame
	public override void Press () {
		AreaManager.GoToArea("path");
	}
}
