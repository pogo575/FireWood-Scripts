using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;


public class ActionObject : MonoBehaviour {
	
	public ActionGroup actionGroupTap;
	public Effect tapEffect;
	public ActionGroup actionGroupPress;
	public Effect pressEffect;
	public float autoMoveDistance = 2f;

	public string debugActionMessage = "Test";
	// Use this for initialization
	void Awake () {
		thisObject = GetComponent<ActionObject>();	
		collider = GetComponent<Collider>();

		Init();
	}

	void Init(){
		
	}

	public virtual void DoAction (ActionController cont, bool pressed) {
		if(!effectRoot)
			effectRoot = transform;
		if(pressed)
			pressEffect.Play(effectRoot.position);
		if(!pressed)
			tapEffect.Play(effectRoot.position);
		
		Debug.Log(debugActionMessage);
	}

	public virtual void ActionTap(ActionController cont){

	}

	public virtual void ActionPress(ActionController cont){

	}



	[HideInInspector]
	public Collider collider;
	[HideInInspector]
	public ActionObject thisObject;
	public Transform effectRoot;
}

[System.Serializable]
public enum ActionGroup{
	primary,
	secondary,
	support,
	ranged,
	none
}

[System.Serializable]
public class Effect{

	public SECTR_AudioCue sound;
	public ParticleSystem particle;
	private SECTR_AudioCueInstance audioInstance;
	public void Play(bool doPlay, Vector3 atPosition){
		if(doPlay){
			if(sound)
				audioInstance = SECTR_AudioSystem.Play(sound, atPosition, false);
			if(particle)
				particle.Play();
		}
		else {
			if(sound)
				audioInstance.Stop(true);
			if(particle)
				particle.Stop();
		}
	}

	public void Play(Vector3 atPosition){
		if(sound)
			audioInstance = SECTR_AudioSystem.Play(sound, atPosition, false);
		if(particle)
			particle.Play();
	}
}
