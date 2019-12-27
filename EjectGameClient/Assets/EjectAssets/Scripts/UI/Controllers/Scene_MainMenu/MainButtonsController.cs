using System.Collections;
using System.Collections.Generic;
using EjectGame.Tools;
using UnityEngine;

public class MainButtonsController : MonoBehaviour 
{
	#region public field



	#endregion


	#region private field



	#endregion


	#region monobehavior callbacks

	void Start () {
		
	}

	#endregion

	#region custom methods

    public void OnStartGameBtnClick()
    {
        GameStateManager.instance.LoadNewScene(GameScenes.Level_LavaHell);
    }

	#endregion


	#region custom coroutines



	#endregion
}