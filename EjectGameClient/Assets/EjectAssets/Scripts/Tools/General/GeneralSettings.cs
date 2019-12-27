using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EjectGame.General
{
    [CreateAssetMenu(menuName = "EjectGame/General Settings")]
    public class GeneralSettings : ScriptableObject
    {
        #region public field

        [Header("Cursor")]
        public CursorIcon defaultCursor;

        [Header("Effect Audio")]
        public int reserveEffectAudioSources = 6;

        [Header("Sfx Audio")]
        public int reserveSfxAudioSources = 6;

        #endregion


        #region private field



        #endregion


        #region monobehavior callbacks



        #endregion

        #region custom methods



        #endregion


        #region custom coroutines



        #endregion
    }
}