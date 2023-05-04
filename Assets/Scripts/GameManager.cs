using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum TransitionState{
	Waiting,
	FadingIn,
	FadingOut
}

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	public string targetScene = "";
	public Image transitionImage;
	public Color transitionImageColor = Color.black;
	public float transitionAlpha = 1.0f;
	public TransitionState transitionState;
	public List<string> scenesInBuild;
	public bool soundInPlaying;
	public AudioClip fadeInSound;
	public bool soundOutPlaying;
	public AudioClip fadeOutSound;
	void Start () {
		if(instance != null){
			Destroy(this.gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(this);

			
			scenesInBuild = new List<string>();
			for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
			{
				string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
				int lastSlash = scenePath.LastIndexOf("/");
				scenesInBuild.Add(scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1));
			}
		}
	}

	void Update()
	{
		switch(instance.transitionState){
			case TransitionState.FadingIn:
				if(!soundInPlaying){
					soundInPlaying = true;
					//AudioManager.PlaySound(fadeInSound);
				}
				if(instance.transitionAlpha < 1.0f){
					instance.transitionAlpha += Time.deltaTime;
				} 
				
				if(instance.transitionAlpha >= 1.0f){
					SceneManager.LoadScene(targetScene);
					soundInPlaying = false;
					instance.transitionState = TransitionState.FadingOut;
				}
				instance.transitionImage.material.SetFloat("_Cutoff", instance.transitionAlpha);
				// instance.transitionImageColor.a = instance.transitionAlpha;
				// instance.transitionImage.color = instance.transitionImageColor;
			break;
			case TransitionState.FadingOut:
			
				if(!soundOutPlaying){
					soundOutPlaying = true;
					//AudioManager.PlaySound(fadeOutSound);
				}

				if(instance.transitionAlpha > 0.0f){
					instance.transitionAlpha -= Time.deltaTime;
				} 
				
				if(instance.transitionAlpha <= 0.0f){
					soundOutPlaying = false;
					instance.transitionState = TransitionState.Waiting;
				}
				instance.transitionImage.material.SetFloat("_Cutoff", instance.transitionAlpha);
				// instance.transitionImageColor.a = instance.transitionAlpha;
				// instance.transitionImage.color = instance.transitionImageColor;
			break;
			case TransitionState.Waiting:
			break;
		}
	}

	public static void GoToScene(string sceneName){
		Debug.Log("Trying To Load: " + sceneName);
		if(instance.scenesInBuild.Contains(sceneName)){
			instance.targetScene = sceneName;
			instance.transitionState = TransitionState.FadingIn;
		} else {
			Debug.LogWarning("Scene Name: " + sceneName + " Doesn't Exist!");
			Debug.Log("Going to Walking Scene");
			instance.targetScene = "WalkingScene";
			instance.transitionState = TransitionState.FadingIn;
		}
	}


}
