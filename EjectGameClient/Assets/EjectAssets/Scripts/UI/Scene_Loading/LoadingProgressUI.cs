using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgressUI : MonoBehaviour 
{
    #region public field
    
    public Color progressColor;
    public Text progressText;
    public Image[] progressBars;

	#endregion


	#region private field

    private bool isLerping = true;
    private int stepIndex = 0;
    private int nextStep;
    private int percentStep = 0;
    private int nexPercentStep = 0;
    private float progressTarget = 0f;
    private float loadingSpeed = 0.5f;

    #endregion


    #region monobehavior callbacks

    void Start ()
	{
	    
	}

    void Update()
    {
        if (isLerping)
        {
            float progressValue = AsyncLoadScene.instance.GetLoadingProgress();
            progressTarget = Mathf.Lerp(progressTarget, progressValue, loadingSpeed * Time.deltaTime);

            nextStep = Mathf.RoundToInt(progressTarget * 13f);

            if (nextStep - stepIndex >= 1)
            {
                int count = nextStep - stepIndex;

                for (int i = 0; i < count; i++)
                {
                    progressBars[stepIndex + i].DOColor(progressColor, 0.5f);
                }
                
                stepIndex = nextStep;
            }

            nexPercentStep = Mathf.RoundToInt(progressTarget / 0.9f * 100);

            if (nexPercentStep - percentStep >= 1)
            {
                percentStep = nexPercentStep;
                progressText.text = Mathf.RoundToInt(progressTarget / 0.9f * 100).ToString();

                if (percentStep == 100)
                {
                    isLerping = false;
                    StartCoroutine(ActivateNextScene());
                }
            }
        }
    }

	#endregion

	#region custom methods



	#endregion


	#region custom coroutines

    private IEnumerator ActivateNextScene()
    {
        yield return new WaitForSeconds(2f);
        AsyncLoadScene.instance.ActivateNextScene();
    }

    #endregion
}
