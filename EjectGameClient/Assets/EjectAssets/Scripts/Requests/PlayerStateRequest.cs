using System.Collections;
using System.Collections.Generic;
using Common;
using ExitGames.Client.Photon;
using UnityEngine;

public class PlayerStateRequest : Request
{
    [HideInInspector] public float playerHp;

    public override void DefaultRequest()
    {
        Debug.Log("DefaultRequest  " + playerHp);
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.PlayerHp, playerHp);
        PhotonEngine.Peer.OpCustom((byte)opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        
    }

    public void SetData(float playerHp)
    {
        this.playerHp = playerHp;
    }
}