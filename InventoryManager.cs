using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour {
	public LightSource torch;
	public InventoryItem wood;

	void Start(){
		wood.UpdateText();
	}


	public bool Pickup(ActionObject act, ItemID id, int iValue) {
		switch(id){
			case ItemID.none:

				break;
			case ItemID.wood:
				if(wood.current<wood.max){
					wood.current += iValue;
					wood.UpdateText();
					return true;
				}
				break;
		}
		return false;
	}

	public bool Remove(ItemID itm){
		switch(itm){
			case ItemID.none:
			break;
			case ItemID.wood:
				if(wood.current > 0){
					wood.current --;
					wood.UpdateText();
					return true;
				}
			break;
		}
		return false;

	}
}

[System.Serializable]
public enum ItemID{
	none,
	wood
}

[System.Serializable]
public class InventoryItem{
	public int current;
	public int max;
	public string textPrefix;
	public Text text;

	public void UpdateText(){
		text.text = (textPrefix + current+"/"+max);
	}
}