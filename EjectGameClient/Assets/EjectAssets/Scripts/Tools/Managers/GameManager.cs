using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
	#region public field

    public GameObject mobileUI;
    public JoyStick moveJoyStick;
    public JoyStick rotateJoyStick;

    #endregion


    #region private field



    #endregion


    #region monobehavior callbacks

    protected override void Awake()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
        {
            mobileUI.SetActive(false);
        }
    }

    #endregion

    #region custom methods



    #endregion


    #region custom coroutines



    #endregion
}