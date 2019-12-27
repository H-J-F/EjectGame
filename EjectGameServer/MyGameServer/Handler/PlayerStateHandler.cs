using System;
using System.Collections.Generic;
using Common;
using Common.Tools;
using Photon.SocketServer;

namespace MyGameServer.Handler
{
    class PlayerStateHandler : BaseHandler
    {
        public PlayerStateHandler()
        {
            opCode = OperationCode.PlayerHp;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            float playerHp = (float)DictTool.GetValue(operationRequest.Parameters, (byte)ParameterCode.PlayerHp);
            

            foreach (var tempPeer in MyGameServer.ServerInstance.peerList)
            {
                if (!string.IsNullOrEmpty(tempPeer.username) && tempPeer != peer && tempPeer.isJoin)
                {
                    EventData ed = new EventData((byte)EventCode.PlayerHp);
                    Dictionary<byte, object> eventData = new Dictionary<byte, object>();
                    eventData.Add((byte)ParameterCode.PlayerHp, playerHp);
                    ed.Parameters = eventData;
                    tempPeer.SendEvent(ed, sendParameters);
                }
            }
        }
    }
}
