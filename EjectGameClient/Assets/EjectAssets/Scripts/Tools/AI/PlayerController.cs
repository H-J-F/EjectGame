using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Tools;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    public GameObject syncPlayerPrefab;
    public GameObject player;
    public BasePlayer playerInputCtrl;
    public PlayerState playerState;

    [HideInInspector] public bool playerNormalAttack = false;
    [HideInInspector] public bool playerUtraAttack = false;

    private readonly float MOVE_OFFSET = 0.01f;
    private Vector3 lastPosition = Vector3.zero;
    private SyncPlayerPropertiesRequest syncPosRequest;
    private SyncPlayerRequest syncPlayerRequest;
    private SyncAttackRequest syncAttackRequest;
    private Dictionary<string, GameObject> playerDict = new Dictionary<string, GameObject>();
    private Dictionary<string, PlayerProperty> playerPropertyDict = new Dictionary<string, PlayerProperty>();

    // Use this for initialization
    void Start()
    {
        playerState.playerInputCtrl = playerInputCtrl;
        playerInputCtrl.playerCtrl = this;
        syncPosRequest = GetComponent<SyncPlayerPropertiesRequest>();
        syncPlayerRequest = GetComponent<SyncPlayerRequest>();
        syncAttackRequest = GetComponent<SyncAttackRequest>();
        syncPlayerRequest.DefaultRequest();
        InvokeRepeating("SyncPosition", 0f, 1f / 30);
    }

    void SyncPosition()
    {
        if (Vector3.Distance(player.transform.position, lastPosition) > MOVE_OFFSET)
        {
            lastPosition = transform.position;
            syncPosRequest.SetData(player.transform.position, player.transform.localEulerAngles.y, playerInputCtrl.CosValue, playerInputCtrl.DotValue);
            syncPosRequest.DefaultRequest();
        }
    }

    public void SyncAttackRequestData(bool normalAttack = false, bool utraAttack = false)
    {
        syncAttackRequest.SetData(normalAttack, utraAttack);
        syncAttackRequest.DefaultRequest();
    }

    public void SendPlayerState()
    {
        playerState.playerStateRequest.SetData(playerState.hp);
        playerState.playerStateRequest.DefaultRequest();
    }

    public void OnSyncPlayerResponse(List<string> usernameList)
    {
        foreach (var username in usernameList)
        {
            OnNewPlayerEvent(username);
        }
    }

    public void OnNewPlayerEvent(string username)
    {
        // 创建其他客户端的 player 角色
        playerState.enemyUsername.text = username;
        GameObject other = Instantiate(syncPlayerPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        playerDict.Add(username, other);
        playerPropertyDict.Add(username, other.GetComponent<PlayerProperty>());
    }

    public void OnSyncPositionEvent(List<PlayerData> playerDataList)
    {
        foreach (var pd in playerDataList)
        {
            PlayerProperty property = DictTool.GetValue<string, PlayerProperty>(playerPropertyDict, pd.Username);

            if (property != null)
            {
                property.username = pd.Username;
                property.targetPosition = new Vector3() { x = pd.Pos.x, y = pd.Pos.y, z = pd.Pos.z };
                property.targetRotationY = pd.RotationY;
                property.animDotValue = pd.DotValue;
                property.animCosValue = pd.CosValue;

                if (!property.isReceivePos)
                {
                    property.isReceivePos = true;
                }
            }
        }
    }

    public void OnSyncAttackEvent(string username, bool normalAttack = false, bool utraAttack = false)
    {
        PlayerProperty property = DictTool.GetValue<string, PlayerProperty>(playerPropertyDict, username);
        property.SetAttackState(normalAttack, utraAttack);
    }

    public void OnPlayerStateEvent(float playerHp)
    {
        Debug.Log("OnPlayerStateEvent  " + playerHp);
        playerState.enemyHp = playerHp;

        if (playerHp <= 0)
        {
            playerInputCtrl.LostControl();
            playerState.gameOverCtrl.ShowUI(true);
        }
    }
}