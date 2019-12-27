
namespace Common
{
    public enum ParameterCode : byte // 区分传送数据的时候，参数的类型
    {
        Username,
        Password,
        Position,
        X, Y, Z,
        UsernameList,
        PlayerDataList,
        UserData,
        PlayerJoinIn,
        RotationX,
        RotationY,
        RotationZ,
        RotationW,
        DotValue,
        CosValue,
        NormalAttack,
        UtraAttack,
        MissileDataList,
        MissileData,
        MuzzleData,
        PlayerHp
    }
}
