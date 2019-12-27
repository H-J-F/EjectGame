using System.Collections;
using System.Collections.Generic;
using Devdog.SciFiDesign.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SkipStartSceneBeginning : MonoBehaviour 
{
	#region public field

    public Text skipTipText;
    public Image skipMaskImg;
    
    // 以下是被控制的所有组件
    public Animator camvasAnimator;
    public PlayableDirector timeline;
    public CameraShowDanger cameraShowDanger;

    public ShaderValueInterpolator topLeftFrame;
    public ShaderValueInterpolator topRightFrame;

    public RectTransform topImg;
    public RectTransform bottomImg;
    public RectTransform reticleRect;
    public RectTransform reticleLineRect;
    public Transform bottomLeftFrame;
    public Transform bottomRightFrame;

    public Image reticle;
    public Image reticleLine;
    
    public GameObject circleLeftSide;
    public GameObject circleRightSide;

    public Slider O2_Slider;
    public Slider HP_Slider;
    public Slider HealthSlider;
    public Slider shieldSlider;
    public Slider energySlider;

    public GameObject[] TurnOffGameObjects;
    public GameObject[] TurnOnGameObjectss;

    public Image[] powerImgs;

	#endregion


	#region private field

    private bool isClicked = false;
    private Image[] circleDegrees;

	#endregion


	#region monobehavior callbacks

    void Awake()
    {
        SetupCircleDegreesArray();
    }

	void Start () {
		
	}

	#endregion

	#region custom methods

    public void ShotDownSkipMask()
    {
        skipMaskImg.raycastTarget = false;
    }

    public void OnClick()
    {
        if (!isClicked)
        {
            isClicked = true;
            skipTipText.DOFade(1f, 0.5f);
            StartCoroutine(ClickSkipTiming());
        }
        else
        {
            StopStartSceneAnim();
        }
    }

    private void StopStartSceneAnim()
    {
        ShotDownSkipMask();
        camvasAnimator.enabled = false;
        timeline.Stop();

        if (cameraShowDanger.gameObject.activeSelf)
        {
            cameraShowDanger.ResetVCAIntensity();
            cameraShowDanger.DisableSelf();
        }

        topLeftFrame.enabled = false;
        topRightFrame.enabled = false;

        topLeftFrame.gameObject.SetActive(true);
        topRightFrame.gameObject.SetActive(true);
        
        skipTipText.gameObject.SetActive(false);

        reticle.color = new Color(reticle.color.r, reticle.color.g, reticle.color.b, 1f);
        reticleLine.color = new Color(reticle.color.r, reticle.color.g, reticle.color.b, 1f);

        reticleRect.anchoredPosition = Vector2.zero;
        reticleLineRect.anchoredPosition = Vector2.zero;
        bottomLeftFrame.localScale = new Vector3(-1, 1, 1);
        bottomRightFrame.localScale = Vector3.one;
        topImg.anchoredPosition = Vector2.zero;
        bottomImg.anchoredPosition = Vector2.zero;

        O2_Slider.value = 0.3f;
        HP_Slider.value = 1f;
        HealthSlider.value = 1f;
        shieldSlider.value = 0.05f;
        energySlider.value = 0.05f;

        foreach (Image img in powerImgs)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
        }

        foreach (Image degree in circleDegrees)
        {
            degree.color = new Color(degree.color.r, degree.color.g, degree.color.b, 1f);
        }

        foreach (GameObject o in TurnOnGameObjectss)
        {
            o.SetActive(true);
        }

        foreach (GameObject o in TurnOffGameObjects)
        {
            o.SetActive(false);
        }
    }

    private void SetupCircleDegreesArray()
    {
        Image[] leftDegreeImgs = circleLeftSide.GetComponentsInChildren<Image>();
        Image[] rightDegreeImgs = circleRightSide.GetComponentsInChildren<Image>();
        circleDegrees = new Image[leftDegreeImgs.Length + rightDegreeImgs.Length];
        leftDegreeImgs.CopyTo(circleDegrees, 0);
        rightDegreeImgs.CopyTo(circleDegrees, leftDegreeImgs.Length);
    }

	#endregion


	#region custom coroutines

    private IEnumerator ClickSkipTiming()
    {
        yield return new WaitForSeconds(2.5f);
        skipTipText.DOFade(0f, 0.5f).OnComplete(() =>
        {
            isClicked = false;
        });
    }

	#endregion
}
