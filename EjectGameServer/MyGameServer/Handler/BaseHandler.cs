using System;
using System.Collections.Generic;
using Common;
using Photon.SocketServer;

namespace MyGameServer.Handler
{
    public abstract class BaseHandler
    {
        public OperationCode opCode;

        public abstract void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer);
    }
}
