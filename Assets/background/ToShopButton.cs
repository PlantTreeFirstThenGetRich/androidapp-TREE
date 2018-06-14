using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToShopButton : MonoBehaviour {

	public void OnClick (){
		Debug.Log ("[ToShopButtonClick] click the button, and change to scene shopscene");
		SuperGameMaster.setNextScene (Scenes.Shop);
	}
}
