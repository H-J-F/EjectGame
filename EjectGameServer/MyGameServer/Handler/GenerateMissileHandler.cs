using System;
using System.Collections.Generic;
using Common;
using Common.Tools;
using Photon.SocketServer;

namespace MyGameServer.Handler
{
    class GenerateMissileHandler : BaseHandler
    {
        public GenerateMissileHandler()
        {
            opCode = OperationCode.GenerateMissile;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            string missileDataToString = (string)DictTool.GetValue(operationRequest.Parameters, (byte)ParameterCode.MissileData);
            string muzzleDataToString = (string)DictTool.GetValue(operationRequest.Parameters, (byte)ParameterCode.MuzzleData);

            foreach (var tempPeer in MyGameServer.ServerInstance.peerList)
            {
                if (!string.IsNullOrEmpty(tempPeer.username) && tempPeer != peer && tempPeer.isJoin)
                {
                    EventData ed = new EventData((byte)EventCode.GenerateMissile);
                    Dictionary<byte, object> eventData = new Dictionary<byte, object>();
                    eventData.Add((byte)ParameterCode.MissileData, missileDataToString);
                    eventData.Add((byte)ParameterCode.MuzzleData, muzzleDataToString);
                    ed.Parameters = eventData;
                    tempPeer.SendEvent(ed, sendParameters);
                }
            }
        }
    }
}