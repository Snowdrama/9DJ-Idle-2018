using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Area", menuName = "Area")]
public class Area : ScriptableObject{
	//An area is a walkable section of the game. 
	public string areaSlug = "forest"; //Forest, Beach, Tower
	public Sprite icon; //Icon representing the area? 
	public Sprite background_1; //tileable background
	public Sprite background_2; //tileable background
	public Sprite background_3; //tileable background
	public Sprite background_4; //tileable background
	public float areaLength = 3600.0f; //how long the area is. 3600 = 1 hour at 1 unit per second
	public float encounterItterationTime = 600.0f; //the number of seconds before we test for an encounter
	public float chanceForEncounter = 50f; //Random.Range(0.0f,100.0f) < chanceForEncounter = encounter

	public bool finalArea;
	public Area northPath;
	public Area southPath;

	public List<AreaEvent> events;
}
