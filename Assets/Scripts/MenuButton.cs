using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIElementType{
	Button,
	Text,
	Image
}

public struct UIElement{
	public string elementName;
	public GameObject go;
	public UIElementType type;
}

public class MenuButton : MonoBehaviour {
	public void TransitionToScene(string sceneName){
		GameManager.GoToScene(sceneName);
	}
}
