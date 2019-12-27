using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using Common.Tools;
using ExitGames.Client.Photon;

public class GenerateMissileEvent : BaseEvent 
{
    private MissileController missileCtrl;

    protected override void Start()
    {
        base.Start();
        missileCtrl = GetComponent<MissileController>();
    }

    public override void OnEvent(EventData eventData)
    {
        string missileDataToString = (string) DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.MissileData);
        string muzzleDataToString = (string) DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.MuzzleData);

        MissileData missileData;
        MuzzleData muzzleData;

        using (StringReader reader = new StringReader(missileDataToString))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MissileData));
            missileData = (MissileData)serializer.Deserialize(reader);
            //reader.Close();
        }

        using (StringReader reader = new StringReader(muzzleDataToString))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MuzzleData));
            muzzleData = (MuzzleData)serializer.Deserialize(reader);
            //reader.Close();
        }

        missileCtrl.OnGenerateMissileEvent(missileData, muzzleData);
    }
}
