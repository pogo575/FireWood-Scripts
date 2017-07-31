using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlace : ActionObject {


	public ItemID acceptItem;
	public float healthPerWood = 20f;
	public int healthTakePerTorchLight = 50;
	public int healthForTorchPerLight = 100;
	public LightSource thisLightSource;
	void Start () {

		thisLightSource.Burn();
	}
	
	public override void DoAction (ActionController cont , bool pressed) {
		if(!pressed)
			ActionTap(cont);
		if(pressed)
			ActionPress(cont);
		base.DoAction(cont,pressed);
	}

	public override void ActionTap(ActionController cont){
		InputObject(cont,false);
		base.ActionTap(cont);
	}

	public override void ActionPress(ActionController cont){
		InputObject(cont,true);
		base.ActionPress(cont);
	}


	public void InputObject(ActionController cont, bool toTorch){

		switch(acceptItem){
			case ItemID.none:

				break;
			case ItemID.wood:
				if(toTorch){
					LightTorch(cont.inventory.torch);
				}


				else if(cont.inventory.Remove(acceptItem)){
					thisLightSource.health += healthPerWood;
				}
					
			break;
		}

		thisLightSource.Burn();
	}


	private float tFireMax;

	public void LightTorch(LightSource torch){
		if(!torch)
			return;

		tFireMax = thisLightSource.health;

		if(tFireMax > healthTakePerTorchLight && torch.health < torch.maxHealth){
			thisLightSource.health -= healthTakePerTorchLight;
			torch.health += healthForTorchPerLight;
		}

		torch.Burn();
	}


}
