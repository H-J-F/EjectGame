using System.Collections;
using System.Collections.Generic;
using Common;
using ExitGames.Client.Photon;
using UnityEngine;

public class SyncAttackRequest : Request
{
    [HideInInspector] public bool normalAttack;
    [HideInInspector] public bool utraAttack;

    // Use this for initialization
    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.NormalAttack, normalAttack);
        data.Add((byte)ParameterCode.UtraAttack, utraAttack);
        PhotonEngine.Peer.OpCustom((byte)opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {

    }

    public void SetData(bool normalAttack = false, bool utraAttack = false)
    {
        this.normalAttack = normalAttack;
        this.utraAttack = utraAttack;
    }
}