using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {

	public bool inAction;

	public AgentAction primaryAction;
	public AgentAction secondaryAction;
	public AgentAction supportAction;
	public AgentAction rangedAction;

	public InventoryManager inventory;
	// Use this for initialization
	void Start () {
		thisController = GetComponent<ActionController>();
		if(!inventory)inventory = GetComponent<InventoryManager>();
		if(primaryAction) primaryAction.controller = thisController;
		if(secondaryAction) secondaryAction.controller = thisController;
		if(supportAction) supportAction.controller = thisController;
		if(rangedAction) rangedAction.controller = thisController;
	}

	public void PerformAction(ActionObject hitObject, bool pressed){
		if(hitObject == null){
			if(primaryAction)primaryAction.CheckAction(null, pressed);
			return;
		}

		if(pressed){
			switch(hitObject.actionGroupPress){
				case ActionGroup.primary:
					if(primaryAction)primaryAction.CheckAction(hitObject,pressed);
				break;
				case ActionGroup.secondary:
					if(secondaryAction)secondaryAction.CheckAction(hitObject,pressed);
				break;
				case ActionGroup.support:
					if(supportAction)supportAction.CheckAction(hitObject,pressed);
				break;
				case ActionGroup.ranged:
					if(rangedAction)rangedAction.CheckAction(hitObject,pressed);
				break;
				case ActionGroup.none:
					SimpleAction(hitObject,pressed);
				break;
			}
		}

		if(!pressed){
			switch(hitObject.actionGroupTap){
				case ActionGroup.primary:
					if(primaryAction)primaryAction.CheckAction(hitObject,pressed);
					break;
				case ActionGroup.secondary:
					if(secondaryAction)secondaryAction.CheckAction(hitObject,pressed);
					break;
				case ActionGroup.support:
					if(supportAction)supportAction.CheckAction(hitObject,pressed);
					break;
				case ActionGroup.ranged:
					if(rangedAction)rangedAction.CheckAction(hitObject,pressed);
				break;
				case ActionGroup.none:
					SimpleAction(hitObject,pressed);
				break;
			}
		}
	}

	public void SimpleAction(ActionObject act, bool pressed) {
		act.DoAction(thisController, pressed); 
	}
		
	private ActionController thisController;

}

