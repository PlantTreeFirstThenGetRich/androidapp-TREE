using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTreeButton : MonoBehaviour {

	public void OnClick() {
		Debug.Log ("[ToTreeButton] click the button, and change to scene mainscene");
		SuperGameMaster.setNextScene (Scenes.MainScene);
	}
}
