using System;
using System.Collections;
using System.Collections.Generic;


namespace EjectGame.Tools
{
    public enum SubmitButtonType
    {
        Login,
        Register
    }

    public enum ButtonTransition
    {
        None,
        ColorTint,
        SpriteSwap
    }

    public enum CustomRotateAxis
    {
        AxisX,
        AxisY,
        AxisZ
    }

    public enum RotateOption
    {
        PointToCamera,
        CustomAxis,
        RegularAxis
    }

    public enum ScrollRectDirection
    {
        Horizontal,
        Vertical
    }

    public enum ValuableItemType
    {
        Coin,
        Diamound
    }

    public enum GameFrameRateType
    {
        Low = 30,
        Medium = 45,
        High = 60,
        Most = 80
    }

    public enum GameQuality
    {
        Low = 0,
        High = 1,
        Fantastic = 2
    }

    public enum GameScenes
    {
        Start,
        MainMenu,
        Level_LavaHell
    }

    public enum FrequentlyPointerAudio
    {
        None,
        NormalHover,
        ElectronicHover,
        NormalClick,
        MetalClick
    }

    public enum LanguageType
    {
        English,
        ZH_CN,
        ZH_TW
    }

    public enum RenderingMode
    {
        Opaque,
        Fade
    }

    public enum WindowOpenAnimType
    {
        None,
        Popup,
        Fade,
        Fade_Popup,
        Noise
    }

    public enum WindowCloseAnimType
    {
        None,
        Shrink,
        Fade,
        Fade_Shrink
    }

    public class EjectTools
    {
        public static int pointerHitEffectCount = 6;
        public static int effectOrderInLayer = 100;

        public static string loadingSceneName = "Loading";
        public static string effectSortingLayerName = "UI Effect";

        public static string uiPathPrefix = "UI/";
        public static string uiIconPathPrefix = "UI/Image/Icons/";
        public static string characterEffectPathPrefix = "Effects/Characters/";
        public static string uiEffectPathPrefix = "Effects/UI Effects/";
        public static string uiAudioPathPrefix = "Audios/AudioClipInfo/UI/";
    }

    public class EnumValueListClass<T>
    {
        public List<T> valueList = new List<T>();

        private int index = 0;

        public EnumValueListClass()
        {
            foreach (var type in Enum.GetValues(typeof(T)))
            {
                valueList.Add((T)type);
            }
        }

        public void SetIndexWithEnumType(T value)
        {
            index = valueList.IndexOf(value);
        }

        public T GetListValue(bool previous)
        {
            index = previous
                ? (index == 0 ? valueList.Count - 1 : index - 1)
                : (index == valueList.Count - 1 ? 0 : index + 1);

            return valueList[index];
        }
    }
}