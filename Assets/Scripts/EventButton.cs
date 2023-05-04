using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventButton : SpriteButton {
	public override void Press(){
		List<AreaEvent> eventList = new List<AreaEvent>();
		eventList.AddRange(AreaManager.instance.standardEvents);
		eventList.AddRange(AreaManager.instance.currentArea.events);

		int randomIndex = Random.Range(0, eventList.Count);
		if(eventList[randomIndex].eventScene != ""){
			AreaManager.StartEvent(eventList[randomIndex].eventScene);
		}
	}
}
