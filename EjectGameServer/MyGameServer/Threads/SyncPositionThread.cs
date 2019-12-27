using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using Common;
using Photon.SocketServer;

namespace MyGameServer.Threads
{
    public class SyncPositionThread
    {
        private Thread t;

        public void Run()
        {
            t = new Thread(UpdatePosition);
            t.IsBackground = true;
            t.Start();
        }

        public void Stop()
        {
            t.Abort();
        }

        private void UpdatePosition()
        {
            Thread.Sleep(5000);

            while (true)
            {
                Thread.Sleep(100);

                // 进行同步
                SendPosition();
            }
        }

        private void SendPosition()
        {
            List<PlayerData> playerDataList = new List<PlayerData>();

            foreach (var peer in MyGameServer.ServerInstance.peerList)
            {
                if (string.IsNullOrEmpty(peer.username) == false && peer.isJoin)
                {
                    PlayerData playerData = new PlayerData();
                    playerData.Username = peer.username;
                    playerData.Pos = new Vector3Data(){x = peer.x, y = peer.y, z = peer.z};
                    playerData.RotationY = peer.rotationY;
                    playerData.CosValue = peer.cosValue;
                    playerData.DotValue = peer.dotValue;
                    playerDataList.Add(playerData);
                }
            }

            StringWriter sw = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerData>));
            serializer.Serialize(sw, playerDataList);
            sw.Close();
            string playerDataListString = sw.ToString();
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add((byte) ParameterCode.PlayerDataList, playerDataListString);

            foreach (var peer in MyGameServer.ServerInstance.peerList)
            {
                if (string.IsNullOrEmpty(peer.username) == false && peer.isJoin)
                {
                    EventData ed = new EventData((byte) EventCode.SyncPosition);
                    ed.Parameters = data;
                    peer.SendEvent(ed, new SendParameters());
                }
            }
        }
    }
}
