using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EjectGame.Tools;
using UnityEngine;
using UnityEngine.Audio;

namespace EjectGame.General
{
    public class AudioManager : BaseManager<AudioManager>
    {
        public AudioMixerGroup effectAudioMixerGroup;
        public AudioMixerGroup sfxAudioMixerGroup;
        
        [HideInInspector] public AudioClipInfo normalHoverAudio;
        [HideInInspector] public AudioClipInfo normalClickAudio;
        [HideInInspector] public AudioClipInfo electronicHoverAudio;
        [HideInInspector] public AudioClipInfo metalClickAudio;

        private static AudioSource[] m_effectAudioSources;
        private static AudioSource[] m_sfxAudioSources;
        private static GameObject m_effectAudioSourecesGameObject;
        private static GameObject m_sfxAudioSourecesGameObject;
        private static Queue<AudioClipInfo> m_effectAudioQueue;
        private static Queue<AudioClipInfo> m_sfxAudioQueue;

        protected override void Awake()
        {
            base.Awake();
            
            int effectAudioCount = GeneralSettingsManager.instance.settings.reserveEffectAudioSources;
            int sfxAudioCount = GeneralSettingsManager.instance.settings.reserveSfxAudioSources;

            enabled = false;
            StartCoroutine(WaitFrameAndEnable(5));

            m_effectAudioQueue = new Queue<AudioClipInfo>(effectAudioCount);
            m_sfxAudioQueue = new Queue<AudioClipInfo>(sfxAudioCount);

            SetupFrequentlyAudios();
            CreateAudioSourcesPool(out m_effectAudioSources, effectAudioCount, out m_effectAudioSourecesGameObject, "EffectAudioSourcesPool", effectAudioMixerGroup);
            CreateAudioSourcesPool(out m_sfxAudioSources, sfxAudioCount, out m_sfxAudioSourecesGameObject, "SfxAudioSourcesPool", sfxAudioMixerGroup);
        }

        void Update()
        {
            AudioQueuePlay(m_effectAudioQueue, m_effectAudioSources);
            AudioQueuePlay(m_sfxAudioQueue, m_sfxAudioSources);
        }

        private IEnumerator WaitFrameAndEnable(int frames)
        {
            for (int i = 0; i < frames; i++)
            {
                yield return null;
            }

            enabled = true;
        }

        private void SetupFrequentlyAudios()
        {
            normalHoverAudio = Resources.Load<AudioClipInfo>(EjectTools.uiAudioPathPrefix + "NormalHoverAudio");
            normalClickAudio = Resources.Load<AudioClipInfo>(EjectTools.uiAudioPathPrefix + "NormalClickAudio");
            electronicHoverAudio = Resources.Load<AudioClipInfo>(EjectTools.uiAudioPathPrefix + "ElectronicHoverAudio");
            metalClickAudio = Resources.Load<AudioClipInfo>(EjectTools.uiAudioPathPrefix + "MetalClickAudio");
        }

        private void AudioQueuePlay(Queue<AudioClipInfo> queue, AudioSource[] audioSources)
        {
            if (queue.Count > 0)
            {
                var source = GetNextAudioSource(audioSources);
                var clip = queue.Dequeue();

                if (source != null)
                {
                    source.Play(clip);
                }
            }
        }

        private void CreateAudioSourcesPool(out AudioSource[] audioSources, int arrayLength, out GameObject target, string targetName, AudioMixerGroup group)
        {
            audioSources = new AudioSource[arrayLength];

            target = new GameObject(targetName);
            target.transform.SetParent(transform);
            target.transform.position = Vector3.zero;

            for (int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i] = target.AddComponent<AudioSource>();
                audioSources[i].outputAudioMixerGroup = group;
            }
        }

        private AudioSource GetNextAudioSource(AudioSource[] audioSources)
        {
            foreach (AudioSource audio in audioSources)
            {
                if (!audio.isPlaying)
                {
                    return audio;
                }
            }

            Debug.LogWarning("未找到空闲的 AudioSource，无法播放音乐");
            return null;
        }
        
        public void EffectAudioPlayOneShot(AudioClipInfo clipInfo)
        {
            AudioPlayOneShot(m_effectAudioQueue, clipInfo);
        }

        public void SfxAudioPlayOneShot(AudioClipInfo clipInfo)
        {
            AudioPlayOneShot(m_sfxAudioQueue, clipInfo);
        }

        public void AudioPlayOneShot(Queue<AudioClipInfo> audioQueue, AudioClipInfo clipInfo)
        {
            if (clipInfo == null || clipInfo.audioClip == null)
            {
                return;
            }

            if (instance == null)
            {
                Debug.LogWarning("未找到 AudioManager，无法播放音乐");
            }

            if (audioQueue.Any(o => o.audioClip == clipInfo.audioClip) == false)
            {
                audioQueue.Enqueue(clipInfo);
            }
        }
    }
}