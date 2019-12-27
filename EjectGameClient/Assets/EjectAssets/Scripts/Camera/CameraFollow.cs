using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetTrans;

    private readonly float m_smoothTime = 0.5f;
    private Vector3 m_velocity = Vector3.zero;
    private Vector3 m_playerToCamera;
    
	void Start ()
	{
	    m_playerToCamera = transform.position - targetTrans.position;
	}
	
	void Update ()
	{
	    Vector3 target = targetTrans.position + m_playerToCamera;

	    if (Vector3.Distance(target, transform.position) > 0.001f)
	    {
            //transform.position = Vector3.Lerp(transform.position, target, 4f * Time.deltaTime);
	        transform.position = Vector3.SmoothDamp(transform.position, target, ref m_velocity, m_smoothTime);
        }
    }
}
