using UnityEngine;
using System.Collections;

public class UIMaster : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public virtual void changeScene (Scenes _nextScene) {
		this.GameMaster.GetComponent<GameMaster> ().ChangeSceneUpdateFlag (_nextScene);
	}

	public GameObject GameMaster;
}

