using System;
using System.Security.Cryptography;

[Serializable]
public class ItemDataFormat
{
	public ItemDataFormat () {
    }

    public ItemDataFormat (ItemDataFormat ori) {
        this.name = ori.name;
        this.price = ori.price;
        this.onStock = ori.onStock;
        this.isUsed = ori.isUsed;
    }
    
    public String name;
    public int price;
	public bool onStock;
	public bool isUsed;

}


