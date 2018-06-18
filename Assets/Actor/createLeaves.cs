using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createLeaves : MonoBehaviour {
	public Button LeafButtonTemplate;
	private Button tempLeafButton;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < SuperGameMaster.InitTotalLeadNumber; i++) {
			LeafDataFormat leafDataFormat = SuperGameMaster.saveData.LeafList [i];
			if (leafDataFormat.newFlag == true) {
				Debug.Log ("[createLeaves] in Start: create on leaf.");
				this.tempLeafButton = (Button)Instantiate (LeafButtonTemplate);
				this.tempLeafButton.GetComponent<Transform> ().SetParent (GameObject.Find ("UpCanvas").GetComponent<Transform> (), true);
				//this.tempLeafButton.GetComponent<Transform> ().position = new Vector3 (leafDataFormat.x, leafDataFormat.y, 0);
				//this.tempLeafButton.GetComponent<Transform> ().localScale *= 0.05f;
				this.tempLeafButton.GetComponent<Transform> ().localScale = new Vector3 (3, 2, 0);

				this.tempLeafButton.GetComponent<Transform> ().position = new Vector3 (leafDataFormat.x/100, -3.67f, 0);
				Debug.Log ("the position of the leaf is: x: " + leafDataFormat.x + ", y: " + leafDataFormat.y);
				this.tempLeafButton.name = "leaf_" + i;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
