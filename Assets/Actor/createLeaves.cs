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
				this.tempLeafButton = (Button)Instantiate (LeafButtonTemplate);
				this.tempLeafButton.GetComponent<Transform> ().SetParent (GameObject.Find ("Canvas").GetComponent<Transform> (), true);
				this.tempLeafButton.name = "leaf_" + i;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
