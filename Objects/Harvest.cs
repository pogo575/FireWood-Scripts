using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.StateMachines;
public class Harvest : ActionObject {
	public string inputEvent = "Remove";
	private FSMOwner fsm;
	public ItemID itemID;

	public int harvestQuantity = 5;
	public int harvestPerHit = 1;

	void Awake(){
		fsm = GetComponent<FSMOwner>();
		Init();	
	}

	void Init(){

	}

	public override void DoAction (ActionController cont , bool pressed) {
		if(cont){
			
			ActionTap(cont);	
		}
		base.DoAction(cont,pressed);
	}

	public override void ActionTap(ActionController cont){

		if(harvestQuantity >0){
			if(cont)
				cont.inventory.Pickup(thisObject, itemID, harvestPerHit);
			harvestQuantity --;
			Remove();
		}

		base.ActionTap(cont);
	}

	public override void ActionPress(ActionController cont){

		base.ActionPress(cont);
	}

	public void Remove(){
		if(harvestQuantity <=0){
			if(fsm)fsm.SendEvent(inputEvent);
		}
	}

	public void EndRemoval(){
		gameObject.SetActive(false);
	}
}
