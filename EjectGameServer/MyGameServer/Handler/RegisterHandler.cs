using System;
using System.Collections.Generic;
using Common;
using Common.Tools;
using MyGameServer.Managers;
using MyGameServer.Model;
using Photon.SocketServer;

namespace MyGameServer.Handler
{
    class RegisterHandler : BaseHandler
    {
        public RegisterHandler()
        {
            opCode = OperationCode.Register;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            string username = DictTool.GetValue(operationRequest.Parameters, (byte) ParameterCode.Username) as string;
            string password = DictTool.GetValue(operationRequest.Parameters, (byte) ParameterCode.Password) as string;

            UserManager manager = new UserManager();
            User user = manager.GetByUsername(username);

            OperationResponse response = new OperationResponse(operationRequest.OperationCode);

            if (user == null)
            {
                user = new User(){Username = username, Password = password};
                manager.Add(user);
                
                response.ReturnCode = (short) ReturnCode.Success;
            }
            else
            {
                response.ReturnCode = (short)ReturnCode.Failed;
            }

            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
