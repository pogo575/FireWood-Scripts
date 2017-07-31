using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParadoxNotion;
using NodeCanvas.StateMachines;
using MovementEffects;

public class AgentAction : MonoBehaviour {
	public ActionController controller;
	public float actionLength = 0.65f;
	public float actionTime;
	public float actionTimer;
	public string inputEvent = "Fire1";
	private FSMOwner fsm;
	[SerializeField]
	private ActionObject _actionObject;

	public ActionObject actionObject{
		get {return _actionObject;}
		set {_actionObject = value;}
	}

	[SerializeField]
	private bool _actionPressed;

	public bool actionPressed{
		get {return _actionPressed;}
		set {_actionPressed = value;}
	}

	void Start () {
		fsm = GetComponent<FSMOwner>();
	}

	public void CheckAction(ActionObject act, bool pressed) {
		actionPressed = pressed;
		actionObject = act;
		fsm.SendEvent(inputEvent);
		if(!controller.inAction)
			Timing.RunCoroutine(_StartAction());
	}

	public void DoAction(){
		if(actionObject)
			actionObject.DoAction(controller,actionPressed);
		actionObject = null;

	}
		

	IEnumerator<float> _StartAction(){
		controller.inAction = true;
		actionTimer = 0f;
		while (actionTimer < actionLength){
			actionTimer += Time.deltaTime;
			actionTime = actionTimer/actionLength;
			yield return 0f;
		}
		StopAction();
		yield break;
	}

	public void StopAction(){
		controller.inAction = false;
	}

}