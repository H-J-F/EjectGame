using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour 
{
	#region public field
    
    public Toggle coinContent;
    public Toggle diamondContent;
    public Toggle lifeContent;

    #endregion


    #region private field

    private PopupWindow shopWindow;

    #endregion


    #region monobehavior callbacks

    void Awake()
    {
        shopWindow = GetComponent<PopupWindow>();
    }

	void Start () {
		
	}

	#endregion

	#region custom methods

    public void OnCoinClick()
    {
        coinContent.isOn = true;
        diamondContent.isOn = false;
        lifeContent.isOn = false;
        shopWindow.OnPopupWindow();
    }

    public void OnDiamondClick()
    {
        coinContent.isOn = false;
        diamondContent.isOn = true;
        lifeContent.isOn = false;
        shopWindow.OnPopupWindow();
    }

    public void OnLifeClick()
    {
        coinContent.isOn = false;
        diamondContent.isOn = false;
        lifeContent.isOn = true;
        shopWindow.OnPopupWindow();
    }

    #endregion


    #region custom coroutines



    #endregion
}
