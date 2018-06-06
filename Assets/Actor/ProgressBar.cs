using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ProgressBar : MonoBehaviour {
	private Slider m_slider; 

	// Use this for initialization
	void Start () {
		m_slider = base.GetComponent<Slider> ();
	}
	
	// Update is called once per frame
	void Update () {
		m_slider.value = SuperGameMaster.LoadingProgress;
	}
}
