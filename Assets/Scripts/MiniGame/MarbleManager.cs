using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MarbleType{
	Red    = 0,
	Orange = 1,
	Green  = 2,
	Yellow = 3,
	Cyan   = 4,
	Blue   = 5,
	Purple = 6,
	Pink   = 7,
}

public struct MarbleData{

}

public class MarbleManager : MonoBehaviour {
	public MarbleManager instance;
	public static Dictionary<MarbleType, List<Marble>> marbles;
	public float chainThreshold = 1.1f;
	
	bool gridChecked = true;
	// Use this for initialization
	void Awake () {
		if(instance != null){
			Destroy(this.gameObject);
		} else {
			instance = this;
			marbles = new Dictionary<MarbleType, List<Marble>>();
			foreach(MarbleType t in Enum.GetValues(typeof(MarbleType))){
				marbles.Add(t, new List<Marble>());
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		bool allSettled = true;
		foreach(MarbleType t in Enum.GetValues(typeof(MarbleType))){
			List<Marble> l = marbles[t];
			foreach(Marble mar in l){
				if(!mar.settled){
					allSettled = false;
				}
			}
		}

		if(!gridChecked && allSettled){
			foreach(MarbleType t in Enum.GetValues(typeof(MarbleType))){
				List<Marble> listOfMarbles = marbles[t];
				foreach(Marble currentMarble in listOfMarbles){
					List<Marble> chain = ChainMarble(currentMarble, listOfMarbles, new List<Marble>());
					if(chain.Count >= 3){
						Debug.Log("Found Chain!");
						foreach(Marble poppedMarble in chain){
							poppedMarble.popped = true;
						}
					}
				}
				foreach(Marble marble in listOfMarbles){
					marble.counted = false;
				}
			}

			Debug.Log("All Settled");
			gridChecked = true;
		} else if(gridChecked && !allSettled){
			gridChecked = false;
		}
	}

	public List<Marble> ChainMarble(Marble currentMarble, List<Marble> allMarbles, List<Marble> currentChain){
		foreach(Marble m in allMarbles){
			if(m != null){
				if(!m.counted && Vector3.Distance(currentMarble.gameObject.transform.position, m.gameObject.transform.position) < chainThreshold){
					if(!currentChain.Contains(m)){
						m.counted = true;
						currentChain.Add(m);
						currentChain = ChainMarble(m, allMarbles, currentChain);
					}
				}
			}
		}
		return currentChain;
	}
}
