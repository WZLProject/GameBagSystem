using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    public override void HideMe()
    {
        EventCenter.Instance.RemoveEventListener<int>(E_EventType.E_Bag_UpdateMoney, UpdatePanel);
    }

    public override void ShowMe()
    {
        Debug.Log(GameDataMgr.Instance.playerInfo.name);
        GetControl<TMP_Text>("Text_玩家名称").text = "名字：" + GameDataMgr.Instance.playerInfo.name;
        GetControl<TMP_Text>("Text_玩家等级").text = "等级：" + GameDataMgr.Instance.playerInfo.lev.ToString();
        GetControl<TMP_Text>("Text_玩家金钱").text = "金币：" + GameDataMgr.Instance.playerInfo.money.ToString();
        GetControl<TMP_Text>("Text_玩家宝石").text = "宝石：" + GameDataMgr.Instance.playerInfo.gem.ToString();
        GetControl<TMP_Text>("Text_玩家能量").text = "能量：" + GameDataMgr.Instance.playerInfo.power.ToString();


        GetControl<Button>("Btn_商店").onClick.AddListener(() =>
        {
            UIMgr.Instance.ShowPanel<ShopPanel>();
        });

        GetControl<Button>("Btn_加钱").onClick.AddListener(() =>
        {
            EventCenter.Instance.EventTrigger(E_EventType.E_Bag_UpdateMoney, 100);
        });

        GetControl<Button>("Btn_加宝石").onClick.AddListener(() =>
        {
            EventCenter.Instance.EventTrigger(E_EventType.E_Bag_UpdateMoney, 100);
        });

        GetControl<Button>("Btn_加能量").onClick.AddListener(() =>
        {
            EventCenter.Instance.EventTrigger(E_EventType.E_Bag_UpdateMoney, 100);
        });

        EventCenter.Instance.AddEventListener<int>(E_EventType.E_Bag_UpdateMoney, UpdatePanel);
    }

    protected override void ClickBtn(string btnName)
    {
        switch (btnName)
        {
            case "Btn_背包":
                UIMgr.Instance.ShowPanel<RolePanel>();
                UIMgr.Instance.ShowPanel<BagPanel>();
                break;
        }
    }

    private void UpdatePanel(int num)
    {
        GetControl<TMP_Text>("Text_玩家名称").text = "名字：" + GameDataMgr.Instance.playerInfo.name;
        GetControl<TMP_Text>("Text_玩家等级").text = "等级：" + GameDataMgr.Instance.playerInfo.lev.ToString();
        GetControl<TMP_Text>("Text_玩家金钱").text = "金币：" + GameDataMgr.Instance.playerInfo.money.ToString();
        GetControl<TMP_Text>("Text_玩家宝石").text = "宝石：" + GameDataMgr.Instance.playerInfo.gem.ToString();
        GetControl<TMP_Text>("Text_玩家能量").text = "能量：" + GameDataMgr.Instance.playerInfo.power.ToString();
    }
}
