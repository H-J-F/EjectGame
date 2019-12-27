using Common;
using Common.Tools;
using Photon.SocketServer;

namespace MyGameServer.Handler
{
    class SyncPositionHandler : BaseHandler
    {
        public SyncPositionHandler()
        {
            opCode = OperationCode.SyncPosition;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyClientPeer peer)
        {
            float x = (float) DictTool.GetValue(operationRequest.Parameters, (byte) ParameterCode.X);
            float y = (float) DictTool.GetValue(operationRequest.Parameters, (byte) ParameterCode.Y);
            float z = (float) DictTool.GetValue(operationRequest.Parameters, (byte) ParameterCode.Z);
            float rotationY = (float) DictTool.GetValue(operationRequest.Parameters, (byte) ParameterCode.RotationY);
            float dotValue = (float) DictTool.GetValue(operationRequest.Parameters, (byte) ParameterCode.DotValue);
            float cosValue = (float) DictTool.GetValue(operationRequest.Parameters, (byte) ParameterCode.CosValue);

            peer.x = x;
            peer.y = y;
            peer.z = z;
            peer.rotationY = rotationY;
            peer.dotValue = dotValue;
            peer.cosValue = cosValue;
        }
    }
}
