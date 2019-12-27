using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SceneBlackMask : MonoBehaviour 
{
	#region public field

    public float fadeDuration = 2f;

	#endregion


	#region private field

    private Image blackMask;
    private Action loadSceneAction;

    #endregion


    #region monobehavior callbacks

    void Awake()
    {
        blackMask = GetComponent<Image>();
    }

    void Start () {
		
	}

    #endregion

    #region custom methods

    public void SetLoadSceneAction(Action action)
    {
        loadSceneAction = action;
    }

    public void BlackMaskFadeIn()
    {
        gameObject.SetActive(true);
        blackMask.color = new Color(blackMask.color.r, blackMask.color.g, blackMask.color.b, 0f);
        blackMask.DOFade(1f, fadeDuration).OnComplete(() => { loadSceneAction(); });
    }

    #endregion


    #region custom coroutines



    #endregion
}
