using System.Collections;
using System.Collections.Generic;
using EjectGame.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : BaseManager<GameStateManager>
{
	#region public field

    [HideInInspector] public GameScenes currentScene = GameScenes.Start;
    [HideInInspector] public GameScenes nextScene;

	#endregion


	#region private field



	#endregion


	#region monobehavior callbacks

	protected override void Start () {
		
	}

	#endregion

	#region custom methods

    public void LoadNewScene(GameScenes nextScene)
    {
        this.nextScene = nextScene;
        SceneManager.LoadScene(EjectTools.loadingSceneName);
    }

    public void LoadByButton()
    {
        LoadNewScene(GameScenes.MainMenu);
    }

	#endregion


	#region custom coroutines



	#endregion
}
