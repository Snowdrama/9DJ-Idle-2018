using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : SpriteButton {

	public override void Press(){
		Application.Quit();
	}
}
