using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WaterButton : MonoBehaviour {
	public GameObject MessageBox;
	public GameObject instant;
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
			SuperGameMaster.SaveDataToFile ();
			WaterButton.showMessage ("You justed watered and Your tree grow well.");
		} else {
			// message box notice can't water
			Debug.Log("[WaterButton]Can't water now.");
			WaterButton.showMessage ("Sorry, you can't water now.");
			Debug.Log("[WaterButton]Show can't water message.");
		}
	}

	public static void showMessage(string message) {
		GameObject MessageBox = Resources.Load<GameObject>("shop/prefab/messagebox"); 
		GameObject instant = GameObject.Instantiate (MessageBox);
		instant.GetComponent<Transform> ().SetParent (GameObject.Find ("PacketCanvas").GetComponent<Transform> (), true);
		instant.transform.localScale = Vector3.one;
		instant.transform.localPosition = Vector3.zero;
		Text tips = instant.transform.Find ("Text").GetComponent<Text> ();
		tips.text = message;
		Destroy(instant, 2);
	}
}
