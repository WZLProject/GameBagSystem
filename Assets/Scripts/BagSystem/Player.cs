using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string name;
    public int lev;
    public int money;
    public int gem;
    public int power;
    public List<BagItemInfo> items;
    public List<BagItemInfo> equips;
    public List<BagItemInfo> gems;
    public List<BagItemInfo> nowEquips;
    public Player()
    {
        name = "ะกร๗";
        lev = 10;
        money = 9999;
        gem = 999;
        power = 99;
        items = new List<BagItemInfo>() { new BagItemInfo() { id = 10, num = 10 }, new BagItemInfo() { id = 11, num = 5 } };
        equips = new List<BagItemInfo>() { new BagItemInfo() { id = 0, num = 1 }, new BagItemInfo() { id = 2, num = 1 }, new BagItemInfo() { id = 1, num = 1 }, new BagItemInfo() { id = 7, num = 1 } };
        gems = new List<BagItemInfo>() { new BagItemInfo() { id = 12, num = 2 }, new BagItemInfo() { id = 13, num = 30 } };
        nowEquips = new List<BagItemInfo>();
    }

    public void AddItem(BagItemInfo info)
    {
        BagItem item = GameDataMgr.Instance.GetItemInfo(info.id);
        switch (item.BagItem_Type)
        {
            case (int)E_Bag_Type.Item:
                bool isAdd = false;
                foreach (var it in items)
                {
                    if (it.id == info.id && it.num < 99)
                    {
                        if (it.num + info.num <= 99)
                        {
                            it.num += info.num;
                            isAdd = true;
                            break;
                        }
                        else
                        {
                            items.Add(new BagItemInfo { id = info.id, num = info.num - (99 - it.num) });
                            it.num = 99;
                            isAdd = true;
                            break;
                        }
                    }
                }
                if (!isAdd)
                {
                    items.Add(info);
                }
                break;
            case (int)E_Bag_Type.Equip:
                equips.Add(info);
                break;
            case (int)E_Bag_Type.Gem:
                isAdd = false;
                foreach (var it in gems)
                {
                    if (it.id == info.id && it.num < 99)
                    {
                        if (it.num + info.num <= 99)
                        {
                            it.num += info.num;
                            isAdd = true;
                            break;
                        }
                        else
                        {
                            gems.Add(new BagItemInfo { id = info.id, num = info.num - (99 - it.num) });
                            it.num = 99;
                            isAdd = true;
                            break;
                        }
                    }
                }
                if (!isAdd)
                {
                    gems.Add(info);
                }
                break;
        }
    }

    public void ChangeMoney(int num)
    {
        if (this.money < 0 && (this.money < num || num > 0))
        {
            return;
        }
        this.money += num;
    }

    public void ChangeGem(int num)
    {
        if (this.gem < 0 && (this.gem < num || num > 0))
        {
            return;
        }
        this.gem += num;
    }

    public void ChangePro(int num)
    {
        if (this.power < 0 && (this.power < num || num > 0))
        {
            return;
        }
        this.power += num;
    }
}
