using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using Common.Tools;
using ExitGames.Client.Photon;
using UnityEngine;

public class LoginRequest : Request
{
    [NonSerialized]
    public string username;
    [NonSerialized]
    public string passsword;

    private LoginController loginCtrl;

    protected override void Start()
    {
        base.Start();
        loginCtrl = GetComponent<LoginController>();
    }

    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte) ParameterCode.Username, username);
        data.Add((byte)ParameterCode.Password, passsword);
        PhotonEngine.Peer.OpCustom((byte) opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        ReturnCode returnCode = (ReturnCode) operationResponse.ReturnCode;

        if (returnCode == ReturnCode.Success)
        {
            PhotonEngine.username = username;

            string playerDataString = (string)DictTool.GetValue<byte, object>(operationResponse.Parameters, (byte)ParameterCode.UserData);

            using (StringReader reader = new StringReader(playerDataString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UserData));
                UserData userData = (UserData)serializer.Deserialize(reader);
                //Debug.Log(userData.username + "  " + userData.diamondNum);
                UserDataManager.instance.userData = userData;
            }
        }

        loginCtrl.OnLoginResponse(returnCode);
    }
}
