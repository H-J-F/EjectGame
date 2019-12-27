using Common;
using ExitGames.Client.Photon;
using UnityEngine;

public abstract class Request : MonoBehaviour
{
    public OperationCode opCode;
    public abstract void DefaultRequest();
    public abstract void OnOperationResponse(OperationResponse operationResponse);

    protected virtual void Start()
    {
        PhotonEngine._instance.AddRequest(this);
    }

    void OnDestroy()
    {
        PhotonEngine._instance.RemoveRequest(this);
    }
}
