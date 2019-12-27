using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Tools;
using ExitGames.Client.Photon;
using UnityEngine;

public class PhotonEngine : MonoBehaviour, IPhotonPeerListener
{
    public static PhotonEngine _instance;
    public static string username;

    private static PhotonPeer peer;

    private Dictionary<OperationCode, Request> requestDict = new Dictionary<OperationCode, Request>();
    private Dictionary<EventCode, BaseEvent> eventDict = new Dictionary<EventCode, BaseEvent>();

    public static PhotonPeer Peer
    {
        get { return peer; }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

	// Use this for initialization
	void Start ()
	{
        // 通过 Listener 接收服务器端的响应信息
		peer = new PhotonPeer(this, ConnectionProtocol.Udp);
	    peer.Connect("127.0.0.1:5055", "MyGame1");
	}
	
	// Update is called once per frame
	void Update ()
	{
	    peer.Service();
    }

    void OnDestroy()
    {
        Debug.Log("PhotonEngine destroyed");
        if (peer != null && peer.PeerState == PeerStateValue.Connected)
        {
            peer.Disconnect();
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        OperationCode opCode = (OperationCode) operationResponse.OperationCode;
        Request request = null;

        if (requestDict.TryGetValue(opCode, out request))
        {
            request.OnOperationResponse(operationResponse);
        }
        else
        {
            Debug.Log("没找到操作码为" + opCode + "的 Request 类");
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log(statusCode);
    }

    public void OnEvent(EventData eventData)
    {
        BaseEvent resultValue;
        EventCode code = (EventCode) eventData.Code;
        BaseEvent e = DictTool.GetValue<EventCode, BaseEvent>(eventDict, code);
        if (e != null)
        {
            e.OnEvent(eventData);
        }
        //else
        //{
        //    Debug.Log(code);
        //}

        //switch (eventData.Code)
        //{
        //    case 1:
        //        Debug.Log("收到了服务器端的事件");

        //        Dictionary<byte, object> data = eventData.Parameters;
        //        object intValue = data[1];
        //        object stringValue = data[2];

        //        Debug.Log("收到的数据为" + intValue + " " + stringValue);
        //        break;
        //    default:
        //        break;
        //}
    }

    public void AddRequest(Request request)
    {
        requestDict.Add(request.opCode, request);
    }

    public void RemoveRequest(Request request)
    {
        requestDict.Remove(request.opCode);
    }

    public void AddEvent(BaseEvent e)
    {
        eventDict.Add(e.eventCode, e);
    }

    public void RemoveEvent(BaseEvent e)
    {
        eventDict.Remove(e.eventCode);
    }
}
