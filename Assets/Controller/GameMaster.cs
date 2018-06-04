using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public virtual void ChangeSceneUpdateFlag(Scenes _nextScene) {
		this.nextScene = _nextScene;
		this.callSceneChange = true;
	}

	public virtual void ChangeScene (Scenes _nextScene) {
		this.nextScene = _nextScene;
		SuperGameMaster.
		this.nowSceneChange = true;
		this.callSceneChange = false;
	}

	protected Scenes nextScene;
	protected bool callSceneChange;
	protected bool nowSceneChange;
}

