using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noticShowTrigger : MonoBehaviour {
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		this.spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
	}
	// Update is called once per frame
	void Update () {
		if (SuperGameMaster.WaterNeeded) {
			this.spriteRenderer.enabled = true;
		} else {
			this.spriteRenderer.enabled = false;
		}
	}
}
