using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Tools;
using ExitGames.Client.Photon;
using UnityEngine;

public class PlayerStateEvent : BaseEvent 
{
    private PlayerController playerCtrl;

    protected override void Start()
    {
        base.Start();
        playerCtrl = GetComponent<PlayerController>();
    }

    public override void OnEvent(EventData eventData)
    {
        float playerHp = (float)DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.PlayerHp);
        Debug.Log("PlayerStateEvent  " + playerHp);
        playerCtrl.OnPlayerStateEvent(playerHp);
    }
}