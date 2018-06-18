using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacketClick : MonoBehaviour {
    public bool packetopenornot;
    public GameObject packetcanvas;
    // Use this for initialization

    private void Awake()
    {
        packetcanvas = GameObject.Find("GridPanel");
        packetopenornot = false;
    }
    void Start () {
        GameObject imageobj = GameObject.Find("Packet");
        packetcanvas.gameObject.SetActive(false);
        Button packet = imageobj.GetComponent<Button>();
        packet.onClick.AddListener(delegate()
        {
            this.PacketButtonClicked();
        });
    }
	
    void PacketButtonClicked()
    {
        if (packetopenornot == false)
        {
            packetopenornot = true;
        }
        else if (packetopenornot)
        {
            packetopenornot = false;
        }
        Debug.Log(packetopenornot);
        CanvasManager.Instance.InitPacket(packetopenornot, packetcanvas);
    }
}
