using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    public Material originMaterial;
    public string itemText;

    public abstract void Use(Player player);
    private void Start()
    {
        gameObject.name = icon.name;    
        itemName= gameObject.name;
        originMaterial = transform.GetComponent<MeshRenderer>().material;
    }
}
