using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraShowDanger : MonoBehaviour 
{
    #region public field

    public float delta = 0.25f;
    public float transformSpeed = 2f;
    public float mainCameraVCATarget = -20f;
    public float uiCameraVCATarget = -12f;
    public Camera mainCamera;
    public Camera uiCamera;

	#endregion


	#region private field

    private bool m_canShow = true;
    private float _rawMainCameraVCAIntensity;
    private float _rawUiCameraVCAIntensity;
    private VignetteAndChromaticAberration _mainCameraVCA;
    private VignetteAndChromaticAberration _uiCameraVCA;

	#endregion


	#region monobehavior callbacks

    void Awake()
    {
        _mainCameraVCA = mainCamera.GetComponent<VignetteAndChromaticAberration>();
        _uiCameraVCA = uiCamera.GetComponent<VignetteAndChromaticAberration>();
    }

	void OnEnable ()
	{
	    _rawMainCameraVCAIntensity = _mainCameraVCA.intensity;
	    _rawUiCameraVCAIntensity = _uiCameraVCA.intensity;

        StartCoroutine(MainCameraShowDanger(mainCameraVCATarget));
	    StartCoroutine(UiCameraShowDanger(uiCameraVCATarget));
	}

    #endregion

    #region custom methods

    public void ResetVCAIntensity()
    {
        m_canShow = false;
        _mainCameraVCA.intensity = _rawMainCameraVCAIntensity;
        _uiCameraVCA.intensity = _rawUiCameraVCAIntensity;
    }

    public void DisableSelf()
    {
        StartCoroutine(DisableSelfCoroutine());
    }

    #endregion


    #region custom coroutines

    private IEnumerator MainCameraShowDanger(float target)
    {
        bool canLerpBack = false;

        while (!canLerpBack && m_canShow)
        {
            canLerpBack = !(Mathf.Abs(_mainCameraVCA.intensity - target) > delta);
            _mainCameraVCA.intensity = Mathf.Lerp(_mainCameraVCA.intensity, target, transformSpeed * Time.deltaTime);
            yield return null;
        }

        while (canLerpBack && m_canShow)
        {
            canLerpBack = Mathf.Abs(_mainCameraVCA.intensity - _rawMainCameraVCAIntensity) > delta;
            _mainCameraVCA.intensity = Mathf.Lerp(_mainCameraVCA.intensity, _rawMainCameraVCAIntensity, transformSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator UiCameraShowDanger(float target)
    {
        bool canLerpBack = false;

        while (!canLerpBack && m_canShow)
        {
            canLerpBack = !(Mathf.Abs(_uiCameraVCA.intensity - target) > delta);
            _uiCameraVCA.intensity = Mathf.Lerp(_uiCameraVCA.intensity, target, transformSpeed * Time.deltaTime);
            yield return null;
        }

        while (canLerpBack && m_canShow)
        {
            canLerpBack = Mathf.Abs(_uiCameraVCA.intensity - _rawUiCameraVCAIntensity) > delta;
            _uiCameraVCA.intensity = Mathf.Lerp(_uiCameraVCA.intensity, _rawUiCameraVCAIntensity, transformSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator DisableSelfCoroutine()
    {
        yield return null;
        gameObject.SetActive(false);
    }

    #endregion
}
