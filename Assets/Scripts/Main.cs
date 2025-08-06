using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //BinaryDataMgr.Instance.InitData();
        BagItemContainer bagItem = BinaryDataMgr.Instance.GetTable<BagItemContainer>();
        ShopItemContainer shopItem = BinaryDataMgr.Instance.GetTable<ShopItemContainer>();


        for (int i=0;i< bagItem.dataDic.Count;i++)
        {
            //Debug.Log("�������ݣ�"+bagItem.dataDic[i].BagItem_Name);
        }
        for (int i = 0; i < shopItem.dataDic.Count; i++)
        {
            //Debug.Log("�̵����ݣ�"+shopItem.dataDic[i].ShopItem_Name);
        }

        GameDataMgr.Instance.Init();
        UIMgr.Instance.ShowPanel<MainPanel>();
        BagMgr.Instance.Init();
    }


}
