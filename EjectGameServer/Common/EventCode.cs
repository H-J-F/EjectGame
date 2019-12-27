
namespace Common
{
    public enum EventCode : byte // 区分服务器向客户端发送事件的类型
    {
        NewPlayer,
        SyncPosition,
        SyncAttack,
        GenerateMissile,
        SyncMissile,
        PlayerHp
    }
}
