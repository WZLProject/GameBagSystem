using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class ShopCell : BasePanel
{
    private ShopItem info;

    public override void HideMe()
    {
        
    }

    public override void ShowMe()
    {
        
    }

    protected override void ClickBtn(string btnName)
    {
        switch (btnName)
        {
            case "Btn_购买":
                if (info.ShopItem_PriceType == 1 && GameDataMgr.Instance.playerInfo.money >= info.ShopItem_Price)
                {
                    GameDataMgr.Instance.playerInfo.AddItem(new BagItemInfo(info.ShopItem_ID,info.ShopItem_Num));
                    EventCenter.Instance.EventTrigger(E_EventType.E_Bag_UpdateMoney, -info.ShopItem_Price);
                    UIMgr.Instance.ShowPanel<OneBtnTipsPanel>();
                    UIMgr.Instance.GetPanel<OneBtnTipsPanel>((panel) => { panel.InitInfo("购买成功"); });
                    //TipsMgr.Instance.ShowOneBtnTips("购买成功");
                }
                else if (info.ShopItem_PriceType == 2 && GameDataMgr.Instance.playerInfo.gem >= info.ShopItem_Price)
                {
                    GameDataMgr.Instance.playerInfo.AddItem(new BagItemInfo(info.ShopItem_ID, info.ShopItem_Num));
                    EventCenter.Instance.EventTrigger(E_EventType.E_Bag_UpdateMoney, -info.ShopItem_Price);
                    UIMgr.Instance.ShowPanel<OneBtnTipsPanel>();
                    UIMgr.Instance.GetPanel<OneBtnTipsPanel>((panel) => { panel.InitInfo("购买成功"); });
                    
                }
                else
                {
                    UIMgr.Instance.ShowPanel<OneBtnTipsPanel>();
                    UIMgr.Instance.GetPanel<OneBtnTipsPanel>((panel) => { panel.InitInfo("货币不足"); });
                    
                }
                break;
                
        }
    }

    public void InitInfo(ShopItem info)
    {
        this.info = info;
        BagItem item = GameDataMgr.Instance.GetItemInfo(info.ShopItem_BagId);
        Debug.Log(item.BagItem_Name);
        GetControl<Image>("Icon").sprite = ResMgr.Instance.Load<Sprite>("Icon/" + item.BagItem_Icon);
        GetControl<TMP_Text>("Text_数量").text = info.ShopItem_Num.ToString();
        GetControl<TMP_Text>("Text_名字").text = item.BagItem_Name;
        //GetControl<Image>("Type").sprite = ResMgr.Instance.Load<Sprite>("Icon/" + (info.priceType == 1 ? "gold" : "gem"));
        GetControl<TMP_Text>("Text_价格").text = info.ShopItem_Price.ToString();
        GetControl<TMP_Text>("Text_介绍").text = info.ShopItem_Tips;
    }
}
