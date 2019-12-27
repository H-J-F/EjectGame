using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Tools;
using ExitGames.Client.Photon;

public class SyncAttackEvent : BaseEvent
{
    private PlayerController playerCtrl;

    protected override void Start()
    {
        base.Start();
        playerCtrl = GetComponent<PlayerController>();
    }

    public override void OnEvent(EventData eventData)
    {
        string username = (string)DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.Username);
        bool normalAttack = (bool)DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.NormalAttack);
        bool utraAttack = (bool)DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.UtraAttack);
        playerCtrl.OnSyncAttackEvent(username, normalAttack, utraAttack);
    }
}
