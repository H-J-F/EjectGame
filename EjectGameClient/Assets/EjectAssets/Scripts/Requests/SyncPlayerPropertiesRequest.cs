using System.Collections;
using System.Collections.Generic;
using Common;
using ExitGames.Client.Photon;
using UnityEngine;

public class SyncPlayerPropertiesRequest : Request
{
    [HideInInspector]public Vector3 pos;
    [HideInInspector]public float rotationY;
    [HideInInspector]public float cosValue;
    [HideInInspector]public float dotValue;

    // Use this for initialization
    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        //data.Add((byte) ParameterCode.Position, new Vector3Data() {x = pos.x, y = pos.y, z = pos.z});
        data.Add((byte) ParameterCode.X, pos.x);
        data.Add((byte) ParameterCode.Y, pos.y);
        data.Add((byte) ParameterCode.Z, pos.z);
        data.Add((byte) ParameterCode.RotationY, rotationY);
        data.Add((byte) ParameterCode.DotValue, dotValue);
        data.Add((byte) ParameterCode.CosValue, cosValue);
        //data.Add((byte) ParameterCode.NormalAttack, normalAttack);
        //data.Add((byte) ParameterCode.UtraAttack, utraAttack);
        PhotonEngine.Peer.OpCustom((byte)opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        
    }

    public void SetData(Vector3 position, float rotationY, float cosValue, float dotValue)
    {
        pos = position;
        this.rotationY = rotationY;
        this.cosValue = cosValue;
        this.dotValue = dotValue;
    }
}
