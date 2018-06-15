using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

	public int currentPage = 0;

	public int MAX_PAGE = 1;

	// item
	public GameObject ItemImage;
	Image itemImage;

	public GameObject ItemName;
	Text ItemNameText;
	
	public GameObject ItemPrice;
	Text ItemPriceText;

	public GameObject ItemDescription;
	Text ItemDescriptionText;

	public ItemDataFormat itemData;

	public GameObject OutOfStock;
	Image OutOfStockImage;
	
	public GameObject BuyButton;
	Button btn;

	// Use this for initialization
	void Start () {
		// load data
		this.itemData = SuperGameMaster.saveData.Item;
		this.itemImage = ItemImage.GetComponent<Image> ();
		this.ItemNameText = GameObject.Find ("ItemNameText").GetComponent<Text> ();
		this.ItemNameText.text = SuperGameMaster.saveData.Item.name;

		this.ItemPriceText = ItemPrice.GetComponent<Text> ();
		this.ItemPriceText.text = SuperGameMaster.saveData.Item.price.ToString();
		this.ItemDescriptionText = ItemDescription.GetComponent<Text> ();
		this.ItemDescriptionText.text = "A nest.";// TODO: Add description to ItemDataFormat

		this.OutOfStockImage = OutOfStock.GetComponent<Image> ();
		
		this.btn = BuyButton.GetComponent<Button> ();

		this.ItemName.SetActive(true);
		this.ItemImage.SetActive(true);
		if(this.itemData.onStock){
			this.OutOfStock.SetActive(false);
			this.ItemPrice.SetActive(true);
			this.BuyButton.SetActive(true);
		}
		else {
			this.OutOfStock.SetActive(true);
			this.ItemPrice.SetActive(false);
			this.BuyButton.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// update itemData;
		this.itemData = SuperGameMaster.saveData.Item;
		switch(this.currentPage){
			case 0:
			this.ItemName.SetActive(true);
			this.ItemImage.SetActive(true);
			this.ItemDescription.SetActive(true);
			if(this.itemData.onStock){
				// display item image
				this.OutOfStock.SetActive(false);
				this.ItemPrice.SetActive(true);
				this.BuyButton.SetActive(true);
			}
			else {
				// display out of stock image
				this.OutOfStock.SetActive(true);
				this.ItemPrice.SetActive(false);
				this.BuyButton.SetActive(false);
			}
			break;
			case 1:
			this.ItemName.SetActive(false);
			this.ItemImage.SetActive(false);
			this.OutOfStock.SetActive(false);
			this.ItemDescription.SetActive(false);
			this.ItemPrice.SetActive(false);
			this.BuyButton.SetActive(false);
			break;
			default:break;
		}
	}

	public void OnClickPageUp() {	
		if(currentPage+1>MAX_PAGE){
			// handle max_page 
			// DEBUG
			SuperGameMaster.saveData.Item.onStock = true;
			SuperGameMaster.saveData.OwnLeaveNumber += 50;
			SuperGameMaster.SaveDataToFile ();
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
	
		}
	}

}
