using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Tools;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public int generateMissileId = 0;

    public GameObject syncNormalMissilePrefab;
    public GameObject syncUltraMissilePrefab;
    public GameObject syncNormalMuzzlePrefab;
    public GameObject syncUltraMuzzlePrefab;
    public BasePlayer playerInputCtrl;

    [HideInInspector] public bool playerNormalAttack = false;
    [HideInInspector] public bool playerUtraAttack = false;

    private readonly float MOVE_OFFSET = 0.01f;
    private SyncMissileRequest syncMissileRequest;
    private GenerateMissileRequest generateMissileRequest;
    private Dictionary<int, Transform> localMissileDict = new Dictionary<int, Transform>();
    private Dictionary<int, MissileProperty> syncMissileDict = new Dictionary<int, MissileProperty>();
    private List<MissileData> localMissileList = new List<MissileData>();
    //private List<MissileData> syncMissileList = new List<MissileData>();

    // Use this for initialization
    void Start()
    {
        playerInputCtrl.missileCtrl = this;
        syncMissileRequest = GetComponent<SyncMissileRequest>();
        generateMissileRequest = GetComponent<GenerateMissileRequest>();
        //generateMissileRequest.DefaultRequest();
        InvokeRepeating("SyncMissile", 0f, 1f / 30);
    }

    void SyncMissile()
    {
        if (localMissileDict.Count > 0)
        {
            for (int i = 0; i < localMissileDict.Count; i++)
            {
                localMissileList[i].Pos = new Vector3Data() { x = localMissileDict[i].position.x, y = localMissileDict[i].position.y, z = localMissileDict[i].position.z };
            }

            syncMissileRequest.SetData(localMissileList);
            syncMissileRequest.DefaultRequest();
        }
    }

    public void AddLocalMissile(int missileType, Transform missile, int muzzleType, Transform muzzle, Vector3 muzzleRotation)
    {
        MissileData missileData = new MissileData()
        {
            id = generateMissileId,
            missileType = missileType,
            Pos = new Vector3Data() {x = missile.position.x, y = missile.position.y, z = missile.position.z}
        };

        MuzzleData muzzleData = new MuzzleData()
        {
            muzzleType = muzzleType,
            Pos = new Vector3Data() { x = muzzle.position.x, y = muzzle.position.y, z = muzzle.position.z },
            Rotation = new Vector3Data() { x = muzzleRotation.x, y = muzzleRotation.y, z = muzzleRotation.z }
        };

        generateMissileRequest.SetData(missileData, muzzleData);
        generateMissileRequest.DefaultRequest();
        localMissileDict.Add(generateMissileId, missile);
        localMissileList.Add(missileData);
        generateMissileId += 1;
    }

    public void OnGenerateMissileResponse(List<string> usernameList)
    {
        //foreach (var username in usernameList)
        //{
            
        //}

        //OnGenerateMissileEvent(username);
    }

    public void OnGenerateMissileEvent(MissileData missileData, MuzzleData muzzleData)
    {
        // 创建其他客户端的 Missile
        GameObject missile = Instantiate(missileData.missileType == 0 ? syncNormalMissilePrefab : syncUltraMissilePrefab, new Vector3(missileData.Pos.x, missileData.Pos.y, missileData.Pos.z), Quaternion.identity);
        GameObject muzzle = Instantiate(muzzleData.muzzleType == 0 ? syncNormalMuzzlePrefab : syncUltraMuzzlePrefab, new Vector3(muzzleData.Pos.x, muzzleData.Pos.y, muzzleData.Pos.z), Quaternion.Euler(muzzleData.Rotation.x, muzzleData.Rotation.y, muzzleData.Rotation.z));
        syncMissileDict.Add(missileData.id, missile.GetComponent<MissileProperty>());
    }

    public void OnSyncMissileEvent(List<MissileData> missileDataList)
    {
        foreach (var md in missileDataList)
        {
            MissileProperty property = DictTool.GetValue<int, MissileProperty>(syncMissileDict, md.id);

            if (property != null)
            {
                property.id = md.id;
                property.targetPosition = new Vector3() { x = md.Pos.x, y = md.Pos.y, z = md.Pos.z };

                if (!property.isReceivePos)
                {
                    property.isReceivePos = true;
                }
            }
        }
    }
}
