using System.Collections;
using System.Collections.Generic;
using EjectGame.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointerHitManager : BaseManager<PointerHitManager>
{
	#region public field

    

	#endregion


	#region private field
    
    private string pointerHitEffectName = "PointerHitEffect";
    private Camera uiCamera;
    private Canvas effectCanvas;
    private RectTransform effectCanvasRectTrans;

    private List<UIEffect> m_hitEffects;

    #endregion


    #region monobehavior callbacks

    protected override void Awake()
    {
        base.Awake();
        uiCamera = GameObject.FindGameObjectWithTag("UI Camera").GetComponent<Camera>();
        effectCanvas = GameObject.FindGameObjectWithTag("Effect Canvas").GetComponent<Canvas>();
        effectCanvasRectTrans = effectCanvas.GetComponent<RectTransform>();
        SetupHitEffects();
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Touch[] allTouches = Input.touches;
            foreach (Touch t in allTouches)
            {
                if (t.phase == TouchPhase.Began)
                {
                    ShowEffect(t.position);
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowEffect(Input.mousePosition);
            }
        }
    }

    #endregion

	#region custom methods

    private void SetupHitEffects()
    {
        m_hitEffects = new List<UIEffect>();

        for (int i = 0; i < EjectTools.pointerHitEffectCount; i++)
        {
            GameObject hitEffectPrefab = Resources.Load<GameObject>(EjectTools.uiEffectPathPrefix + pointerHitEffectName);
            UIEffect hitEffect = Instantiate(hitEffectPrefab, effectCanvas.transform).GetComponent<UIEffect>();

            hitEffect.HideSelf();
            hitEffect.SetParent(effectCanvas.transform);

            m_hitEffects.Add(hitEffect);
        }
    }

    private UIEffect GetHitEffect()
    {
        foreach (UIEffect effect in m_hitEffects)
        {
            if (effect.IsEffectFree())
            {
                return effect;
            }
        }

        return null;
    }

    private void ShowEffect(Vector2 clickPosition)
    {
        UIEffect hitEffect = GetHitEffect();

        if (hitEffect == null)
        {
            Debug.LogWarning("当前屏幕点击数过多，无空余点击特效。");
            return;
        }

        Vector2 mouseWorldPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(effectCanvasRectTrans, clickPosition, uiCamera, out mouseWorldPos);

        hitEffect.SetPosition(mouseWorldPos);
        hitEffect.Show();
        StartCoroutine(HideHitEffectCoroutine(hitEffect));
    }

    #endregion


    #region custom coroutines

    private IEnumerator HideHitEffectCoroutine(UIEffect effect)
    {
        yield return new WaitForSeconds(0.6f); // 点击特效的最长 Start Lifetime
        effect.HideSelf();
    }

	#endregion
}
