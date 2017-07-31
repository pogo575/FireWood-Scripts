using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MovementEffects;

public class gCursor : MonoBehaviour {
	
	public float interactionDistance = 3f;
	public bool interactionRange;

	public static Vector3 cursorWorldPos;
	public static Vector3 cursorWorldEndPos;

	public Image cursorUISprite;

	public RectTransform cursorRect;
	public Texture2D defaultcursor;
	public float defaultSize = 6f;

	public Texture2D inRangeCursor;
	public Texture2D useCursor;
	public float useSize = 20f;

	public LayerMask castToItemLayer;
	private RaycastHit castHit;
	public Transform hitTransform;
	public static ActionObject hitAction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if(!UpdateMouseCursor() && !gInput.actionController.inAction)
			NoSelection();

		
	}

	public bool UpdateMouseCursor(){
		if(cursorRelativeTransform)
			interactionRange = (Vector3.Distance(cursorWorldEndPos, cursorRelativeTransform.position) < interactionDistance);
	
		if(interactionRange && !gInput.actionController.inAction){
			SetInRangeCursor();

			if(CastMouse(castToItemLayer)){
				hitTransform = castHit.transform;
				if(hitTransform)
					hitAction = hitTransform.GetComponent<ActionObject>();
				if(hitAction){
					SetUseSprite();

					return true;
				}
			}
		}
		else 
			SetDefaultSprite();

		return false;
	}


	public bool CastMouse(LayerMask lyr){
		return (Physics.Linecast(cursorWorldPos, cursorWorldEndPos, out castHit, lyr));
	}

	public void NoSelection(){
		hitTransform = null;
		hitAction = null;
	}

	public void SetInRangeCursor(){
		outOfRangeCursor = false;
		//Cursor.SetCursor(inRangeCursor,Vector2.zero,CursorMode.Auto);
	}


	public void SetUseSprite(){

		//Cursor.SetCursor(useCursor,Vector2.zero,CursorMode.Auto);

		if(cursorRect){
			cursorRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, useSize);
			cursorRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, useSize);
		}

	}

	public bool outOfRangeCursor;
	public void SetDefaultSprite(){

		//Cursor.SetCursor(defaultcursor,Vector2.zero,CursorMode.Auto);
		outOfRangeCursor = true;
		if(cursorRect){
			cursorRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, defaultSize);
			cursorRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, defaultSize);
		}
	}

	public Transform cursorRelativeTransform;
}
