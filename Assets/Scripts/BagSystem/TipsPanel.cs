using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TipsPanel : BasePanel
{

    private BagItemInfo itemInfo;

    public override void HideMe()
    {
        
    }

    public override void ShowMe()
    {
        
    }
    public void InitInfo(BagItemInfo info)
    {
        this.itemInfo = info;
        BagItem itemData = GameDataMgr.Instance.GetItemInfo(info.id);
        GetControl<Image>("Icon").sprite = ResMgr.Instance.Load<Sprite>("Icon/" + itemData.BagItem_Icon);
        GetControl<TMP_Text>("Text_����").text = "����:" + info.num.ToString();
        GetControl<TMP_Text>("Text_����").text = itemData.BagItem_Name;
        GetControl<TMP_Text>("Text_��ʾ��Ϣ").text = itemData.BagItem_Tips;
    }

}
