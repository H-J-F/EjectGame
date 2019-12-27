using System;
using System.Collections;
using System.Collections.Generic;
using EjectGame.Language;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSwitchItem : MonoBehaviour 
{
	#region public field

    public string textId;

	#endregion


	#region private field

    [HideInInspector] public Text target;

	#endregion


	#region monobehavior callbacks

    void Awake()
    {
        target = GetComponent<Text>();
        LanguageManager.instance.LanguageSwitchItems.Add(this);
    }

	void Start ()
	{
	    SetupText();
	}

    private void OnDestroy()
    {
        if (LanguageManager.instance != null && LanguageManager.instance.LanguageSwitchItems != null)
        {
            LanguageManager.instance.LanguageSwitchItems.Remove(this);
        }
    }
    
    #endregion

    #region custom methods

    public void SetupText()
    {
        if (String.IsNullOrEmpty(textId))
            return;

        LanguageUnit unit = LanguageManager.instance.GetLanguageUnit(textId);
        target.text = unit.content;

        if (unit.fontSize != 0)
            target.fontSize = unit.fontSize;

        if (unit.lineSpace > 0.0001f)
            target.lineSpacing = unit.lineSpace;
    }

    public void ChangeTextWithId(string id)
    {
        textId = id;
        SetupText();
    }

	#endregion


	#region custom coroutines



	#endregion
}