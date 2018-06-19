using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClick : MonoBehaviour {
    public Image image;
    public GridPanel panel1;

    public void Click()
    {
        image.gameObject.SetActive(true);
        SuperGameMaster.saveData.Item.isUsed = true;
        SuperGameMaster.SaveDataToFile ();
        panel1.DestroyItems();
    }
}
