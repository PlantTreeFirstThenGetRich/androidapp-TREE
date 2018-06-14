using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

	public GameObject Menu1;

	public GameObject Menu2;

	public int currentPage = 0;

	public int MAX_PAGE = 1;

	// coins
	public Text OwnLeavesNumberText;

	// item
	public Text ItemName;
	
	public Text ItemPrice;

	public Text ItemDescription;

	public ItemDataFormat itemData;
	
	// Use this for initialization
	void Start () {
		this.Menu1 = GameObject.Find("Menu1");
		this.Menu2 = GameObject.Find("Menu2");
		this.Menu2.SetActive(false);
		// load data
		this.OwnLeavesNumberText = GameObject.Find ("LeaveNumber").GetComponent<Text> ();
		this.OwnLeavesNumberText.text = SuperGameMaster.saveData.OwnLeaveNumber.ToString();
		this.itemData = SuperGameMaster.saveData.Item;
	}
	
	// Update is called once per frame
	void Update () {
		// update coins and itemData;
		this.OwnLeavesNumberText.text = SuperGameMaster.saveData.OwnLeaveNumber.ToString();
		this.itemData = SuperGameMaster.saveData.Item;
		switch(currentPage){
			case 0: 
				Menu1.SetActive(true);
				Menu2.SetActive(false);
				
				break;
		}
		// handle onStock
	}

	public void OnClickPageUp() {	
		if(currentPage+1>MAX_PAGE){
			// handle max_page 
		}
		else {
			currentPage+=1;
		}
	}

	public void OnClickPageDown() {
		if(currentPage-1<0){
			// handle min_page
		}
		else {
			currentPage-=1;
		}
	}

	public void OnClickBuy() {	
		// check coins 
		if(SuperGameMaster.saveData.OwnLeaveNumber >= itemData.price){
			// success
			// update coins and itemStatus
			SuperGameMaster.saveData.OwnLeaveNumber -= itemData.price;
			SuperGameMaster.saveData.Item.onStock = false;
			SuperGameMaster.SaveDataToFile ();
		}
		else {
			// failed
			// display err message
			
			//DEBUG
			SuperGameMaster.saveData.OwnLeaveNumber += 50;
			SuperGameMaster.SaveDataToFile ();
		}
	}

}
