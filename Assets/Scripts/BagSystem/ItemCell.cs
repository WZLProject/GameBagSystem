using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// 物品类型
/// </summary>
public enum E_Item_Type
{
    Bag=0,
    head,
    Body,
    Shose,
    WeaponA,
    WeaponB,
}

public class ItemCell : BasePanel
{
    private BagItemInfo _itemInfo;
    public BagItemInfo itemInfo
    {
        get
        {
            return _itemInfo;
        }
    }

    private bool isDragOpen = false;

    public E_Item_Type equipType = E_Item_Type.Bag;

    public void InitInfo(BagItemInfo info)
    {
        this._itemInfo = info;
        
        if (info == null)
        {
            GetControl<Image>("Icon").color = new Color(0, 0, 0, 0);
            return;
        }
        GetControl<Image>("Icon").color = new Color(1, 1, 1, 1);
        BagItem itemData = GameDataMgr.Instance.GetItemInfo(info.id);
        GetControl<Image>("Icon").sprite = ResMgr.Instance.Load<Sprite>("Icon/" + itemData.BagItem_Icon);
        if (equipType == E_Item_Type.Bag)
        {
            GetControl<TMP_Text>("Num").text = info.num.ToString();
        }

        if (itemData.BagItem_Type == (int)E_Bag_Type.Equip && isDragOpen == false)
        {
            //Debug.Log("添加拖拽事件监听");
            isDragOpen = true;
            OpenDragEvent();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        GetControl<Image>("Icon").color = new Color(0, 0, 0, 0);
        EventCenter.AddCustomEventListener(GetControl<Image>("BK"), EventTriggerType.PointerEnter, EnterItemCell);
        EventCenter.AddCustomEventListener(GetControl<Image>("BK"), EventTriggerType.PointerExit, ExitItemCell);
    }

    public override void HideMe()
    {
        
    }

    public override void ShowMe()
    {
        
    }

    private void OpenDragEvent()
    {
        EventCenter.AddCustomEventListener(GetControl<Image>("BK"), EventTriggerType.BeginDrag, BeginDragItemCell);
        EventCenter.AddCustomEventListener(GetControl<Image>("BK"), EventTriggerType.Drag, DragItemCell);
        EventCenter.AddCustomEventListener(GetControl<Image>("BK"), EventTriggerType.EndDrag, EndDragItemCell);
    }

    private void EnterItemCell(BaseEventData date)
    {
        EventCenter.Instance.EventTrigger(E_EventType.E_Bag_ItemCellEnter,this);
    }
    private void ExitItemCell(BaseEventData date)
    {
        EventCenter.Instance.EventTrigger(E_EventType.E_Bag_ItemCellExit, this);
    }

    private void BeginDragItemCell(BaseEventData date)
    {
        EventCenter.Instance.EventTrigger(E_EventType.E_Bag_ItemCellBeginDrag, this);
    }
    private void DragItemCell(BaseEventData date)
    {
        EventCenter.Instance.EventTrigger(E_EventType.E_Bag_ItemCellDrag, date);
    }

    private void EndDragItemCell(BaseEventData date)
    {
        EventCenter.Instance.EventTrigger(E_EventType.E_Bag_ItemCellEndDrag, this);
    }
}
