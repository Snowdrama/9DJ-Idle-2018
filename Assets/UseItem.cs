using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : SpriteButton {
	public ItemType type;

	public override void Press(){
		if(Player.instance.playerData.items.ContainsKey(type) && Player.instance.playerData.items[type].count > 0){
			Player.instance.AddEffect(type);
			Item it = Player.instance.playerData.items[type];
			it.count = it.count - 1;
			Player.instance.playerData.items[type] = it;
		} else {
			//play error sound
		}
	}
}
