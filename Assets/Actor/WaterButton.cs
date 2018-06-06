using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WaterButton : MonoBehaviour {
	//private Button btn;
	// Use this for initialization
	void Start () {
		//btn = GameObject.Find("WaterButton").getComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick () {
		if (SuperGameMaster.WaterNeeded == true) {
			SuperGameMaster.WaterNeeded = false;
			DateTime currentTime = DateTime.Now;
			SuperGameMaster.saveData.lastWaterTime = new DateTime (currentTime.Year, 
				currentTime.Month, currentTime.Day, 
				currentTime.Hour, currentTime.Minute, 
				currentTime.Second, currentTime.Millisecond);
		} else {
			// message box notice can't water
			Debug.Log("Can't water now.");
		}
	}
}
