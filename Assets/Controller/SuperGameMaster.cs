using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SuperGameMaster : MonoBehaviour {
	private void Awake() {
		Debug.unityLogger.logEnabled = true;
		// init while first time creating SuperMaster
		if (!SuperGameMaster.create_SuperMaster) {
			// this object can't be destoryed by the scence change
			DontDestroyOnLoad (this);

			SuperGameMaster.create_SuperMaster = true;
			Application.targetFrameRate = 60;
			// Subscribe to this event to get notified when the active Scene has changed.
			SceneManager.activeSceneChanged += new UnityAction<Scene, Scene> (this.OnActiveSceneChanged);
			// Add a delegate to this to get notifications when a Scene has loaded.
			SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode> (this.OnSceneLoaded);
			SceneManager.sceneUnloaded += new UnityAction<Scene> (this.OnSceneUnloaded);
			SuperGameMaster.saveMgr = base.GetComponent<SaveManager> ();
			SuperGameMaster.nowLoading = false;
			base.StartCoroutine ("initLoading");
		} else {
			Destroy (base.gameObject);
		}

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	private void OnActiveSceneChanged(Scene i_preChangedScene, Scene i_postChangedScene) {
		Debug.LogFormat ("[SuperGameMaster] scene change : preChangedScene: {0} postChangedScene: {1}",
			new object [] {
				i_preChangedScene.name,
				i_postChangedScene.name
			});
	}

	private void OnSceneLoaded (Scene i_loadedScene, LoadSceneMode i_mode) {}
	private void OnSceneUnloaded (Scene i_unloaded) {}

	public void init () {
		SuperGameMaster.GameTimer = 0f;
	}

	public static void LoadData () {
		SuperGameMaster.saveData = SuperGameMaster.saveMgr.LoadData ("GameData.sav");
		SuperGameMaster.deviceTime = DateTime.Now;
		if (SuperGameMaster.saveData.lastDateTime == new DateTime (1970, 1, 1)) {
			SuperGameMaster.saveData.lastDateTime = new DateTime (SuperGameMaster.deviceTime.Year, 
				SuperGameMaster.deviceTime.Month, SuperGameMaster.deviceTime.Day, 
				SuperGameMaster.deviceTime.Hour, SuperGameMaster.deviceTime.Minute, 
				SuperGameMaster.deviceTime.Second, SuperGameMaster.deviceTime.Millisecond); 
		}
		if (SuperGameMaster.saveData.lastDateTime <= SuperGameMaster.deviceTime) {
			SuperGameMaster.timeError = false;
			SuperGameMaster.timeErrorString = string.Empty;
		} else {
			SuperGameMaster.timeError = true;
			string str = string.Concat (new object [] {
				SuperGameMaster.saveData.lastDateTime.Year,
				SuperGameMaster.saveData.lastDateTime.Month,
				SuperGameMaster.saveData.lastDateTime.Day,
				SuperGameMaster.saveData.lastDateTime.ToShortTimeString()
			}
			);
			SuperGameMaster.timeErrorString = "[SuperGameMaster] in LoadData: return to last time, the next update is after" + str;
		}
		Debug.Log (string.Concat (new object [] {
			"[SuperGameMaster] the last time you enter the game is:",
			SuperGameMaster.saveData.lastDateTime.ToString(),
			"delta-T is:",
			SuperGameMaster.LastTime_SpanSec(),
			"second"
		}));
	}


	// time to last open the game
	public static float LastTime_SpanSec () {
		if (SuperGameMaster.timeError) {
			return 0f;
		}
		return Mathf.Clamp ((float)(SuperGameMaster.deviceTime - SuperGameMaster.saveData.lastDateTime)
			.TotalSeconds, 0f, 2592000f);
	}

	// TODO: not totally understand the meaning of timeSpanSec
	// maybe the time to next generate
	public static void MathTime_Leaf (int addTimer) {
		// the min time in all leaf
		int num = int.MaxValue;
		foreach (LeafDataFormat leafDataFormat in SuperGameMaster.saveData.LeafList) {
			if (leafDataFormat.timeSpanSec > 0) {
				leafDataFormat.timeSpanSec -= addTimer;
				if (leafDataFormat.timeSpanSec <= 0) {
					leafDataFormat.newFlag = true;
					leafDataFormat.timeSpanSec--;
				} 
				if (num > leafDataFormat.timeSpanSec && leafDataFormat.timeSpanSec > 0) {
					num = leafDataFormat.timeSpanSec;
				}
			}
		}
		if (num != int.MaxValue) {
			Debug.Log (string.Concat(new object [] {
				"[SuperGameMaster] the time to last open this game is:",
				addTimer,
				"/ to next generate leaf is :",
				num / 3600,
				"hour ",
				num % 3600 / 60,
				"minute ",
				num % 60,
				"second "
			}));
		}
	}

	private IEnumerator initLoading () {
		SuperGameMaster.nowLoading = true;
		SuperGameMaster.LoadingProgress = 0f;
		this.init ();
		// Hold off one frame and proceed to the next.
		yield return null;
		SuperGameMaster.LoadData ();
		SuperGameMaster.LoadingProgress = 30f;
		yield return null;
		if (!SuperGameMaster.timeError) {
			SuperGameMaster.MathTime_Leaf ((int)SuperGameMaster.LastTime_SpanSec ());
			SuperGameMaster.LoadingProgress = 60f;
			yield return null;
		}
		SuperGameMaster.LoadingProgress = 100f;
		yield return null;
		SuperGameMaster.nowLoading = false;
		yield break;
	}

	public static void setNextScene(Scenes _NextScene) {
		SuperGameMaster.NextScene = _NextScene;
	}

	private static bool create_SuperMaster;

	public static Scenes NowScene;
	public static Scenes NextScene = Scenes.NONE;
	public static Scenes PrevScene = Scenes.Main;
	public static Scenes StartScene = Scenes.Main;
	public static Scenes DefaultScene = Scenes.Main;

	public static bool nowLoading;
	public static float LoadingProgress;
	public static float GameTimer;

	public static DateTime deviceTime;
	public static bool timeError;
	public static string timeErrorString;

	public static SaveDataFormat saveData;
	public static SaveManager saveMgr;
}
