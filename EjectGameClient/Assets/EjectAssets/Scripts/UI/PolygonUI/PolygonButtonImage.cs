using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(PolygonCollider2D))]
public class PolygonButtonImage : Image 
{
	#region public field



	#endregion


	#region private field

    private PolygonCollider2D areaPolygon;

    private PolygonCollider2D Polygon
    {
        get
        {
            if (areaPolygon == null)
            {
                areaPolygon = GetComponent<PolygonCollider2D>();
            }
            
            return areaPolygon;
        }
    }

    #endregion

    protected PolygonButtonImage()
    {
        useLegacyMeshGeneration = true;
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
    }

    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        return Polygon.OverlapPoint(eventCamera.ScreenToWorldPoint(screenPoint));
    }

#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        transform.localPosition = Vector3.zero;
        float w = (rectTransform.sizeDelta.x * 0.5f) + 0.1f;
        float h = (rectTransform.sizeDelta.y * 0.5f) + 0.1f;
        Polygon.points = new Vector2[]
        {
            new Vector2(-w,-h),
            new Vector2(w,-h),
            new Vector2(w,h),
            new Vector2(-w,h)
        };
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(PolygonButtonImage), true)]
public class PolygonButtonImageInspector : Editor
{
    public override void OnInspectorGUI()
    {

    }
}
#endif