using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using Common.Tools;
using Photon.SocketServer;

namespace MyGameServer.Handler
{
    class SyncPlayerHandler : BaseHandler
    {
        public SyncPlayerHandler()
        {
            opCode = OperationCode.SyncPlayer;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            List<string> usernameList = new List<string>();
            foreach (var tempPeer in MyGameServer.ServerInstance.peerList)
            {
                if (!string.IsNullOrEmpty(tempPeer.username)  && tempPeer != peer && tempPeer.isJoin)
                {
                    usernameList.Add(tempPeer.username);
                }
            }

            peer.isJoin = (bool)DictTool.GetValue(operationRequest.Parameters, (byte)ParameterCode.PlayerJoinIn);

            //usernameList.Add("123"); // 测试用数据

            StringWriter sw = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            serializer.Serialize(sw, usernameList);
            sw.Close();
            string usernameListToString = sw.ToString();
            //MyGameServer.log.Info(usernameListToString);

            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add((byte) ParameterCode.UsernameList, usernameListToString);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            response.Parameters = data;
            peer.SendOperationResponse(response, sendParameters);

            // 告诉其他客户端有新的客户端（当前客户端）加入
            foreach (var tempPeer in MyGameServer.ServerInstance.peerList)
            {
                if (!string.IsNullOrEmpty(tempPeer.username) && tempPeer != peer && tempPeer.isJoin)
                {
                    EventData ed = new EventData((byte) EventCode.NewPlayer);
                    Dictionary<byte, object> eventData = new Dictionary<byte, object>();
                    eventData.Add((byte) ParameterCode.Username, peer.username);
                    ed.Parameters = eventData;
                    tempPeer.SendEvent(ed, sendParameters);
                }
            }
        }
    }
}
