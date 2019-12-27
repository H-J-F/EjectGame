using System.Collections;
using System.Collections.Generic;
using EjectGame.Tools;
using UnityEngine;

public class UltraAttackPlayer : UltraTimingPlayer 
{
    #region public field

    public Transform ultraAttackPoint;

    #endregion


    #region private field

    protected readonly int m_ultraAttackId = Animator.StringToHash("UltraAttack");

    protected bool canUltraAttackRequest = true;

    protected GameObject ultraMissilePrefab;
    protected GameObject ultraMuzzlePrefab;

    #endregion


    #region monobehavior callbacks

    protected override void Start () {
        base.Start();
        ultraMissilePrefab = Resources.Load<GameObject>(EjectTools.characterEffectPathPrefix + "Ghost/Ultra Missile");
        ultraMuzzlePrefab = Resources.Load<GameObject>(EjectTools.characterEffectPathPrefix + "Ghost/Ultra Muzzle");
    }

	#endregion

	#region custom methods

    protected override void GetMouseInput()
    {
        // 根据是否释放大招来判断鼠标左键按下发起攻击动作
        if (m_enableUltra)
        {
            if (Input.GetMouseButton(0))
            {
                m_animator.SetBool(m_ultraAttackId, true);

                if (canUltraAttackRequest)
                {
                    StartCoroutine(SyncUltraAttackCoroutine());
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                m_animator.SetBool(m_attackId, true);

                if (canNormalAttackRequest)
                {
                    StartCoroutine(SyncNormalAttackCoroutine());
                }
            }

            // 鼠标右键释放大招
            if (Input.GetMouseButtonUp(1))
            {
                EnableUltraAttack();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_animator.SetBool(m_attackId, false);
            m_animator.SetBool(m_ultraAttackId, false);
        }
    }

    protected override void GetTouchInput()
    {
        if (m_enableUltra)
        {
            m_animator.SetBool(m_ultraAttackId, RotateJoyStick.PointerDown);
        }
        else
        {
            m_animator.SetBool(m_attackId, RotateJoyStick.PointerDown);
        }
    }

    public override void DisableUltraAttack()
    {
        base.DisableUltraAttack();
        m_animator.SetBool(m_ultraAttackId, false);
    }

    public void UltraAttack()
    {
        GameObject ultraMissile = GameObject.Instantiate(ultraMissilePrefab, ultraAttackPoint.position, transform.rotation);
        
        GameObject ultraMuzzle = GameObject.Instantiate(ultraMuzzlePrefab, ultraAttackPoint.position, transform.rotation);
        missileCtrl.AddLocalMissile(1, ultraMissile.transform, 1, ultraMuzzle.transform, transform.localEulerAngles);
    }

    #endregion


    #region custom coroutines

    protected virtual IEnumerator SyncUltraAttackCoroutine()
    {
        canUltraAttackRequest = false;
        playerCtrl.SyncAttackRequestData(false, true);
        yield return new WaitForSeconds(1f);
        canUltraAttackRequest = true;
    }

    #endregion
}
