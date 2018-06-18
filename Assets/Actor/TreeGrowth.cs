using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowth : MonoBehaviour {
	public Sprite[] sprites;
	SpriteRenderer spriteRenderer;

	void Start () {
		int growth_second = SuperGameMaster.saveData.treeGrowthTimeSec;
		spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
        Debug.Log("TreeGrowth.cs start" + sprites.Length);
        int growth_days = (int)(growth_second / 60 / 60 / 24);
		// test minute
		//int growth_days = (int)(growth_second / 60);
		Debug.Log ("[TreeGrowth] in start: the growth_days is: " + growth_days);
		if (growth_days <= 1) {
			Debug.Log ("[TreeGrowth] in start: 0 tree");
            Debug.Log("TreeGrowth.cs " + sprites.Length);
            spriteRenderer.sprite = sprites [0];
		} else if (3 >= growth_days && growth_days > 1) {
			Debug.Log ("[TreeGrowth] in start: 1 tree");
            Debug.Log("TreeGrowth.cs " + sprites.Length);
            spriteRenderer.sprite = sprites [1];
		} else if (growth_days > 3) {
			Debug.Log ("[TreeGrowth] in start: 2 tree");
            Debug.Log("TreeGrowth.cs " + sprites.Length);
            spriteRenderer.sprite = sprites [2];
		}
	}
}
