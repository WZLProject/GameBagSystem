using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using static UnityEditor.Progress;

public enum E_Bag_Type
{
    Item = 1,
    Equip,
    Gem
}

public class GameDataMgr : BaseManager<GameDataMgr>
{
    private GameDataMgr() { }

    BagItemContainer bagItem;
    ShopItemContainer shopItem;
    public ShopItemContainer ShopItem
    {
        get { return shopItem; }
    }

    private static string PlayerInfo_Url;
    public Player playerInfo;


    public void Init()
    {
        bagItem = BinaryDataMgr.Instance.GetTable<BagItemContainer>();
        shopItem = BinaryDataMgr.Instance.GetTable<ShopItemContainer>();
        Debug.Log(shopItem.dataDic[0].ShopItem_Name);

        //bagItem.dataDic
        //shopItem.dataDic

        if (File.Exists(PlayerInfo_Url))
        {
            
        }
        else
        {
            playerInfo = new Player();
            
        }
        EventCenter.Instance.AddEventListener<int>(E_EventType.E_Bag_UpdateMoney, ChangeMoney);
        EventCenter.Instance.AddEventListener<int>(E_EventType.E_Bag_UpdateMoney, ChangeGem);
        EventCenter.Instance.AddEventListener<int>(E_EventType.E_Bag_UpdateMoney, ChangePro);
    }

    public BagItem GetItemInfo(int id)
    {
        if (bagItem.dataDic.ContainsKey(id))
        {
            return bagItem.dataDic[id];
        }
        return null;
    }

    public void SavePlayerInfo()
    {
        //string json = JsonUtility.ToJson(playerInfo);
        //File.WriteAllBytes(PlayerInfo_Url, Encoding.UTF8.GetBytes(json));
    }

    private void ChangeMoney(int num)
    {
        playerInfo.ChangeMoney(num);
        SavePlayerInfo();
    }

    private void ChangeGem(int num)
    {
        playerInfo.ChangeGem(num);
        SavePlayerInfo();
    }

    private void ChangePro(int num)
    {
        playerInfo.ChangePro(num);
        SavePlayerInfo();
    }
}
