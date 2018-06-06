using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwnLeavesNumberUpdate : MonoBehaviour {
	Text OwnLeavesNumberText;
	// Use this for initialization
	void Start () {
		this.OwnLeavesNumberText = GameObject.Find ("LeaveNumber").GetComponent<Text> ();
		this.OwnLeavesNumberText.text = SuperGameMaster.saveData.OwnLeaveNumber.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		this.OwnLeavesNumberText.text = SuperGameMaster.saveData.OwnLeaveNumber.ToString();
	}
}
