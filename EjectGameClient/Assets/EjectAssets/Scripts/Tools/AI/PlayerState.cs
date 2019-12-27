using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public float hp = 1;
    public float enemyHp = 1;
    public float lerpSpeed = 4f;
    public Image hpImg;
    public Image enemyHpImg;
    public Text selfUsername;
    public Text enemyUsername;
    public GameOverController gameOverCtrl;

    [HideInInspector] public BasePlayer playerInputCtrl;
    [HideInInspector] public PlayerStateRequest playerStateRequest;

    void Start()
    {
        playerStateRequest = GetComponent<PlayerStateRequest>();
        selfUsername.text = UserDataManager.instance.userData.username;
    }

    void Update()
    {
        hpImg.fillAmount = Mathf.Lerp(hpImg.fillAmount, hp, lerpSpeed * Time.deltaTime);
        enemyHpImg.fillAmount = Mathf.Lerp(enemyHpImg.fillAmount, enemyHp, lerpSpeed * Time.deltaTime);
    }

    public void GetDemage(float demage)
    {
        hp -= demage;

        if (hp <= 0)
        {
            Die();
            gameOverCtrl.ShowUI(false);
        }
    }

    public void Die()
    {
        Debug.Log("you die");
        playerInputCtrl.LostControl();
    }
}