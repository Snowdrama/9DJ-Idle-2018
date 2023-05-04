using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct SoundEffect{
	public string name;
	public AudioClip clip;
}

public class AudioManager : MonoBehaviour {
	public static AudioManager instance;
	public GameObject soundEffectPrefab;
	public GameObject soundEffectPrefabLocal;
	public List<SoundEffect> clipList;
	public Dictionary<string, AudioClip> clips;
	void Start(){
		soundEffectPrefab = soundEffectPrefabLocal;
		clips = new Dictionary<string, AudioClip>();
		foreach(SoundEffect se in clipList){
			clips.Add(se.name, se.clip);
		}
	}

	public static void PlaySound(AudioClip clip){
		AudioSource.PlayClipAtPoint(clip, Vector3.zero, PlayerPrefs.GetFloat("Volume", 1.0f));
	}
	public static void PlaySound(AudioClip clip, Vector3 pos){
		AudioSource.PlayClipAtPoint(clip, pos, PlayerPrefs.GetFloat("Volume", 1.0f));
	}
	public static void PlaySound(string clip){
		if(instance.clips.ContainsKey(clip)){
			AudioSource.PlayClipAtPoint(instance.clips[clip], Vector3.zero, PlayerPrefs.GetFloat("Volume", 1.0f));
		}
	}
	public static void PlaySound(string clip, Vector3 pos){
		if(instance.clips.ContainsKey(clip)){
			AudioSource.PlayClipAtPoint(instance.clips[clip], pos, PlayerPrefs.GetFloat("Volume", 1.0f));
		}
	}


}
