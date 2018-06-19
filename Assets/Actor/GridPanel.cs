using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPanel : MonoBehaviour {
    public Transform[] grids;

    public Transform getEmptyGrid()
    {
        int i = 0;
        bool flag = false;
        for (i = 0; i < grids.Length; i++)
        {
            if (grids[i].childCount == 0)
            {
                flag = true;
                break;
            }
        }
        if (flag)
        {
            return grids[i];
        }
        return null;
    }

    public int getFullCount()
    {
        int count = 0;
        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i].childCount != 0)
            {
                count += 1;
            }
            else
            {
                break;
            }
        }
        return count;
    }

    public void setEmptyNull()
    {
        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i].childCount == 0)
            {
                grids[i].gameObject.SetActive(false);
            }
        }
    }

    public void setEmptyDisplay()
    {
        for (int i = 0; i < grids.Length; i++)
        {   
                grids[i].gameObject.SetActive(true);
        }
    }

    public void DestroyItems()
    {
        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i].childCount != 0)
            {
                DestroyImmediate(grids[i].GetChild(0).gameObject);
            }
        }
    }
}
