using System.Collections;
using System.Collections.Generic;
using EjectGame.Tools;

namespace EjectData
{
    public class ValuableItemData : BaseItemData
    {
        public ValuableItemType valueType;
        public string valueTypeIconPath;
        public int value;

        public override bool Equals(BaseItemData other)
        {
            ValuableItemData data = (ValuableItemData) other;

            return valueType == data.valueType 
                   && valueTypeIconPath == data.valueTypeIconPath 
                   && value == data.value;
        }
    }
}