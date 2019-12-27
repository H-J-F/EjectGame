using System;
using System.Collections.Generic;
using Common;
using Common.Tools;
using MyGameServer.Handler;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

namespace MyGameServer
{
    public class MyClientPeer : ClientPeer
    {
        public bool isJoin = false;
        public string username;
        public float x, y, z, rotationY, dotValue, cosValue;

        public MyClientPeer(InitRequest initRequest) : base(initRequest)
        {
        }

        // 处理客户端的请求
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            BaseHandler handler = DictTool.GetValue(MyGameServer.ServerInstance.handlerDict, (OperationCode) operationRequest.OperationCode);

            if (handler != null)
            {
                handler.OnOperationRequest(operationRequest, sendParameters, this);
            }
            else
            {
                BaseHandler defaultHandler = DictTool.GetValue(MyGameServer.ServerInstance.handlerDict, OperationCode.Default);
                defaultHandler.OnOperationRequest(operationRequest, sendParameters, this);
            }

            //switch (operationRequest.OperationCode) // 通过 OpCode 区分不同的请求
            //{
            //case 1:
            //    MyGameServer.log.Info("一个客户端发起了请求");

            //    Dictionary<byte, object> data = operationRequest.Parameters;
            //    object intValue = data[1];
            //    object stringValue = data[2];

            //    MyGameServer.log.Info("传递的数据是" + intValue + " " + stringValue);

            //    OperationResponse opResponse = new OperationResponse(1);

            //    Dictionary<byte, object> data2 = new Dictionary<byte, object>();
            //    data2.Add(1, 123);
            //    data2.Add(2, "hello world!");
            //    opResponse.SetParameters(data2);

            //    SendOperationResponse(opResponse, sendParameters); // 给客户端一个响应

            //    EventData ed = new EventData(1);
            //    ed.Parameters = data2;
            //    SendEvent(ed, sendParameters);
            //    break;

            //default:
            //    break;
            // }
        }

        // 处理客户端断开连接的后续工作
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            MyGameServer.ServerInstance.peerList.Remove(this);
        }
    }
}
