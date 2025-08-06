using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RolePanel : BasePanel
{
    public ItemCell itemHead;
    public ItemCell itemBody;
    public ItemCell itemShose;
    public ItemCell itemWeaponA;
    public ItemCell itemWeaponB;

    public override void HideMe()
    {
        
    }

    public override void ShowMe()
    {
        
        UpdateRolePanel();
    }

    protected override void ClickBtn(string btnName)
    {
        switch (btnName)
        {
            case "Btn_¹Ø±Õ":
                UIMgr.Instance.HidePanel<RolePanel>();
                break;
        }
    }

    public void UpdateRolePanel()
    {
        List<BagItemInfo> nowEquips = GameDataMgr.Instance.playerInfo.nowEquips;
        itemHead.InitInfo(null);
        itemBody.InitInfo(null);
        itemShose.InitInfo(null);
        itemWeaponA.InitInfo(null);
        itemWeaponB.InitInfo(null);
        itemShose.InitInfo(null);
        BagItem itemInfo;
        for (int i = 0; i < nowEquips.Count; i++)
        {
            itemInfo = GameDataMgr.Instance.GetItemInfo(nowEquips[i].id);
            switch (itemInfo.BagItem_EquipType)
            {
                case (int)E_Item_Type.head:
                    itemHead.InitInfo(nowEquips[i]);
                    break;
                case (int)E_Item_Type.Body:
                    itemBody.InitInfo(nowEquips[i]);
                    break;
                case (int)E_Item_Type.Shose:
                    itemShose.InitInfo(nowEquips[i]);
                    break;
                case (int)E_Item_Type.WeaponA:
                    itemWeaponA.InitInfo(nowEquips[i]);
                    break;
                case (int)E_Item_Type.WeaponB:
                    itemWeaponB.InitInfo(nowEquips[i]);
                    break;
            }
        }
    }
}
