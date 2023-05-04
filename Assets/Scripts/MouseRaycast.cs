using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycast : MonoBehaviour {
	public GameObject hitObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
			touchPosition =  Camera.main.ScreenToWorldPoint(touchPosition);
			RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector3.forward, 100);
			if(hit.collider != null){
				hitObject = hit.collider.gameObject;
			}

		} else if(Input.GetMouseButtonDown(0)){
			//Debug.Log("Mouse Raycasting");
            Vector2 touchPosition = Input.mousePosition;
			touchPosition =  Camera.main.ScreenToWorldPoint(touchPosition);
			RaycastHit2D hit = Physics2D.Raycast(touchPosition, -Vector3.forward, 100);
			DebugExtension.DebugArrow(touchPosition, Vector3.forward * 100);
			if(hit.collider != null){
				hitObject = hit.collider.gameObject;
				if(hitObject.GetComponent<SpriteButton>() != null){
					hitObject.GetComponent<SpriteButton>().Press();
				}
			}
		}
	}
}
