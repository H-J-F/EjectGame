using Common;
using Common.Tools;
using ExitGames.Client.Photon;

public class NewPlayerEvent : BaseEvent
{
    private PlayerController playerCtrl;

    protected override void Start()
    {
        base.Start();
        playerCtrl = GetComponent<PlayerController>();
    }
    
    public override void OnEvent(EventData eventData)
    {
        string username = (string) DictTool.GetValue<byte, object>(eventData.Parameters, (byte) ParameterCode.Username);
        playerCtrl.OnNewPlayerEvent(username);
    }
}
