using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using Common.Tools;
using MyGameServer.Managers;
using MyGameServer.Model;
using Photon.SocketServer;

namespace MyGameServer.Handler
{
    class LoginHandler : BaseHandler
    {
        public LoginHandler()
        {
            opCode = OperationCode.Login;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            string username = DictTool.GetValue(operationRequest.Parameters, (byte) ParameterCode.Username) as string;
            string password = DictTool.GetValue(operationRequest.Parameters, (byte) ParameterCode.Password) as string;

            UserManager manager = new UserManager();
            bool isSuccess = manager.VerifyUser(username, password);

            OperationResponse response = new OperationResponse(operationRequest.OperationCode);

            if (isSuccess)
            {
                response.ReturnCode = (short) ReturnCode.Success;
                peer.username = username;
                User user = manager.GetByUsername(username);
                UserData userData = new UserData() {id = user.Id, username = user.Username, diamondNum = user.Diamond_num, goldNum = user.Gold_num, livesNum = user.Lives_num};

                StringWriter sw = new StringWriter();
                XmlSerializer serializer = new XmlSerializer(typeof(UserData));
                serializer.Serialize(sw, userData);
                sw.Close();
                string userString = sw.ToString();
                //MyGameServer.log.Info(userString);

                Dictionary<byte, object> data = new Dictionary<byte, object>();
                data.Add((byte)ParameterCode.UserData, userString);
                response.Parameters = data;
            }
            else
            {
                response.ReturnCode = (short) ReturnCode.Failed;
            }
            
            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
