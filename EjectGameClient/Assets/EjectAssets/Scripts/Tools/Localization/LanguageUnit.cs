using System;
using System.Collections;
using System.Collections.Generic;

namespace EjectGame.Language
{
    [Serializable]
    public struct LanguageUnit : IEquatable<LanguageUnit>
    {
        public string textId;
        public string content;
        public int fontSize;
        public float lineSpace;

        public bool Equals(LanguageUnit other)
        {
            return textId == other.textId 
                   && content == other.content 
                   && fontSize == other.fontSize
                   && lineSpace - other.lineSpace < 0.0001f;
        }
    }
}