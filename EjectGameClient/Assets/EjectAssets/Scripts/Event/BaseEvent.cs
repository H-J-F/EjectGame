using Common;
using ExitGames.Client.Photon;
using UnityEngine;

public abstract class BaseEvent : MonoBehaviour {

    public EventCode eventCode;
    public abstract void OnEvent(EventData eventData);

    protected virtual void Start()
    {
        PhotonEngine._instance.AddEvent(this);
    }

    void OnDestroy()
    {
        PhotonEngine._instance.RemoveEvent(this);
    }
}
