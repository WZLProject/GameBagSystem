using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item : MonoBehaviour
{
    public int id;
    public string name;
    public string icon;
    public int equipType;
    public int type;
    public int price;
    public string tips;
}

[Serializable]
public class BagItemInfo
{
    public int id;
    public int num;
    public BagItemInfo() { }
    public BagItemInfo(int id,int num)
    {
        this.id = id;
        this.num = num;
    }
}
public class BagItems
{
    public List<BagItem> info;
}


public class Shops
{
    public List<ShopItem> info;
}



