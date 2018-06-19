using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public GridPanel panel1;
    public GameObject leftPage;
    public GameObject rightPage;
    private static CanvasManager instance;
    public int currentpanel;
    public Image item11;

    // Use this for initialization
    void Start()
    {
        leftPage = GameObject.Find("LeftPage");
        rightPage = GameObject.Find("RightPage");
        Button leftpagebtn = leftPage.GetComponent<Button>();
        Button rightpagebtn = rightPage.GetComponent<Button>();
        leftpagebtn.onClick.AddListener(delegate()
        {
            this.showLeftPage();
        });
        rightpagebtn.onClick.AddListener(delegate ()
        {
            this.showRightPage();
        });
    }

    void Update()
    {
        switch (currentpanel)
        {
            case 1:
                LoadItems();
                break;
            case 2:
                panel1.DestroyItems();
                break;
            case 3:
                panel1.DestroyItems();
                break;
        }
    }
    private void Awake()
    {
        instance = this;
        currentpanel = 1;
    }

    public static CanvasManager Instance
    {
        get{
            return instance;
        }
    }

    public void LoadItems()
    {
        if (item11.transform.childCount >= 1)
        {

        }
        else
        {
            if (SuperGameMaster.saveData.Item.onStock == false && SuperGameMaster.saveData.Item.isUsed == false)
            {
                Transform emptygrid;
                emptygrid = panel1.grids[0];
                Debug.Log(emptygrid);
                GameObject itemprefab = Resources.Load<GameObject>("itempacket/ItemImage");
                string imagepath = "image/Nest";
                Texture2D tt = (Texture2D)Resources.Load(imagepath) as Texture2D;
                Sprite k = Sprite.Create(tt, new Rect(0, 0, tt.width, tt.height), new Vector2(0.5f, 0.5f));
                itemprefab.GetComponent<Image>().sprite = k;
                GameObject itemini = GameObject.Instantiate(itemprefab);
                itemini.transform.SetParent(emptygrid);
                itemini.transform.localPosition = Vector3.zero;
                itemini.transform.localScale = Vector3.one;
            }
        }
    }

    public void InitPacket(bool openornot,GameObject packetcanvas)
    {
        if (openornot)
        {
            ShelterControl.Instance.setShelterPanel();
            packetcanvas.gameObject.SetActive(true);
            this.LoadItems();
        }
        else
        {
            ShelterControl.Instance.cancelShelterPanel();
            packetcanvas.gameObject.SetActive(false);
        }
    }

	public void showLeftPage()
    {
       
        if (currentpanel > 1)
        {
            currentpanel -= 1;
        }
        else
        {

        }
    }
    public void showRightPage()
    {
        if(currentpanel < 3)
        {
            currentpanel += 1;
        }
    }
}