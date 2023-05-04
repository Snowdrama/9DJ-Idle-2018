using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {
	public Sprite openChestSprite;
	public Sprite noneSprite;
	public bool isOpen;
	List<Vector3> originalPositions;
	public List<GameObject> itemSpriteObjects;
	public List<ChestButton> buttons;
	public List<Sprite> itemSprites;
	void Start(){
		originalPositions = new List<Vector3>();
		foreach(GameObject go in itemSpriteObjects){
			originalPositions.Add(go.transform.position);
		}
	}

	void Update(){
		var ind = 0;
		foreach(GameObject go in itemSpriteObjects){
			go.transform.position = originalPositions[ind] + new Vector3(0, Mathf.Sin(Time.time + (ind * 0.33f)),0);
			ind++;
		}
	}

	public void Open(int index){
		this.GetComponent<SpriteRenderer>().sprite = openChestSprite;
		itemSpriteObjects[index].GetComponent<SpriteRenderer>().sprite = noneSprite;
	}
	public void Open(int index, Item chosenItem){
		this.GetComponent<SpriteRenderer>().sprite = openChestSprite;
		itemSpriteObjects[index].GetComponent<SpriteRenderer>().sprite = itemSprites[(int)chosenItem.itemType-1];
	}

}
