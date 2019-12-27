using System.Collections;
using System.Collections.Generic;
using EjectGame.General;

public class GeneralSettingsManager : BaseManager<GeneralSettingsManager>
{
	#region public field

    public GeneralSettings settings;

    #endregion


    #region private field

    private static bool m_isDontDestroy = false;

    #endregion


    #region monobehavior callbacks

    protected override void Awake()
    {
        base.Awake();
        settings.defaultCursor.Enable();

        if (m_isDontDestroy == false)
        {
            m_isDontDestroy = true;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

    #region custom methods



    #endregion


    #region custom coroutines



    #endregion
}
