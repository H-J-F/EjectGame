using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProperty : MonoBehaviour
{
    public int id;
    public bool isReceivePos = false;
    public Vector3 targetPosition = Vector3.zero;

    private readonly float DISTANCE_OFFET = 0.001f;

    private bool isInitPos = false;

    void Update()
    {
        if (isReceivePos && !isInitPos)
        {
            transform.position = targetPosition;
            isInitPos = true;
        }

        if (isReceivePos && Vector3.Distance(targetPosition, transform.position) > DISTANCE_OFFET)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);
        }
    }
}
