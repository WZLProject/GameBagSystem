using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ShopPanel : BasePanel
{
    public Transform content;

    private List<ShopCell> cellList = new List<ShopCell>();

    public override void HideMe()
    {
        
    }

    public override void ShowMe()
    {
        foreach (var cell in cellList)
        {
            Destroy(cell.gameObject);
        }
        cellList.Clear();
        for (int i=0;i< GameDataMgr.Instance.ShopItem.dataDic.Count-1;i++)
        {
            int temp = i;
            //Debug.Log(GameDataMgr.Instance.ShopItem.dataDic[i]);
            ResMgr.Instance.LoadAsync<GameObject>("UI/ShopCell", (go) =>
            {
                //Debug.Log(GameDataMgr.Instance.ShopItem.dataDic[i].ShopItem_Name);
                ShopCell cell = Instantiate(go).GetComponent<ShopCell>();
                cell.transform.SetParent(content);
                cell.transform.localScale = new Vector3(1, 1, 1);
                cell.transform.localPosition = Vector3.zero;
                cell.InitInfo(GameDataMgr.Instance.ShopItem.dataDic[temp]);
                cellList.Add(cell);
            });
        }
    }

    protected override void ClickBtn(string btnName)
    {
        switch (btnName)
        {
            case "Btn_¹Ø±Õ":
                UIMgr.Instance.HidePanel<ShopPanel>();
                break;
        }
    }

}
