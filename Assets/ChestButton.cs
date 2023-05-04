using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestButton : SpriteButton {
	public int buttonIndex = 0;
	public static float chanceForItem;
	public List<Item> possibleItems;
	public Chest targetChest;
	public AudioClip itemSound;
	public AudioClip noItemSound;
	public override void Start(){
		chanceForItem = 0.75f;
	}
	public override void Press(){
		if(Random.Range(0.0f, 1.0f) > chanceForItem){
			Debug.Log("Got an Item!");
			Item it = possibleItems[Random.Range(0, possibleItems.Count)];

			if(Player.instance.playerData.items.ContainsKey(it.itemType)){
				it.count = Player.instance.playerData.items[it.itemType].count + 1;
				Player.instance.playerData.items[it.itemType] = it;
			} else {		
				Player.instance.playerData.items.Add(it.itemType, it);
			}
			SaveManager.SaveGame();
			AudioManager.PlaySound(itemSound);
			targetChest.Open(buttonIndex, it);
		} else {
			chanceForItem -= 0.20f;
			Debug.Log("Chance for item is now: " + chanceForItem);
			AudioManager.PlaySound(noItemSound);
			targetChest.Open(buttonIndex);
		}
		this.gameObject.SetActive(false);
	}
}
