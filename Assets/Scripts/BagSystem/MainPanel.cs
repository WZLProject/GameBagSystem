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
        GetControl<TMP_Text>("Text_�������").text = "���֣�" + GameDataMgr.Instance.playerInfo.name;
        GetControl<TMP_Text>("Text_��ҵȼ�").text = "�ȼ���" + GameDataMgr.Instance.playerInfo.lev.ToString();
        GetControl<TMP_Text>("Text_��ҽ�Ǯ").text = "��ң�" + GameDataMgr.Instance.playerInfo.money.ToString();
        GetControl<TMP_Text>("Text_��ұ�ʯ").text = "��ʯ��" + GameDataMgr.Instance.playerInfo.gem.ToString();
        GetControl<TMP_Text>("Text_�������").text = "������" + GameDataMgr.Instance.playerInfo.power.ToString();


        GetControl<Button>("Btn_�̵�").onClick.AddListener(() =>
        {
            UIMgr.Instance.ShowPanel<ShopPanel>();
        });

        GetControl<Button>("Btn_��Ǯ").onClick.AddListener(() =>
        {
            EventCenter.Instance.EventTrigger(E_EventType.E_Bag_UpdateMoney, 100);
        });

        GetControl<Button>("Btn_�ӱ�ʯ").onClick.AddListener(() =>
        {
            EventCenter.Instance.EventTrigger(E_EventType.E_Bag_UpdateMoney, 100);
        });

        GetControl<Button>("Btn_������").onClick.AddListener(() =>
        {
            EventCenter.Instance.EventTrigger(E_EventType.E_Bag_UpdateMoney, 100);
        });

        EventCenter.Instance.AddEventListener<int>(E_EventType.E_Bag_UpdateMoney, UpdatePanel);
    }

    protected override void ClickBtn(string btnName)
    {
        switch (btnName)
        {
            case "Btn_����":
                UIMgr.Instance.ShowPanel<RolePanel>();
                UIMgr.Instance.ShowPanel<BagPanel>();
                break;
        }
    }

    private void UpdatePanel(int num)
    {
        GetControl<TMP_Text>("Text_�������").text = "���֣�" + GameDataMgr.Instance.playerInfo.name;
        GetControl<TMP_Text>("Text_��ҵȼ�").text = "�ȼ���" + GameDataMgr.Instance.playerInfo.lev.ToString();
        GetControl<TMP_Text>("Text_��ҽ�Ǯ").text = "��ң�" + GameDataMgr.Instance.playerInfo.money.ToString();
        GetControl<TMP_Text>("Text_��ұ�ʯ").text = "��ʯ��" + GameDataMgr.Instance.playerInfo.gem.ToString();
        GetControl<TMP_Text>("Text_�������").text = "������" + GameDataMgr.Instance.playerInfo.power.ToString();
    }
}
