using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClick : MonoBehaviour {
    public Image image;
    public GridPanel panel1;
    public GridPanel panel2;
    public GridPanel panel3;

    public void Click()
    {
        image.gameObject.SetActive(true);
        SuperGameMaster.saveData.Item.isUsed = true;
        SuperGameMaster.SaveDataToFile ();
        panel1.DestroyItems();
        panel2.DestroyItems();
        panel3.DestroyItems();
        //panel1.setEmptyNull();
        //panel2.setEmptyNull();
        //panel3.setEmptyNull();
        CanvasManager.Instance.LoadItems();
    }
}
