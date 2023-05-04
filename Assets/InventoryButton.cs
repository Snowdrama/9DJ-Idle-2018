using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : SpriteButton {
	public InventorySlide inventorySlide;
	public AudioClip inventorySlideSound;
	public override void Press(){
		inventorySlide.showingInventory = !inventorySlide.showingInventory;
		AudioManager.PlaySound(inventorySlideSound);
	}
}
