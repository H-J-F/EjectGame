using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using Common.Tools;
using ExitGames.Client.Photon;

public class SyncPlayerRequest : Request
{

    private PlayerController playerCtrl;

    protected override void Start()
    {
        base.Start();
        playerCtrl = GetComponent<PlayerController>();
    }
    
    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.PlayerJoinIn, true);
        PhotonEngine.Peer.OpCustom((byte)opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        string usernameListToString = (string) DictTool.GetValue(operationResponse.Parameters, (byte) ParameterCode.UsernameList);

        using (StringReader reader = new StringReader(usernameListToString))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            List<string> usernameList = (List<string>) serializer.Deserialize(reader);
            playerCtrl.OnSyncPlayerResponse(usernameList);
        }
    }
}
