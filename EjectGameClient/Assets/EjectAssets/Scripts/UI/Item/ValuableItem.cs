using System.Collections;
using System.Collections.Generic;
using EjectData;
using EjectGame.Tools;
using UnityEngine;
using UnityEngine.UI;

public class ValuableItem : BaseItem
{
    public Image valueTypeIcon;
    public Text valueText;

    public override void Setup()
    {
        ValuableItemData newData = (ValuableItemData)data;

        //itemName.text = newData.name;
        Sprite iconSprite = Resources.Load<Sprite>(EjectTools.uiIconPathPrefix + newData.iconPath);
        icon.sprite = iconSprite;
        
        valueTypeIcon.sprite = Resources.Load<Sprite>(EjectTools.uiIconPathPrefix + newData.valueTypeIconPath);
        valueText.text = newData.value.ToString();
    }
}