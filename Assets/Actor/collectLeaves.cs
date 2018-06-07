using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collectLeaves : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick () {
		// modify correspond SuperGameMaster saveData value
		// notice: when you  create the leaf object, the name format should be leaf_0 leaf_1 leaf_2
		int nameLength = base.name.Length;
		int leafindex = int.Parse (base.name.Substring (nameLength - 1, 1));
		SuperGameMaster.saveData.LeafList [leafindex].newFlag = false;
		SuperGameMaster.saveData.LeafList [leafindex].timeSpanSec = SuperGameMaster.InitLeafGenerateTime;
		// destory the leaf object
		Destroy (this.gameObject);
		SuperGameMaster.SaveDataToFile ();
	}
}
