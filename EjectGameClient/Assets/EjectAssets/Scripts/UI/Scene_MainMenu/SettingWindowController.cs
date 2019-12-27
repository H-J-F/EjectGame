using System.Collections;
using System.Collections.Generic;
using EjectGame.Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingWindowController : MonoBehaviour 
{
	#region public field

    public Text soundValueText;
    public Text musicValueText;
    public SwitchFadeText frameRateSwitch;
    public SwitchFadeText qualitySwitch;
    public SwitchFadeText languageSwitch;

	#endregion


	#region private field



	#endregion


	#region monobehavior callbacks

	void Awake ()
	{
        SwitchSetTempContentText(frameRateSwitch, ref frameRateSwitch.onBeforePreviousAction, "MainMenuScene_FrameRate_", GameSettingManager.instance.gameFrameRateValueList, true);
        SwitchSetTempContentText(qualitySwitch, ref qualitySwitch.onBeforePreviousAction, "MainMenuScene_Graphics_", GameSettingManager.instance.gameQualityValueList, true);
        SwitchSetTempContentText(languageSwitch, ref languageSwitch.onBeforePreviousAction, "MainMenuScene_Language_", LanguageManager.instance.languageValueList, true);

	    SwitchSetTempContentText(frameRateSwitch, ref frameRateSwitch.onBeforeNextAction, "MainMenuScene_FrameRate_", GameSettingManager.instance.gameFrameRateValueList, false);
        SwitchSetTempContentText(qualitySwitch, ref qualitySwitch.onBeforeNextAction, "MainMenuScene_Graphics_", GameSettingManager.instance.gameQualityValueList, false);
        SwitchSetTempContentText(languageSwitch, ref languageSwitch.onBeforeNextAction, "MainMenuScene_Language_", LanguageManager.instance.languageValueList, false);

	    frameRateSwitch.completeAction.AddListener(() =>
	    {
	        GameSettingManager.instance.GameTargetFrameRate = (GameFrameRateType) frameRateSwitch.settingType;
	    });

	    qualitySwitch.completeAction.AddListener(() =>
	    {
	        GameSettingManager.instance.GameQualitySetting = (GameQuality) qualitySwitch.settingType;
	    });

        languageSwitch.completeAction.AddListener(() =>
	    {
	        LanguageManager.instance.LanguageType = (LanguageType) languageSwitch.settingType;
        });
    }

    void Start()
    {
        frameRateSwitch.SetCurrentContentTextWithId("MainMenuScene_FrameRate_" + GameSettingManager.instance.GameTargetFrameRate);
        qualitySwitch.SetCurrentContentTextWithId("MainMenuScene_Graphics_" + GameSettingManager.instance.GameQualitySetting);
        languageSwitch.SetCurrentContentTextWithId("MainMenuScene_Language_" + LanguageManager.instance.LanguageType);
    }

	#endregion

	#region custom methods

    private void SwitchSetTempContentText<T>(SwitchFadeText switchFadeText, ref UnityEvent unityEvent, string idPrefix, EnumValueListClass<T> valueList, bool previous)
    {
        unityEvent.AddListener(() =>
        {
            switchFadeText.SetTempContentTextWithId(idPrefix, valueList.GetListValue(!previous));
        });
    }

    public void OnSoundValueChanged(float soundValue)
    {
        soundValueText.text = soundValue.ToString();
    }

    public void OnMusicValueChanged(float musicValue)
    {
        musicValueText.text = musicValue.ToString();
    }

	#endregion


	#region custom coroutines
    


    #endregion
}
