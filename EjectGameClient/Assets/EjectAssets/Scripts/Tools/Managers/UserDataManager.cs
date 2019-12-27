using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class UserDataManager : BaseManager<UserDataManager>
{
    [HideInInspector]
    public UserData userData;

    protected override void Awake()
    {
        base.Awake();
    }


}