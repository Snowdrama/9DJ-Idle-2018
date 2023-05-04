using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSlidingPanel : SpriteButton {
	public SlidingPanel panelSlide;
	public AudioClip panelSlideInSound;
	public AudioClip panelSlideOutSound;
	public override void Press(){
		Debug.Log("SlidingPanel");
		if(panelSlide != null){
			panelSlide.showingPanel = !panelSlide.showingPanel;
		}
		if(panelSlide.showingPanel){
			if(panelSlideInSound != null){
				AudioManager.PlaySound(panelSlideInSound);
			}
		} else {
			if(panelSlideInSound != null){
				AudioManager.PlaySound(panelSlideOutSound);
			}
		}
	}
}
