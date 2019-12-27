using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using Common.Tools;
using ExitGames.Client.Photon;

public class SyncPositionEvent : BaseEvent {

    private PlayerController playerCtrl;

    protected override void Start()
    {
        base.Start();
        playerCtrl = GetComponent<PlayerController>();
    }

    public override void OnEvent(EventData eventData)
    {
        string playerDataListString = (string) DictTool.GetValue<byte, object>(eventData.Parameters, (byte) ParameterCode.PlayerDataList);

        using (StringReader reader = new StringReader(playerDataListString))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerData>));
            List<PlayerData> playerDataList = (List<PlayerData>) serializer.Deserialize(reader);
            playerCtrl.OnSyncPositionEvent(playerDataList);
        }
    }
}
