using System.Collections;
using System.Collections.Generic;
using Devdog.SciFiDesign.UI;
using DG.Tweening;
using EjectGame.Tools;
using UnityEngine;
using UnityEngine.UI;


public class PopupWindow : MonoBehaviour 
{
	#region public field

    public bool showBackgroundMask = false;
    public bool canBgMaskClickExit = false;
    public GameObject targetWindow;
    public GameObject targetWindowBgMask = null;
    public WindowOpenAnimType openType;
    public WindowCloseAnimType closeType;

	#endregion


	#region private field

    private float m_targetBgMaskAlpha;
    private Vector3 windowScale;
    private Image m_targetWinBgMaskImage;
    private Button m_targetWinBgMaskBtn;
    private AudioSource audioSource;
    private CanvasGroup m_canvasGroup;
    private ShaderValueInterpolator m_svi;

	#endregion


	#region monobehavior callbacks
    
	void Start()
	{
	    windowScale = targetWindow.transform.localScale;

	    if (targetWindowBgMask != null)
	    {
	        targetWindowBgMask.SetActive(false);
	        m_targetWinBgMaskImage = targetWindowBgMask.GetComponent<Image>();
	        m_targetBgMaskAlpha = m_targetWinBgMaskImage.color.a;

	        m_targetWinBgMaskBtn = targetWindowBgMask.GetComponent<Button>();
	        if (m_targetWinBgMaskBtn != null)
	        {
	            m_targetWinBgMaskBtn.enabled = canBgMaskClickExit;
	        }
	    }
	    
        targetWindow.SetActive(false);
	    m_canvasGroup = targetWindow.GetComponent<CanvasGroup>();

	    if (m_canvasGroup == null)
	    {
	        m_canvasGroup = targetWindow.AddComponent<CanvasGroup>();
	    }

        m_svi = targetWindow.GetComponent<ShaderValueInterpolator>();

	    if (m_svi != null)
	    {
	        m_svi.enabled = false;
	    }

	    audioSource = targetWindow.GetComponent<AudioSource>();

        switch (openType)
	    {
	        case WindowOpenAnimType.None:
                break;

            case WindowOpenAnimType.Popup:
                targetWindow.transform.localScale = new Vector3(windowScale.x, 0, windowScale.z);
                break;

            case WindowOpenAnimType.Fade:
                m_canvasGroup.alpha = 0;
                break;

            case WindowOpenAnimType.Fade_Popup:
                m_canvasGroup.alpha = 0;
                targetWindow.transform.localScale = new Vector3(windowScale.x, 0, windowScale.z);
                break;

            case WindowOpenAnimType.Noise:
                if (m_svi == null)
                {
                    openType = WindowOpenAnimType.None;
                    Debug.LogError("targetWindow 缺少 ShaderValueInterpolator 组件，已将 openType 设置为 None");
                }
                else
                {
                    m_svi.enabled = true;
                }
                break;
	    }
	}

	#endregion

	#region custom methods

    public void OnPopupWindow()
    {
        StartCoroutine(PopupWindowCoroutine());
    }

    public void OnCloseWindow()
    {
        ResetTargetWindow();

        if (showBackgroundMask)
        {
            if (closeType == WindowCloseAnimType.None)
            {
                targetWindowBgMask.SetActive(false);
            }
            else if (closeType == WindowCloseAnimType.Fade_Shrink)
            {
                m_targetWinBgMaskImage.DOFade(0f, 0.4f).OnComplete(() =>
                {
                    targetWindowBgMask.SetActive(false);
                });
            }
            else
            {
                m_targetWinBgMaskImage.DOFade(0f, 0.2f).OnComplete(() =>
                {
                    targetWindowBgMask.SetActive(false);
                });
            }
        }

        switch (closeType)
        {
            case WindowCloseAnimType.None:
                targetWindow.SetActive(false);
                break;

            case WindowCloseAnimType.Shrink:
                targetWindow.transform.DOScaleY(0f, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    targetWindow.SetActive(false);
                });
                break;

            case WindowCloseAnimType.Fade:
                m_canvasGroup.DOFade(0f, 0.2f).OnComplete(() =>
                {
                    targetWindow.SetActive(false);
                });
                break;

            case WindowCloseAnimType.Fade_Shrink:
                m_canvasGroup.DOFade(0f, 0.4f).OnComplete(() =>
                {
                    targetWindow.SetActive(false);
                });
                targetWindow.transform.DOScaleY(0f, 0.2f).SetEase(Ease.InBack);
                break;
        }
    }

    private void ResetTargetWindow()
    {
        m_canvasGroup.alpha = 1f;
        targetWindow.transform.localScale = windowScale;
    }

    #endregion


    #region custom coroutines

    private IEnumerator PopupWindowCoroutine()
    {
        targetWindow.SetActive(true);

        if (showBackgroundMask)
        {
            if (openType == WindowOpenAnimType.None)
            {
                m_targetWinBgMaskImage.color = new Color(m_targetWinBgMaskImage.color.r, m_targetWinBgMaskImage.color.g, m_targetWinBgMaskImage.color.b, m_targetBgMaskAlpha);
                targetWindowBgMask.SetActive(true);
            }
            else
            {
                m_targetWinBgMaskImage.color = new Color(m_targetWinBgMaskImage.color.r, m_targetWinBgMaskImage.color.g, m_targetWinBgMaskImage.color.b, 0);
                targetWindowBgMask.SetActive(true);
                m_targetWinBgMaskImage.DOFade(m_targetBgMaskAlpha, 0.2f);
            }
        }

        switch (openType)
        {
            case WindowOpenAnimType.None:
                m_canvasGroup.alpha = 1f;
                targetWindow.transform.localScale = windowScale;
                if (showBackgroundMask)
                {
                    targetWindowBgMask.SetActive(true);
                }
                break;

            case WindowOpenAnimType.Popup:
                m_canvasGroup.alpha = 1f;
                targetWindow.transform.localScale = new Vector3(windowScale.x, 0, windowScale.z);
                targetWindow.transform.DOScaleY(windowScale.y, 0.2f).SetEase(Ease.OutBack);
                break;

            case WindowOpenAnimType.Fade:
                m_canvasGroup.alpha = 0f;
                targetWindow.transform.localScale = windowScale;
                m_canvasGroup.DOFade(1f, 0.2f);
                break;

            case WindowOpenAnimType.Fade_Popup:
                m_canvasGroup.alpha = 0f;
                m_canvasGroup.DOFade(1f, 0.4f);
                targetWindow.transform.localScale = new Vector3(windowScale.x, 0, windowScale.z);
                targetWindow.transform.DOScaleY(windowScale.y, 0.2f).SetEase(Ease.OutBack);
                break;

            case WindowOpenAnimType.Noise:
                if (audioSource != null)
                {
                    audioSource.Play();
                }
                targetWindow.transform.localScale = new Vector3(windowScale.x, 0, windowScale.z);
                m_canvasGroup.alpha = 0f;
                m_svi.DoAnimations();
                yield return null;
                targetWindow.transform.localScale = windowScale;
                m_canvasGroup.alpha = 1f;
                break;
        }

        yield return null;
    }

    #endregion
}
