using System;

namespace Common
{
    [Serializable]
    public class Vector3Data
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }

    [Serializable]
    public class Vector4Data
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float w { get; set; }
    }
}
