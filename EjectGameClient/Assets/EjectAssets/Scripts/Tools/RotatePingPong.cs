﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EjectGame.Tools
{
    public class RotatePingPong : MonoBehaviour
    {
        public Vector3 rotationMin;
        public Vector3 rotationMax;

        public AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        public float speed = 0.2f;
        public bool randomizeSpeed = false;

        public float maxSpeed = 1f;

        private float _speed = 0f;

        void Awake()
        {
            _speed = speed;
            if (randomizeSpeed)
            {
                _speed = Random.Range(speed, maxSpeed);
            }
        }

        void Start()
        {

        }

        void Update()
        {
            var a = Vector3.Lerp(Vector3.zero, rotationMax - rotationMin, curve.Evaluate(Mathf.PingPong(Time.time * _speed, 1f)));
            a += rotationMin;

            transform.localRotation = Quaternion.Euler(a);
        }
    }
}