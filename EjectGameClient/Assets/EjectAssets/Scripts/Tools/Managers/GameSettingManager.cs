
using System.Collections;
using System.Collections.Generic;
using EjectGame.Tools;
using UnityEngine;

public class GameSettingManager : BaseManager<GameSettingManager>
{
	#region public field

    public EnumValueListClass<GameFrameRateType> gameFrameRateValueList = new EnumValueListClass<GameFrameRateType>();
    public EnumValueListClass<GameQuality> gameQualityValueList = new EnumValueListClass<GameQuality>();

    public GameFrameRateType GameTargetFrameRate
    {
        get { return (GameFrameRateType) Application.targetFrameRate; }
        set { Application.targetFrameRate = (int) value; }
    }

    public GameQuality GameQualitySetting
    {
        get { return (GameQuality) QualitySettings.GetQualityLevel(); }
        set { QualitySettings.SetQualityLevel((int) value, true); }
    }

    #endregion


    #region private field

    

    #endregion


    #region monobehavior callbacks

    protected override void Awake()
    {
        base.Awake();
        GameTargetFrameRate = GameFrameRateType.High;
        GameQualitySetting = GameQuality.Fantastic;
        gameFrameRateValueList.SetIndexWithEnumType(GameTargetFrameRate);
        gameQualityValueList.SetIndexWithEnumType(GameQualitySetting);
    }

    #endregion

    #region custom methods

    

    #endregion


    #region custom coroutines



    #endregion
}
