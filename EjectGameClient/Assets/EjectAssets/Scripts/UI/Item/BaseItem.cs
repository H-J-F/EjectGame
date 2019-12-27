using System.Collections;
using System.Collections.Generic;
using EjectData;
using EjectGame.Tools;
using UnityEngine;
using UnityEngine.UI;

public class BaseItem : MonoBehaviour
{
    public bool swapColor;
    [HideInInspector]
    public Image itemBG;
    public Image icon;
    public Sprite normalSprite;
    public Sprite selectedSprite;
    public Color selectedColor;
    public LanguageSwitchItem itemName;
    public BaseItemData data;

    private Toggle itemBtn;
    private Color normalColor;


    protected virtual void Awake()
    {
        itemBG = GetComponent<Image>();
        itemBtn = GetComponent<Toggle>();
        normalColor = itemBG.color;
        itemBtn.onValueChanged.AddListener(OnSelect);
    }

	protected virtual void Start () {
		
	}
    
    public virtual void Setup()
    {
        //itemName.ChangeTextWithId(data.nameTextId);
        Sprite iconSprite = Resources.Load<Sprite>(EjectTools.uiIconPathPrefix + data.iconPath);
        icon.sprite = iconSprite;
    }

    public virtual void OnSelect(bool isOn)
    {
        itemBG.sprite = isOn ? selectedSprite : normalSprite;
        if (swapColor)
            itemBG.color = isOn ? selectedColor : normalColor;
    }

    public void SetToggleGroup(ToggleGroup group)
    {
        itemBtn.group = group;
    }

    public void LogValue(Vector2 pos)
    {
        Debug.Log(pos);
    }
}