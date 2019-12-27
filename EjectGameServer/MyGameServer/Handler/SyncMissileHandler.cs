using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using Common.Tools;
using Photon.SocketServer;

namespace MyGameServer.Handler
{
    class SyncMissileHandler : BaseHandler
    {
        public SyncMissileHandler()
        {
            opCode = OperationCode.SyncMissile;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            string missileListToString = (string) DictTool.GetValue(operationRequest.Parameters, (byte)ParameterCode.MissileDataList);

            //using (StringReader reader = new StringReader(missileListToString))
            //{
            //    XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            //    List<string> missileList = (List<string>)serializer.Deserialize(reader);
            //}

            foreach (var tempPeer in MyGameServer.ServerInstance.peerList)
            {
                if (!string.IsNullOrEmpty(tempPeer.username) && tempPeer != peer && tempPeer.isJoin)
                {
                    EventData ed = new EventData((byte)EventCode.SyncMissile);
                    Dictionary<byte, object> eventData = new Dictionary<byte, object>();
                    eventData.Add((byte)ParameterCode.MissileDataList, missileListToString);
                    ed.Parameters = eventData;
                    tempPeer.SendEvent(ed, sendParameters);
                }
            }
        }
    }
}
