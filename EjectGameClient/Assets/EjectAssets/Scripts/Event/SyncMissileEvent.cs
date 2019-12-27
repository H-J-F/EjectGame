using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using Common.Tools;
using ExitGames.Client.Photon;
using UnityEngine;

public class SyncMissileEvent : BaseEvent 
{
    private MissileController missileCtrl;

    protected override void Start()
    {
        base.Start();
        missileCtrl = GetComponent<MissileController>();
    }

    public override void OnEvent(EventData eventData)
    {
        string missileListToString = (string) DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.MissileDataList);
        using (StringReader reader = new StringReader(missileListToString))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<MissileData>));
            List<MissileData> missileList = (List<MissileData>)serializer.Deserialize(reader);
            missileCtrl.OnSyncMissileEvent(missileList);
        }
    }
}