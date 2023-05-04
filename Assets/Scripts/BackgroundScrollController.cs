using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrollController : MonoBehaviour {
	public Sprite defaultTexture;
	public List<Material> backgroundMaterials;
	List<Sprite> backgroundSprites;
	public float[] scrollOffsets;
	public float[] scrollSpeeds = {0.2f, 0.2f, 0.2f, 0.2f};
	public Area displayedArea;
	// Use this for initialization
	void Start () {
		displayedArea = AreaManager.instance.currentArea;
		backgroundMaterials[0].SetTexture("_MainTex", displayedArea.background_1.texture);
		if(displayedArea.background_2 != null){
			backgroundMaterials[1].SetTexture("_MainTex", displayedArea.background_2.texture);
		} else {
			backgroundMaterials[1].SetTexture("_MainTex", defaultTexture.texture);
		}
		if(displayedArea.background_3 != null){
			backgroundMaterials[2].SetTexture("_MainTex", displayedArea.background_3.texture);
		} else {
			backgroundMaterials[2].SetTexture("_MainTex", defaultTexture.texture);
		}
		if(displayedArea.background_4 != null){
			backgroundMaterials[3].SetTexture("_MainTex", displayedArea.background_4.texture);
		} else {
			backgroundMaterials[3].SetTexture("_MainTex", defaultTexture.texture);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(AreaManager.instance.currentArea != displayedArea){
			displayedArea = AreaManager.instance.currentArea;
			backgroundMaterials[0].SetTexture("_MainTex", displayedArea.background_1.texture);
			if(displayedArea.background_2 != null){
				backgroundMaterials[1].SetTexture("_MainTex", displayedArea.background_2.texture);
			} else {
				backgroundMaterials[1].SetTexture("_MainTex", defaultTexture.texture);
			}
			if(displayedArea.background_3 != null){
				backgroundMaterials[2].SetTexture("_MainTex", displayedArea.background_3.texture);
			} else {
				backgroundMaterials[2].SetTexture("_MainTex", defaultTexture.texture);
			}
			if(displayedArea.background_4 != null){
				backgroundMaterials[3].SetTexture("_MainTex", displayedArea.background_4.texture);
			} else {
				backgroundMaterials[3].SetTexture("_MainTex", defaultTexture.texture);
			}
		}
		if(AreaManager.instance.areaState == AreaState.Walking){
			
			if(Player.instance.playerData.currentHappiness <= (int)HappinessState.Meh){
				Debug.Log("Not moving due to Happiness");
			} else {
				for(int i = 0; i < backgroundMaterials.Count; i++){
					scrollOffsets[i] += (Time.deltaTime * scrollSpeeds[i]) * (AreaManager.instance.travelModifier+1);
					if(scrollOffsets[i] > 1){
						//this keeps the offset between 0 and 1
						scrollOffsets[i] = scrollOffsets[i] - 1;
					}
					backgroundMaterials[i].SetTextureOffset("_MainTex", new Vector2(scrollOffsets[i], 0));
				}
			}
		}
	}

	public static float FloorToDecimal(float aValue, int aDigits)
	{
		float m = Mathf.Pow(10,aDigits);
		aValue *= m;
		aValue = Mathf.Floor(aValue);
		return aValue / m;
	}
}
