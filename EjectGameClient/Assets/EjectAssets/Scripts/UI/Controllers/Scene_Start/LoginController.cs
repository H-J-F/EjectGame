using System.Collections;
using System.Collections.Generic;
using Common;
using EjectGame.Tools;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour 
{
    #region public field

    public InputField usernameIF;
    public InputField passwordIF;
    public SceneBlackMask blackMask;
    [HideInInspector] public SubmitButtonType submitButtonType = SubmitButtonType.Login;

	#endregion


	#region private field

    private LoginRequest loginRequest;

	#endregion


	#region monobehavior callbacks

	void Start () {
	    loginRequest = GetComponent<LoginRequest>();
    }

	#endregion

	#region custom methods

    public void OnSubmitButtonClick()
    {
        switch (submitButtonType)
        {
            case SubmitButtonType.Login:
                Debug.Log("用户登陆中！");
                loginRequest.username = usernameIF.text;
                loginRequest.passsword = passwordIF.text;
                loginRequest.DefaultRequest();

                break;
            case SubmitButtonType.Register:

                break;
        }
    }

    public void OnLoginResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            // 跳转场景
            Debug.Log("用户登陆成功！");
            blackMask.SetLoadSceneAction(LoadMainMenuScene);
            blackMask.BlackMaskFadeIn();
        }
        else if (returnCode == ReturnCode.Failed)
        {
            //hintMessage.text = "用户名或密码错误！";
            Debug.Log("用户名或密码错误！");
        }
    }

    #endregion


    #region custom coroutines

    private void LoadMainMenuScene()
    {
        GameStateManager.instance.LoadNewScene(GameScenes.MainMenu);
    }

	#endregion
}
