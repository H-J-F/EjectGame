using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObstacle : MonoBehaviour 
{
    #region public field

    public float fadeAlpha = 0.3f;
    public Material opaqueMaterial;
    public Material fadeMaterial;

    #endregion


    #region private field

    private int m_layerMaskIndex;
    private List<Renderer> m_rendererList = new List<Renderer>();
    private List<Renderer> m_rendererListCopy = new List<Renderer>();
    private List<Renderer> m_opaqueRendererList = new List<Renderer>();

    #endregion


    #region monobehavior callbacks

    void Awake()
    {
        m_layerMaskIndex = 1 << LayerMask.NameToLayer("AlphaEnvironment");
    }

    void Start () {
		
	}
	
	void Update () {
	    RaycastHit[] hits;
	    hits = Physics.RaycastAll(transform.position, Camera.main.transform.position - transform.position, 30f, m_layerMaskIndex);

	    m_rendererListCopy = new List<Renderer>(m_rendererList);

        if (hits.Length > 0)
	    {
	        foreach (RaycastHit hit in hits)
	        {
	            Renderer tempRenderer = hit.collider.transform.parent.GetComponent<Renderer>();
	            if (tempRenderer != null)
	            {
	                if (!m_rendererList.Contains(tempRenderer))
	                {
	                    m_rendererList.Add(tempRenderer);
	                    tempRenderer.material = fadeMaterial;
                    }
	                else
	                {
	                    m_rendererListCopy.Remove(tempRenderer);
	                }
	            }
	        }
	    }

	    foreach (Renderer fadeRenderer in m_rendererList)
	    {
	        if (m_opaqueRendererList.Contains(fadeRenderer))
	        {
	            m_opaqueRendererList.Remove(fadeRenderer);
	        }
	    }

	    foreach (Renderer opaqueRenderer in m_rendererListCopy)
	    {
	        m_rendererList.Remove(opaqueRenderer);

	        if (!m_opaqueRendererList.Contains(opaqueRenderer))
	        {
	            m_opaqueRendererList.Add(opaqueRenderer);
	        }
	    }

        MaterialFade();
	    MaterialOpaque();
    }

    #endregion

    #region custom methods

    private void MaterialFade()
    {
        for (int i = m_rendererList.Count - 1; i >= 0; i--)
        {
            if (m_rendererList[i].material.color.a > fadeAlpha)
            {
                Color color = m_rendererList[i].material.color;
                color.a -= 0.01f;
                m_rendererList[i].material.color = color;
            }
        }
    }

    private void MaterialOpaque()
    {
        for (int i = m_opaqueRendererList.Count - 1; i >= 0; i--)
        {
            if (m_opaqueRendererList[i].material.color.a < 1f)
            {
                Color color = m_opaqueRendererList[i].material.color;
                color.a += 0.01f;
                m_opaqueRendererList[i].material.color = color;
            }
            else
            {
                m_opaqueRendererList[i].material = opaqueMaterial;
                m_opaqueRendererList.Remove(m_opaqueRendererList[i]);
            }
        }
    }

    #endregion


    #region custom coroutines



    #endregion
}