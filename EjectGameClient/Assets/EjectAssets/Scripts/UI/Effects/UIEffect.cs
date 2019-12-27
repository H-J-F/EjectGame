using System.Collections;
using System.Collections.Generic;
using EjectGame.Tools;
using UnityEngine;

public class UIEffect : MonoBehaviour 
{
    #region public field



    #endregion


    #region private field

    protected ParticleSystem particleSystem;
    protected Renderer[] renderers;

    #endregion


    #region monobehavior callbacks

    protected virtual void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        renderers = GetComponentsInChildren<Renderer>();
        SetupRenderer();
    }

    protected virtual void Start()
    {

    }

    #endregion

    #region custom methods

    protected virtual void SetupRenderer()
    {
        foreach (var renderer in renderers)
        {
            renderer.sortingLayerName = EjectTools.effectSortingLayerName;
            renderer.sortingOrder = EjectTools.effectOrderInLayer;
        }
    }

    public virtual void Play()
    {
        if (particleSystem.isPlaying == false)
        {
            particleSystem.Play(true);
        }
        else
        {
            Stop();
            particleSystem.Play(true);
        }
    }

    public virtual void Stop()
    {
        particleSystem.Stop(true);
    }

    public virtual void Show()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        else
        {
            Play();
        }
    }

    public virtual void HideSelf()
    {
        gameObject.SetActive(false);
    }

    public virtual void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public virtual void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    public virtual void SetParentAndPos(Transform parent, Vector3 position)
    {
        transform.SetParent(parent);
        transform.localPosition = position;
    }

    public virtual bool IsEffectFree()
    {
        return gameObject.activeSelf == false;
    }

    #endregion


    #region custom coroutines



    #endregion
}
