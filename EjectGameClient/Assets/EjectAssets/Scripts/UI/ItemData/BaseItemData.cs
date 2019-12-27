using System;
using System.Collections;
using System.Collections.Generic;

namespace EjectData
{
    public class BaseItemData : IEquatable<BaseItemData>
    {
        public string iconPath;
        public string nameTextId;


        public virtual bool Equals(BaseItemData other)
        {
            return iconPath == other.iconPath && nameTextId == other.nameTextId;
        }
    }
}