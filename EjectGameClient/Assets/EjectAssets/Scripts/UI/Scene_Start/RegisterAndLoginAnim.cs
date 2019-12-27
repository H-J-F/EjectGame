using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EjectGame.Tools;
using UnityEngine;
using UnityEngine.UI;

public class RegisterAndLoginAnim : MonoBehaviour 
{
	#region public field

    public Transform usernameTrans;
    public Transform passwordTrans;
    public Image leftArrow;
    public Image rightArrow;
    public CanvasGroup repeatPasswordCanvasGroup;
    public LanguageSwitchItem translateBtnText;
    public LanguageSwitchItem submitBtnText;
    public LoginController loginController;

	#endregion


	#region private field

    private bool isLogin = true;
    private bool canClick = true;

	#endregion


	#region monobehavior callbacks

	void Start()
	{
	    
	}

	#endregion

	#region custom methods

    public void OnTranslateBtnClick()
    {
        if (!canClick) return;

        if (isLogin)
        {
            TranslateToRegister();
        }
        else
        {
            TranslateToLogin();
        }

        isLogin = !isLogin;
    }

    private void TranslateToRegister()
    {
        translateBtnText.ChangeTextWithId("Login");
        submitBtnText.ChangeTextWithId("Register");
        loginController.submitButtonType = SubmitButtonType.Register;
        translateBtnText.SetupText();
        submitBtnText.SetupText();
        canClick = false;

        usernameTrans.DOLocalMoveY(50f, 0.3f).SetRelative(true);
        passwordTrans.DOLocalMoveY(65f, 0.3f).SetRelative(true);
        transform.DOLocalMoveY(-35f, 0.3f).SetRelative(true);

        leftArrow.transform.DOLocalMoveX(100f, 0.3f).SetRelative(true);
        rightArrow.transform.DOLocalMoveX(100f, 0.3f).SetRelative(true);
        leftArrow.DOFade(1f, 0.3f);
        rightArrow.DOFade(0f, 0.3f);

        repeatPasswordCanvasGroup.gameObject.SetActive(true);
        repeatPasswordCanvasGroup.DOFade(1f, 0.3f).OnComplete(() =>
        {
            canClick = true;
        });
    }

    private void TranslateToLogin()
    {
        translateBtnText.ChangeTextWithId("Register");
        submitBtnText.ChangeTextWithId("Login");
        loginController.submitButtonType = SubmitButtonType.Login;
        canClick = false;

        usernameTrans.DOLocalMoveY(-50f, 0.3f).SetRelative(true);
        passwordTrans.DOLocalMoveY(-65f, 0.3f).SetRelative(true);
        transform.DOLocalMoveY(35f, 0.3f).SetRelative(true);

        leftArrow.transform.DOLocalMoveX(-100f, 0.3f).SetRelative(true);
        rightArrow.transform.DOLocalMoveX(-100f, 0.3f).SetRelative(true);
        leftArrow.DOFade(0f, 0.3f);
        rightArrow.DOFade(1f, 0.3f);

        repeatPasswordCanvasGroup.DOFade(0f, 0.3f).OnComplete(() =>
        {
            repeatPasswordCanvasGroup.gameObject.SetActive(false);
            canClick = true;
        });
    }

	#endregion


	#region custom coroutines
    


	#endregion
}
