using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraTimingPlayer : BasePlayer 
{
    #region public field

    public float ultraTime;

    #endregion


    #region private field

    protected float m_rawUltraTime = 10f;

    #endregion


    #region monobehavior callbacks

    protected override void Start () {
        base.Start();
        ultraTime = m_rawUltraTime;
    }

	#endregion

	#region custom methods

    protected override void UpdateFinalWork()
    {
        if (m_enableUltra)
        {
            UltraTiming();
        }
    }

    void UltraTiming()
    {
        ultraTime -= Time.deltaTime;

        if (ultraTime <= 0)
        {
            ultraTime = m_rawUltraTime;
            DisableUltraAttack();
        }
    }

    #endregion


    #region custom coroutines



    #endregion
}
