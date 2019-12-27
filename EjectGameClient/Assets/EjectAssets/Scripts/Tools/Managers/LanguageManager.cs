using System;
using System.Collections;
using System.Collections.Generic;
using EjectGame.Language;
using EjectGame.Tools;
using UnityEngine;

public class LanguageManager : BaseManager<LanguageManager>
{
	#region public field

    [HideInInspector]
    public List<LanguageSwitchItem> LanguageSwitchItems = new List<LanguageSwitchItem>();
    public LanguageSettings simplifiedChinese;
    public LanguageSettings traditionalChinese;
    public LanguageSettings english;
    public Dictionary<string, LanguageUnit> simplifiedChineseDict = new Dictionary<string, LanguageUnit>();
    public Dictionary<string, LanguageUnit> traditionalChineseDict = new Dictionary<string, LanguageUnit>();
    public Dictionary<string, LanguageUnit> englishDict = new Dictionary<string, LanguageUnit>();

    public EnumValueListClass<LanguageType> languageValueList = new EnumValueListClass<LanguageType>();

    public LanguageType LanguageType
    {
        get { return languageType; }

        set
        {
            languageType = value;
            StartCoroutine(SwitchLanguage());
        }
    }

    #endregion


    #region private field

    private LanguageType languageType = LanguageType.ZH_CN;

    #endregion


    #region monobehavior callbacks

    protected override void Awake()
    {
        base.Awake();
        languageValueList.SetIndexWithEnumType(LanguageType);
        SetupLanguage(simplifiedChinese, simplifiedChineseDict);
        SetupLanguage(traditionalChinese, traditionalChineseDict);
        SetupLanguage(english, englishDict);
    }

	#endregion

	#region custom methods

    

    private void SetupLanguage(LanguageSettings settings, Dictionary<string, LanguageUnit> dict)
    {
        foreach (LanguageUnit unit in settings.languageUnits)
        {
            dict.Add(unit.textId, unit);
        }
    }

    public LanguageUnit GetLanguageUnit(string textId)
    {
        switch (languageType)
        {
            case LanguageType.ZH_CN:
                return simplifiedChineseDict[textId];
            case LanguageType.ZH_TW:
                return traditionalChineseDict[textId];
            case LanguageType.English:
                return englishDict[textId];
            default:
                return simplifiedChineseDict[textId];
        }
    }

    public void ChangeToOtherLanguage()
    {
        LanguageType = languageType == LanguageType.ZH_CN ? LanguageType.ZH_TW : LanguageType.ZH_CN;
    }

	#endregion


	#region custom coroutines

    private IEnumerator SwitchLanguage()
    {
        for (int i = 0; i < LanguageSwitchItems.Count; i++)
        {
            LanguageSwitchItems[i].SetupText();

            if (i != 0 && i % 20 == 0)
            {
                yield return null;
            }
        }
    }

	#endregion
}
