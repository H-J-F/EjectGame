using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EjectGame.General
{
    [Serializable]
    public class AudioClipInfo : MonoBehaviour
    {
        public AudioClip audioClip;
        public float volume = 1f;
        public float pitch = 1;
        public bool loop = false;
    }
}