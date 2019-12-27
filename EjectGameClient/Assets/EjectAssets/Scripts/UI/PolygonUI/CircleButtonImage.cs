using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(CircleCollider2D))]
public class CircleButtonImage : Image 
{
    private CircleCollider2D areaCircle;

    private CircleCollider2D Circle
    {
        get
        {
            if (areaCircle == null)
            {
                areaCircle = GetComponent<CircleCollider2D>();
            }

            return areaCircle;
        }
    }

    protected CircleButtonImage()
    {
        useLegacyMeshGeneration = true;
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
    }

    public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        return Circle.OverlapPoint(eventCamera.ScreenToWorldPoint(screenPoint));
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CircleButtonImage), true)]
public class CircleButtonImageInspector : Editor
{
    public override void OnInspectorGUI()
    {

    }
}
#endif