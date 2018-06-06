using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowth : MonoBehaviour {
	public Sprite[] sprites;
	SpriteRenderer spriteRenderer;

	void Start () {
		int growth_second = (int)SuperGameMaster.RegisterTime_SpanSec ();
		spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
		int growth_days = (int)(growth_second / 60 / 60 / 24);
		Debug.Log ("[TreeGrowth] in start: the growth_days is: " + growth_days);
		if (growth_days <= 3) {
			spriteRenderer.sprite = sprites [0];
		} else if (7 >= growth_days && growth_days > 3) {
			spriteRenderer.sprite = sprites [1];
		} else if (10 >= growth_days && growth_days > 7) {
			spriteRenderer.sprite = sprites [2];
		} else if (growth_days > 10) {
			spriteRenderer.sprite = sprites [3];
		}
	}
}
