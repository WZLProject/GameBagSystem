using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BagPanel : BasePanel
{
    public Transform content;
    private List<ItemCell> celllist = new List<ItemCell>();


    public override void HideMe()
    {
        
    }

    public override void ShowMe()
    {
        ChangeType(E_Bag_Type.Item);
    }

    protected override void ClickBtn(string btnName)
    {
        switch (btnName)
        {
            case "Btn_关闭":
                UIMgr.Instance.HidePanel<BagPanel>();
                break;
        }
    }

    protected override void ToggleValueChange(string sliderName, bool value)
    {
        switch (sliderName)
        {
            case "Toggle_道具":
                if (value)
                {
                    ChangeType(E_Bag_Type.Item);
                }
                break;
            case "Toggle_装备":
                if (value)
                {
                    ChangeType(E_Bag_Type.Equip);
                }
                break;
            case "Toggle_碎片":
                if (value)
                {
                    ChangeType(E_Bag_Type.Gem);
                }
                break;
        }
    }

    public void ChangeType(E_Bag_Type type)
    {
        List<BagItemInfo> info = GameDataMgr.Instance.playerInfo.items;
        switch (type)
        {
            case E_Bag_Type.Item:
                info = GameDataMgr.Instance.playerInfo.items;
                break;
            case E_Bag_Type.Equip:
                info = GameDataMgr.Instance.playerInfo.equips;
                break;
            case E_Bag_Type.Gem:
                info = GameDataMgr.Instance.playerInfo.gems;
                break;
        }
        foreach (var item in celllist)
        {
            Destroy(item.gameObject);
        }
        celllist.Clear();
        foreach (BagItemInfo item in info)
        {
            ResMgr.Instance.LoadAsync<GameObject>("UI/ItemCell", (go) =>
            {
                //Debug.Log(item.id);
                ItemCell cell = Instantiate(go).GetComponent<ItemCell>();
                cell.transform.SetParent(content);
                cell.transform.localPosition = new Vector3(0, 0, 0);
                cell.transform.localScale = new Vector3(1, 1, 1);
                cell.InitInfo(item);
                celllist.Add(cell);
            });
        }
    }
}
