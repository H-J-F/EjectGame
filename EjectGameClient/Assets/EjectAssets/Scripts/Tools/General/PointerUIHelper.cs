using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EjectGame.General
{
    public class PointerUIHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler
    {
        public UnityEvent onPointerEnter = new UnityEvent();
        public UnityEvent onPointerExit = new UnityEvent();
        public UnityEvent onPointerClick = new UnityEvent();
        public UnityEvent onPointerDown = new UnityEvent();
        public UnityEvent onPointerUp = new UnityEvent();
        public UnityEvent onSelect = new UnityEvent();

        private Image targetImg;
        private Selectable selectable;

        void Awake()
        {
            targetImg = GetComponent<Image>();
            selectable = GetComponent<Selectable>();
        }

        void Start() { }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
            {
                if ((selectable != null && selectable.IsInteractable()) || selectable == null)
                    onPointerEnter.Invoke();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
            {
                if ((selectable != null && selectable.IsInteractable()) || selectable == null)
                    onPointerExit.Invoke();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if ((selectable != null && selectable.IsInteractable()) || selectable == null)
                onPointerClick.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if ((selectable != null && selectable.IsInteractable()) || selectable == null)
                onPointerDown.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if ((selectable != null && selectable.IsInteractable()) || selectable == null)
                onPointerUp.Invoke();
        }

        public void OnSelect(BaseEventData eventData)
        {
            onSelect.Invoke();
        }


        #region Custom Methods

        public void PlayAudioClip(AudioClipInfo clip)
        {
            AudioManager.instance.EffectAudioPlayOneShot(clip);
        }

        public void PlayNormalHoverAudioClip()
        {
            AudioManager.instance.EffectAudioPlayOneShot(AudioManager.instance.normalHoverAudio);
        }

        public void PlayNormalClickAudioClip()
        {
            AudioManager.instance.EffectAudioPlayOneShot(AudioManager.instance.normalClickAudio);
        }

        public void PlayElectronicHoverAudioClip()
        {
            AudioManager.instance.EffectAudioPlayOneShot(AudioManager.instance.electronicHoverAudio);
            }

        public void PlayMetalClickAudioClip()
        {
            AudioManager.instance.EffectAudioPlayOneShot(AudioManager.instance.metalClickAudio);
        }

        public void SwapSprite(Sprite sprite)
        {
            if (targetImg != null)
            {
                targetImg.sprite = sprite;
            }
        }

        public void SwapColor(Color color)
        {
            if (targetImg != null)
            {
                targetImg.DOColor(color, 0.2f);
            }
        }

        #endregion
    }
}