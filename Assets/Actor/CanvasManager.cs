using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public GridPanel panel1;
    public GridPanel panel2;
    public GridPanel panel3;
    public GameObject leftPage;
    public GameObject rightPage;
    private static CanvasManager instance;
    public int currentpanel;

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
        if(SuperGameMaster.saveData.Item.onStock == false && SuperGameMaster.saveData.Item.isUsed == false)
        {
            Transform emptygrid;
            if (panel1.getFullCount() < 9)
            {
                emptygrid = panel1.getEmptyGrid();
            }
            else if(panel2.getFullCount() < 9)
            {
                emptygrid = panel2.getEmptyGrid();
            }
            else
            {
                emptygrid = panel3.getEmptyGrid();
            }
            GameObject itemprefab = Resources.Load<GameObject>("itempacket/ItemImage");
            string imagepath = "image/"+SuperGameMaster.saveData.Item.name;
            Texture2D tt = (Texture2D)Resources.Load(imagepath) as Texture2D;
            Sprite k = Sprite.Create(tt, new Rect(0, 0, tt.width, tt.height), new Vector2(0.5f, 0.5f));
            itemprefab.GetComponent<Image>().sprite = k;
            GameObject itemini = GameObject.Instantiate(itemprefab);
            itemini.transform.SetParent(emptygrid);
            itemini.transform.localPosition = Vector3.zero;
            itemini.transform.localScale = Vector3.one;
        }
            panel1.setEmptyNull();
            panel2.setEmptyNull();
            panel3.setEmptyNull();
    }

    public void InitPacket(bool openornot,GameObject packetcanvas)
    {
        if (openornot)
        {
            ShelterControl.Instance.setShelterPanel();
            this.LoadItems();
            packetcanvas.gameObject.SetActive(true);
            showPanel1();
        }
        else
        {
            panel1.setEmptyDisplay();
            panel2.setEmptyDisplay();
            panel3.setEmptyDisplay();
            panel1.DestroyItems();
            panel2.DestroyItems();
            panel3.DestroyItems();
            ShelterControl.Instance.cancelShelterPanel();
            packetcanvas.gameObject.SetActive(false);
        }
    }

    public void showPanel1()
    {
        panel1.gameObject.SetActive(true);
        panel2.gameObject.SetActive(false);
        panel3.gameObject.SetActive(false);
    }

    public void showPanel2()
    {
        panel1.gameObject.SetActive(false);
        panel2.gameObject.SetActive(true);
        panel3.gameObject.SetActive(false);
    }

    public void showPanel3()
    {
        panel1.gameObject.SetActive(false);
        panel2.gameObject.SetActive(false);
        panel3.gameObject.SetActive(true);
    }

	public void showLeftPage()
    {
        if(currentpanel == 2)
        {
            showPanel1();
            currentpanel = 1;
        }
        else if(currentpanel == 3)
        {
            showPanel2();
            currentpanel = 2;
        }
    }
    public void showRightPage()
    {
        if(currentpanel == 1)
        {
            showPanel2();
            currentpanel = 2;
        }
        else if(currentpanel == 2)
        {
            showPanel3();
            currentpanel = 3;
        }
    }
}