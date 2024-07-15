using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ItemS : MonoBehaviour
{
    public Sprite Image;
    public string DataClassName;
    public string DataName;
    public int Price;
    public bool UseBles;
    public int UsaBles;
    public void Start()
    {
        getPrice(); 
    }
    public void getPrice()
    {
        DataClassName = Image.name;
        Debug.Log(DataClassName);
        GetTooltip(DataClassName);

    }

    public void GetTooltip(string itemClassName)
    {
        //var itemData = DataManager.Inst.GetIteminfo(DataClassName);
        //if (itemData == null)
        //    return;
        //Price=itemData.Price;
        //UsaBles = itemData.Usable;
        //DataName= itemData.Name;
        //if(itemData.Usable > 0)
        //{
        //    UseBles=true;
        //}
        //else
        //{
        //    UseBles= false;
        //}
    }


}

