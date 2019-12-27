using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EjectGame.Tools
{
    public class RotateWithEulerAngle : MonoBehaviour
    {
        public Vector3 rotationAxis;

        void Start()
        {

        }

        void Update()
        {
            transform.Rotate(rotationAxis * Time.deltaTime);
        }
    }
}