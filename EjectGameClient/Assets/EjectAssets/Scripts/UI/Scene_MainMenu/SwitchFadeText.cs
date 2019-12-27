using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SwitchFadeText : MonoBehaviour
{
    public int fadeDistance;
    public LanguageSwitchItem content_1;
    public LanguageSwitchItem content_2;

    [HideInInspector] public object settingType;

    [HideInInspector] public UnityEvent onBeforePreviousAction = new UnityEvent();
    [HideInInspector] public UnityEvent onBeforeNextAction = new UnityEvent();
    [HideInInspector] public UnityEvent completeAction = new UnityEvent();

    private bool canClick = true;
    private LanguageSwitchItem currentContent;
    private LanguageSwitchItem tempContent;

	void Awake ()
	{
	    currentContent = content_1;
	    tempContent = content_2;
	}

    void Start()
    {
        tempContent.target.color = new Color(tempContent.target.color.r, tempContent.target.color.g, tempContent.target.color.b, 0);
    }

    public void OnContentChange(bool Previous)
    {
        if (canClick)
        {
            canClick = false;

            if (Previous)
                onBeforePreviousAction.Invoke();
            else
                onBeforeNextAction.Invoke();


            int direction = Previous ? -1 : 1;

            tempContent.transform.localPosition = new Vector3(currentContent.transform.localPosition.x - direction * fadeDistance, tempContent.transform.localPosition.y, tempContent.transform.localPosition.z);

            currentContent.target.DOFade(0, 0.3f);
            currentContent.transform.DOLocalMoveX(direction * fadeDistance, 0.3f, true).SetRelative(true);

            tempContent.target.DOFade(1, 0.3f);
            tempContent.transform.DOLocalMoveX(direction * fadeDistance, 0.3f, true).SetRelative(true).OnComplete(() =>
            {
                completeAction.Invoke();
                var temp = tempContent;
                tempContent = currentContent;
                currentContent = temp;
                canClick = true;
            });
        }
    }

    public void SetTempContentTextWithId<T>(string idPrefix, T type)
    {
        settingType = type;
        tempContent.ChangeTextWithId(idPrefix + type);
    }

    public void SetCurrentContentTextWithId(string id)
    {
        currentContent.ChangeTextWithId(id);
    }
}
