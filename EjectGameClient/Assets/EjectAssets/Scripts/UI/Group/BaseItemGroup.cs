using System.Collections;
using System.Collections.Generic;
using EjectData;
using UnityEngine;
using UnityEngine.UI;

public class BaseItemGroup : MonoBehaviour
{
    public int itemCount;
    public GameObject itemPerfab;

    [HideInInspector]
    public List<BaseItem> itemList = new List<BaseItem>();


    protected virtual void Awake()
    {

    }

	protected virtual void Start () {
		
	}

    public void GenerateItems(ToggleGroup toggleGroup)
    {
        for (int i = 0; i < itemCount; i++)
        {
            BaseItem item = Instantiate(itemPerfab, transform).GetComponent<BaseItem>();

            item.SetToggleGroup(toggleGroup);
            itemList.Add(item);
        }
    }

    public void SetupItemData(List<BaseItemData> dataList)
    {
        if (dataList.Count > itemCount)
        {
            Debug.LogError("数据列表长度超过装载的 item 列表");
            return;
        }

        for (int i = 0; i < dataList.Count; i++)
        {
            itemList[i].data = dataList[i];
            itemList[i].Setup();
        }
    }
}
