using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagMgr : BaseManager<BagMgr>
{
    private BagMgr() { }

    private ItemCell nowDragItem;
    private ItemCell nowInItem;
    private Image nowSelItemImg;

    private bool isDraging = false;

    public void Init()
    {
        EventCenter.Instance.AddEventListener<ItemCell>(E_EventType.E_Bag_ItemCellBeginDrag, BeginDragItemCell);
        EventCenter.Instance.AddEventListener<BaseEventData>(E_EventType.E_Bag_ItemCellDrag, DragItemCell);
        EventCenter.Instance.AddEventListener<ItemCell>(E_EventType.E_Bag_ItemCellEndDrag, EndDragItemCell);
        EventCenter.Instance.AddEventListener<ItemCell>(E_EventType.E_Bag_ItemCellEnter, EnterItemCell);
        EventCenter.Instance.AddEventListener<ItemCell>(E_EventType.E_Bag_ItemCellExit, ExitItemCell);
    }


    public void ChangeEquip()
    {
        //当前被拖动的格子是从背包拖到装备的
        if (nowDragItem.equipType == E_Item_Type.Bag)
        {
            //存在拖入的格子且不拖入进的格子不是背包中的格子
            if (nowInItem != null && nowInItem.equipType != E_Item_Type.Bag)
            {
                Debug.Log("存在拖入的格子且不拖入进的格子不是背包中的格子");
                BagItem info = GameDataMgr.Instance.GetItemInfo(nowDragItem.itemInfo.id);
                //判断拖入的格子类型和装备类型是否一致
                if ((int)nowInItem.equipType == info.BagItem_EquipType)
                {
                    //判断当前拖入的格子是否是空的，是空的则直接装备，如果不是空的需要与背包拖入的装备交换
                    if (nowInItem.itemInfo == null)
                    {
                        GameDataMgr.Instance.playerInfo.nowEquips.Add(nowDragItem.itemInfo);
                        GameDataMgr.Instance.playerInfo.equips.Remove(nowDragItem.itemInfo);
                    }
                    else
                    {
                        GameDataMgr.Instance.playerInfo.nowEquips.Remove(nowInItem.itemInfo);
                        GameDataMgr.Instance.playerInfo.nowEquips.Add(nowDragItem.itemInfo);
                        GameDataMgr.Instance.playerInfo.equips.Remove(nowDragItem.itemInfo);
                        GameDataMgr.Instance.playerInfo.equips.Add(nowInItem.itemInfo);
                    }
                    //UIMgr.Instance.GetPanel<BagPanel>((panel) => { panel.ChangeType(E_Bag_Type.Equip); });
                    //UIMgr.Instance.GetPanel<RolePanel>((panel) => { panel.UpdateRolePanel(); });
                    //GameDataMgr.Instance.SavePlayerInfo();
                }
                
            }
            else if (nowInItem != null && nowInItem.equipType == E_Item_Type.Bag)
            {
                int gridIndex = 0;
                int dragIndex = 0;
                for (int i = 0; i < GameDataMgr.Instance.playerInfo.nowEquips.Count; i++)
                {
                    if (GameDataMgr.Instance.playerInfo.nowEquips[i] == nowInItem.itemInfo)
                    {
                        gridIndex = i;
                        Debug.Log(gridIndex);
                        //GameDataMgr.Instance.playerInfo.nowEquips[i] = nowInItem.itemInfo;
                    }
                    else if (GameDataMgr.Instance.playerInfo.nowEquips[i] == nowDragItem.itemInfo)
                    {
                        dragIndex = i;
                        Debug.Log(dragIndex);
                        //GameDataMgr.Instance.playerInfo.nowEquips[i] = nowInItem.itemInfo;
                    }
                }
                for (int i = 0; i < GameDataMgr.Instance.playerInfo.equips.Count; i++)
                {
                    if (GameDataMgr.Instance.playerInfo.equips[i] == nowInItem.itemInfo)
                    {
                        gridIndex = i;
                        Debug.Log(gridIndex);
                        //GameDataMgr.Instance.playerInfo.nowEquips[i] = nowInItem.itemInfo;
                    }
                    else if (GameDataMgr.Instance.playerInfo.equips[i] == nowDragItem.itemInfo)
                    {
                        dragIndex = i;
                        Debug.Log(dragIndex);
                        //GameDataMgr.Instance.playerInfo.nowEquips[i] = nowInItem.itemInfo;
                    }
                }
                BagItemInfo tempB = GameDataMgr.Instance.playerInfo.equips[gridIndex];
                GameDataMgr.Instance.playerInfo.equips[gridIndex] = GameDataMgr.Instance.playerInfo.equips[dragIndex];
                GameDataMgr.Instance.playerInfo.equips[dragIndex] = tempB;

            }

        }
        else
        {
            Debug.Log("没有拖入到任何格子中，下装备");
            //没有拖入到任何格子中，下装备
            if (nowInItem == null || nowInItem.equipType != E_Item_Type.Bag)
            {
                GameDataMgr.Instance.playerInfo.nowEquips.Remove(nowDragItem.itemInfo);
                GameDataMgr.Instance.playerInfo.equips.Add(nowDragItem.itemInfo);
            }
            //交换装备
            else if (nowInItem != null && nowInItem.equipType == E_Item_Type.Bag)
            {
                for(int i = 0;i< GameDataMgr.Instance.playerInfo.nowEquips.Count; i++)
                {
                    if (GameDataMgr.Instance.playerInfo.nowEquips[i]== nowDragItem.itemInfo)
                    {
                        GameDataMgr.Instance.playerInfo.nowEquips[i] = nowInItem.itemInfo;
                    }
                }
                for (int i = 0; i < GameDataMgr.Instance.playerInfo.equips.Count; i++)
                {
                    if (GameDataMgr.Instance.playerInfo.equips[i] == nowInItem.itemInfo)
                    {
                        GameDataMgr.Instance.playerInfo.equips[i] = nowDragItem.itemInfo;
                    }
                }
                //GameDataMgr.Instance.playerInfo.nowEquips.Remove(nowDragItem.itemInfo);
                //GameDataMgr.Instance.playerInfo.nowEquips.Add(nowInItem.itemInfo);
                //GameDataMgr.Instance.playerInfo.equips.Remove(nowInItem.itemInfo);
                //GameDataMgr.Instance.playerInfo.equips.Add(nowDragItem.itemInfo);
            }
            
        }
        UIMgr.Instance.GetPanel<BagPanel>((panel) => { panel.ChangeType(E_Bag_Type.Equip); });
        UIMgr.Instance.GetPanel<RolePanel>((panel) => { panel.UpdateRolePanel(); });
        GameDataMgr.Instance.SavePlayerInfo();
    }

    private void BeginDragItemCell(ItemCell itemCell)
    {
        
        if (itemCell.itemInfo == null)
        {
            return;
        }
        isDraging = true;
        UIMgr.Instance.HidePanel<TipsPanel>();
        nowDragItem = itemCell;
        GameObject obj = PoolMgr.Instance.GetObj("UI/Icon");
        nowSelItemImg = obj.GetComponent<Image>();
        nowSelItemImg.sprite = itemCell.GetControl<Image>("Icon").sprite;
        nowSelItemImg.transform.SetParent(UIMgr.Instance.uiCanvas.transform.Find("Top"));
        nowSelItemImg.transform.localScale = Vector3.one;
        //nowSelItemImg.transform.localPosition = Vector3.zero;
        if (!isDraging)
        {
            PoolMgr.Instance.PushObj(nowSelItemImg.gameObject);
            nowSelItemImg = null;
        }
    }

    private void DragItemCell(BaseEventData data)
    {
        if (nowSelItemImg == null)
        {
            return;
        }
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(UIMgr.Instance.uiCanvas.transform.Find("Top") as RectTransform, (data as PointerEventData).position, (data as PointerEventData).pressEventCamera, out localPos);
        nowSelItemImg.transform.localPosition = localPos;
    }

    private void EndDragItemCell(ItemCell itemCell)
    {
        Debug.Log("拖拽后");
        isDraging = false;
        if (nowSelItemImg == null)
        {
            return;
        }
        PoolMgr.Instance.PushObj(nowSelItemImg.gameObject);
        nowSelItemImg = null;
        ChangeEquip();
        nowDragItem = null;
        nowInItem = null;
    }

    private void EnterItemCell(ItemCell itemCell)
    {
        
        if (isDraging)
        {
            nowInItem = itemCell;
            return;
        }
        if (itemCell.itemInfo == null)
        {
            return;
        }
        UIMgr.Instance.ShowPanel<TipsPanel>();
        UIMgr.Instance.GetPanel<TipsPanel>( (panel) =>
        {
            panel.InitInfo(itemCell.itemInfo);
            panel.transform.position = itemCell.GetControl<Image>("Icon").transform.position;
            if (isDraging)
            {
                UIMgr.Instance.HidePanel<TipsPanel>();
            }
        });
    }

    private void ExitItemCell(ItemCell itemCell)
    {
        if (isDraging)
        {
            Debug.Log("出范围后");
            nowInItem = null;
            return;
        }
        if (itemCell.itemInfo == null)
        {
            return;
        }
        UIMgr.Instance.HidePanel<TipsPanel>();
    }
}
