using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using ExitGames.Client.Photon;

public class GenerateMissileRequest : Request
{
    public MissileData missileData;
    public MuzzleData muzzleData;

    public override void DefaultRequest()
    {
        StringWriter sw = new StringWriter();
        XmlSerializer serializer = new XmlSerializer(typeof(MissileData));

        serializer.Serialize(sw, missileData);
        string missileDataToString = sw.ToString();

        StringWriter sw2 = new StringWriter();
        XmlSerializer serializer2 = new XmlSerializer(typeof(MuzzleData));
        serializer2 = new XmlSerializer(typeof(MuzzleData));
        serializer2.Serialize(sw2, muzzleData);
        string muzzleDataToString = sw2.ToString();

        sw.Close();
        sw2.Close();

        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.MissileData, missileDataToString);
        data.Add((byte)ParameterCode.MuzzleData, muzzleDataToString);
        PhotonEngine.Peer.OpCustom((byte)opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        
    }

    public void SetData(MissileData missileData, MuzzleData muzzleData)
    {
        this.missileData = missileData;
        this.muzzleData = muzzleData;
    }
}
