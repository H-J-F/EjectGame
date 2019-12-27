using System.Collections;
using System.Collections.Generic;
using EjectGame.General;
using EjectGame.Tools;
using UnityEngine;
using UnityEngine.UI;

public class FrequentlyPointerUIHelper : MonoBehaviour 
{
	#region public field

    public bool isSwapSprite;
    public bool isSwapColor;
    public FrequentlyPointerAudio hoverAudio;
    public FrequentlyPointerAudio clickAudio;
    public Sprite highlightSprite = null;
    public Sprite clickSprite = null;
    public Color normalColor;
    public Color highlightColor;
    public Color clickColor;

    #endregion


    #region private field

    private PointerUIHelper uiHelper;
    private Image targetImg;
    private Sprite rawSprite;

    #endregion


    #region monobehavior callbacks

    void Awake()
    {
        uiHelper = gameObject.AddComponent<PointerUIHelper>();
        targetImg = GetComponent<Image>();
        SetupUIPointerHelper();
    }

    void Start () {
		
	}

	#endregion

	#region custom methods

    private void SetupUIPointerHelper()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
        {
            uiHelper.onPointerEnter.AddListener(() =>
            {
                switch (hoverAudio)
                {
                    case FrequentlyPointerAudio.None:
                        break;
                    case FrequentlyPointerAudio.NormalHover:
                        uiHelper.PlayNormalHoverAudioClip();
                        break;
                    case FrequentlyPointerAudio.NormalClick:
                        uiHelper.PlayNormalClickAudioClip();
                        break;
                    case FrequentlyPointerAudio.ElectronicHover:
                        uiHelper.PlayElectronicHoverAudioClip();
                        break;
                    case FrequentlyPointerAudio.MetalClick:
                        uiHelper.PlayMetalClickAudioClip();
                        break;
                }

                if (isSwapSprite && targetImg != null && targetImg.sprite != null && highlightSprite != null)
                {
                    uiHelper.SwapSprite(highlightSprite);
                }

                if (isSwapColor && targetImg != null)
                {
                    uiHelper.SwapColor(highlightColor);
                }
            });
        }

        uiHelper.onPointerClick.AddListener(() =>
        {
            switch (clickAudio)
            {
                case FrequentlyPointerAudio.None:
                    break;
                case FrequentlyPointerAudio.NormalHover:
                    uiHelper.PlayNormalHoverAudioClip();
                    break;
                case FrequentlyPointerAudio.NormalClick:
                    uiHelper.PlayNormalClickAudioClip();
                    break;
                case FrequentlyPointerAudio.ElectronicHover:
                    uiHelper.PlayElectronicHoverAudioClip();
                    break;
                case FrequentlyPointerAudio.MetalClick:
                    uiHelper.PlayMetalClickAudioClip();
                    break;
            }
        });

        if (isSwapSprite && targetImg != null && targetImg.sprite != null)
        {
            rawSprite = targetImg.sprite;

            if (clickSprite != null)
            {
                uiHelper.onPointerDown.AddListener(() =>
                {
                    uiHelper.SwapSprite(clickSprite);
                });

                uiHelper.onPointerUp.AddListener(() =>
                {
                    uiHelper.SwapSprite(rawSprite);
                });
            }

            uiHelper.onPointerExit.AddListener(() =>
            {
                uiHelper.SwapSprite(rawSprite);
            });
        }

        if (isSwapColor && targetImg != null)
        {
            uiHelper.onPointerDown.AddListener(() =>
            {
                uiHelper.SwapColor(clickColor);
            });

            uiHelper.onPointerUp.AddListener(() =>
            {
                uiHelper.SwapColor(normalColor);
            });

            uiHelper.onPointerExit.AddListener(() =>
            {
                uiHelper.SwapColor(normalColor);
            });
        }
    }

	#endregion


	#region custom coroutines



	#endregion
}
