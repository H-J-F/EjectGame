using System.Collections;
using System.Collections.Generic;
using EjectGame.Tools;
using UnityEngine;


public class SelfRotate : MonoBehaviour 
{
    #region public field
    
    public RotateOption option = RotateOption.RegularAxis;
    public CustomRotateAxis rotateAxis = CustomRotateAxis.AxisY;
    public Vector3 customAxisVector3 = Vector3.zero;

    public float angularVelocity;

    #endregion


    #region private field

    private Vector3 m_rotateAxis;

    #endregion


    #region monobehavior callbacks

    void Start()
    {
        switch (option)
        {
            case RotateOption.PointToCamera:

                transform.LookAt(Camera.main.transform);
                m_rotateAxis = Vector3.forward;
                break;

            case RotateOption.CustomAxis:

                m_rotateAxis = customAxisVector3;
                break;

            case RotateOption.RegularAxis:

                switch (rotateAxis)
                {
                    case CustomRotateAxis.AxisX:
                        m_rotateAxis = Vector3.right;
                        break;
                    case CustomRotateAxis.AxisY:
                        m_rotateAxis = Vector3.up;
                        break;
                    case CustomRotateAxis.AxisZ:
                        m_rotateAxis = Vector3.forward;
                        break;
                }
                break;
        }
    }

    void Update ()
    {
        transform.Rotate(m_rotateAxis, angularVelocity * Time.deltaTime, Space.Self);
    }

	#endregion

	#region custom methods



	#endregion


	#region custom coroutines



	#endregion
}
