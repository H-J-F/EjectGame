using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using ExitGames.Client.Photon;
using UnityEngine;

public class SyncMissileRequest : Request 
{
    [HideInInspector] public string localMissileListToString;

    // Use this for initialization
    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.MissileDataList, localMissileListToString);
        PhotonEngine.Peer.OpCustom((byte)opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {

    }

    public void SetData(List<MissileData> localMissileList)
    {
        StringWriter sw = new StringWriter();
        XmlSerializer serializer = new XmlSerializer(typeof(List<MissileData>));
        serializer.Serialize(sw, localMissileList);
        sw.Close();
        localMissileListToString = sw.ToString();
    }
}
