using System;
using System.Collections.Generic;
using Common;
using Common.Tools;
using Photon.SocketServer;

namespace MyGameServer.Handler
{
    class SyncAttackHandler : BaseHandler
    {
        public SyncAttackHandler()
        {
            opCode = OperationCode.SyncAttack;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            bool normalAttack = (bool)DictTool.GetValue(operationRequest.Parameters, (byte)ParameterCode.NormalAttack);
            bool utraAttack = (bool)DictTool.GetValue(operationRequest.Parameters, (byte)ParameterCode.UtraAttack);

            foreach (var tempPeer in MyGameServer.ServerInstance.peerList)
            {
                if (!string.IsNullOrEmpty(tempPeer.username) && tempPeer != peer && tempPeer.isJoin)
                {
                    EventData ed = new EventData((byte)EventCode.SyncAttack);
                    Dictionary<byte, object> eventData = new Dictionary<byte, object>();
                    eventData.Add((byte)ParameterCode.Username, peer.username);
                    eventData.Add((byte)ParameterCode.NormalAttack, normalAttack);
                    eventData.Add((byte)ParameterCode.UtraAttack, utraAttack);
                    ed.Parameters = eventData;
                    tempPeer.SendEvent(ed, sendParameters);
                }
            }
        }
    }
}
