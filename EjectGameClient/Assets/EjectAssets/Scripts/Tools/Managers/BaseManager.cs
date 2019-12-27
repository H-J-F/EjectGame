using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager<T> : MonoBehaviour where T : MonoBehaviour
{
    #region public field

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }

            return _instance;
        }
    }

    #endregion


    #region private field

    private static T _instance;

    #endregion


    #region monobehavior callbacks

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {

    }
    #endregion

    #region custom methods



    #endregion


    #region custom coroutines



    #endregion
}
