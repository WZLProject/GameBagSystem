using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OneBtnTipsPanel : BasePanel
{
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
            case "Btn_�ر�":
                UIMgr.Instance.HidePanel<OneBtnTipsPanel>();
                break;
        }
    }

    public void InitInfo(string text)
    {
        GetControl<TMP_Text>("Text_��ʾ��Ϣ").text = text;
    }

}
