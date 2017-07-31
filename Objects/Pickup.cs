using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : ActionObject {
	public ItemID itemID;
	public int pickupValue = 2;
	void Awake(){
		Init();	
	}

	void Init(){
		
	}

	public override void DoAction (ActionController cont , bool pressed) {
		if(cont){
			if(cont.inventory.Pickup(thisObject, itemID, pickupValue))
				Remove();
		}
		base.DoAction(cont,pressed);
	}

	public override void ActionTap(ActionController cont){
		base.ActionTap(cont);
	}

	public override void ActionPress(ActionController cont){

		base.ActionPress(cont);
	}

	public void Remove(){
		gameObject.SetActive(false);
	}
}
