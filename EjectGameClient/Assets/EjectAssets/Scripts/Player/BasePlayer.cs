using System.Collections;
using System.Collections.Generic;
using Common;
using EjectGame.Tools;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasePlayer : MonoBehaviour
{
    #region 公有字段
    
    public float playerSpeed = 4f;
    public Transform attackPoint;

    [HideInInspector] public PlayerController playerCtrl;
    [HideInInspector] public MissileController missileCtrl;

    public float DotValue
    {
        get { return dotValue; }
    }

    public float CosValue
    {
        get { return cosValue; }
    }

    protected JoyStick MoveJoyStick
    {
        get { return GameManager.instance.moveJoyStick; }
    }

    protected JoyStick RotateJoyStick
    {
        get { return GameManager.instance.rotateJoyStick; }
    }

    #endregion


    #region 私有字段

    private const float rotateSpeed = 15f;

    private bool canControl = true;
    
    protected float dotValue;
    protected float cosValue;

    protected bool canNormalAttackRequest = true;
    protected bool m_enableUltra = false;
    protected readonly int m_attackId = Animator.StringToHash("Attack");
    
    protected readonly int m_dotValueId = Animator.StringToHash("DotValue");
    protected readonly int m_cosValueId = Animator.StringToHash("CosValue");

    protected int m_terrainLayerIndex;
    protected Animator m_animator;
    protected CharacterController m_controller;

    protected GameObject normalMissilePrefab;
    protected GameObject normalMuzzlePrefab;

    #endregion


    #region Monobehavior 回调方法

    protected virtual void Start()
    {
        m_terrainLayerIndex = 1 << LayerMask.NameToLayer("Terrain");
        m_controller = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        normalMissilePrefab = Resources.Load<GameObject>(EjectTools.characterEffectPathPrefix + "Ghost/Normal Missile");
        normalMuzzlePrefab = Resources.Load<GameObject>(EjectTools.characterEffectPathPrefix + "Ghost/Normal Muzzle");
    }

    void Update()
    {
        if (canControl)
        {
            GetCharacterMove();

            RotatePlayer();

            GetInput();

            UpdateFinalWork();
        }
    }

    #endregion


    #region 自定义方法

    void GetCharacterMove()
    {
        Vector3 velocity;

        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            velocity = new Vector3(h, 0, v);
        }
        else
        {
            velocity = MoveJoyStick.Direction;
        }

        float inputSpeed = Mathf.Clamp(velocity.magnitude, 0, 1f);
        dotValue = Vector3.Dot(transform.forward, velocity.normalized);
        cosValue = Vector3.Dot(transform.right, velocity.normalized);

        m_animator.SetFloat(m_dotValueId, dotValue);
        m_animator.SetFloat(m_cosValueId, cosValue);

        m_controller.SimpleMove(velocity.normalized * inputSpeed * playerSpeed);
    }

    protected void GetInput()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                GetMouseInput();
            }
        }
        else
        {
            GetTouchInput();
        }
    }

    protected virtual void GetMouseInput()
    {
        // 鼠标左键普通攻击
        if (Input.GetMouseButton(0))
        {
            m_animator.SetBool(m_attackId, true);

            if (canNormalAttackRequest)
            {
                StartCoroutine(SyncNormalAttackCoroutine());
            }
        }

        // 鼠标左键抬起停止攻击
        if (Input.GetMouseButtonUp(0))
        {
            m_animator.SetBool(m_attackId, false);
        }

        // 鼠标右键释放大招
        if (Input.GetMouseButtonUp(1))
        {
            EnableUltraAttack();
        }
    }

    protected virtual void GetTouchInput()
    {
        m_animator.SetBool(m_attackId, RotateJoyStick.PointerDown);
    }

    protected virtual void UpdateFinalWork()
    {
        // 空方法，子类进行重写
    }

    void RotatePlayer()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, m_terrainLayerIndex))
                {
                    Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    transform.LookAt(target);
                }
            }
        }
        else
        {
            if (!RotateJoyStick.IsSmallerThanMinValueDistance())
            {
                float angle = Vector2.Angle(Vector2.up, new Vector2(RotateJoyStick.Direction.x, RotateJoyStick.Direction.z));

                angle = RotateJoyStick.Direction.x >= 0 ? angle : 360f - angle;
                Debug.Log(RotateJoyStick.Direction + "  " + angle);
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, angle, 0));

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
            }
        }
    }

    public void EnableUltraAttack()
    {
        m_enableUltra = true;
    }

    public virtual void DisableUltraAttack()
    {
        m_enableUltra = false;
    }

    public virtual void NormalAttack()
    {
        GameObject missile = GameObject.Instantiate(normalMissilePrefab, attackPoint.position, transform.rotation);
        GameObject muzzle = GameObject.Instantiate(normalMuzzlePrefab, attackPoint.position, transform.rotation);
        missileCtrl.AddLocalMissile(0, missile.transform, 0, muzzle.transform, transform.localEulerAngles);
    }

    public void LostControl()
    {
        canControl = false;
    }

    #endregion


    #region 协程

    protected virtual IEnumerator SyncNormalAttackCoroutine()
    {
        canNormalAttackRequest = false;
        playerCtrl.SyncAttackRequestData(true, false);
        yield return new WaitForSeconds(1f);
        canNormalAttackRequest = true;
    }

    #endregion


    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Enemy Normal Missile")
        {
            Debug.Log("Enemy Normal Missile");
            playerCtrl.playerState.GetDemage(0.15f);
            playerCtrl.SendPlayerState();
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Enemy Ultra Missile")
        {
            Debug.Log("Enemy Ultra Missile");
            playerCtrl.playerState.GetDemage(0.25f);
            playerCtrl.SendPlayerState();
            other.gameObject.SetActive(false);
        }
    }
}