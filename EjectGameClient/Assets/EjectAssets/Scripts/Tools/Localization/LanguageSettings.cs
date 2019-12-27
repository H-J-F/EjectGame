using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EjectGame.Tools;

namespace EjectGame.Language
{
    [CreateAssetMenu(menuName = "EjectGame/Language Settings")]
    public class LanguageSettings : ScriptableObject
    {
        #region public field

        [Header("Language")]
        public LanguageType language;

        [Header("Static Language Unit")]
        public LanguageUnit[] languageUnits;

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