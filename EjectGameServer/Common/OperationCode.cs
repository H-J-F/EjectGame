﻿
namespace Common
{
    public enum OperationCode : byte // 区分请求和响应的类型
    {
        Login,
        Register,
        Default,
        SyncPosition,
        SyncPlayer,
        SyncAttack,
        SyncMissile,
        GenerateMissile,
        PlayerHp
    }
}
