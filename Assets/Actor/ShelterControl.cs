using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterControl : MonoBehaviour
{

    public GameObject shelter;
    public GameObject gridpanel;
    public GameObject packet;
    public GameObject nest;

    private static ShelterControl instance;

    private void Awake()
    {
        shelter = GameObject.Find("ShelterPanel");
        gridpanel = GameObject.Find("GridPanel");
        packet = GameObject.Find("Packet");
        instance = this;
        if (SuperGameMaster.saveData.Item.isUsed)
        {
            nest.gameObject.SetActive(true);
        }
        else
        {
            nest.gameObject.SetActive(false);
        }
    }

    public static ShelterControl Instance
    {
        get
        {
            return instance;
        }
    }
    public void setShelterPanel()
    {
        shelter.transform.SetSiblingIndex(3);
        gridpanel.transform.SetSiblingIndex(4);
        packet.transform.SetSiblingIndex(4);
        shelter.gameObject.SetActive(true);
    }

    public void cancelShelterPanel()
    {
        shelter.gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {
        shelter.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
