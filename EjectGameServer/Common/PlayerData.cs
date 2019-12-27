using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class PlayerData
    {
        public Vector3Data Pos { get; set; }
        public float RotationY { get; set; }
        public float DotValue { get; set; }
        public float CosValue { get; set; }
        public string Username { get; set; }
    }

    [Serializable]
    public class MissileData
    {
        public Vector3Data Pos { get; set; }
        public int id { get; set; }
        public int missileType { get; set; }
    }

    [Serializable]
    public class MuzzleData
    {
        public Vector3Data Pos { get; set; }
        public Vector3Data Rotation { get; set; }
        public int muzzleType { get; set; }
    }
}