using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayerDataController : MonoBehaviour
{
    public int coins = 1000;
    public int diamounds = 100;
    public int lives = 10;

    public Text coinsText;
    public Text diamoundsText;
    public Text livesText;

    void Start()
    {
        coins = UserDataManager.instance.userData.goldNum;
        diamounds = UserDataManager.instance.userData.diamondNum;
        lives = UserDataManager.instance.userData.livesNum;
    }

    public void BuyLives_1()
    {
        if (diamounds >= 10)
        {
            lives += 1;
            diamounds -= 10;
            Invoke("SetText", 0.1f);
        }
    }

    public void BuyLives_2()
    {
        if (diamounds >= 50)
        {
            lives += 6;
            diamounds -= 50;
            Invoke("SetText", 0.1f);
        }
    }

    public void BuyLives_3()
    {
        if (diamounds >= 100)
        {
            lives += 12;
            diamounds -= 100;
            Invoke("SetText", 0.1f);
        }
    }

    public void BuyLives_4()
    {
        if (diamounds >= 250)
        {
            lives += 30;
            diamounds -= 250;
            Invoke("SetText", 0.1f);
        }
    }

    public void BuyCoins_1()
    {
        if (diamounds >= 10)
        {
            coins += 1000;
            diamounds -= 10;
            Invoke("SetText", 0.15f);
        }
    }

    public void BuyCoins_2()
    {
        if (diamounds >= 30)
        {
            coins += 3150;
            diamounds -= 30;
            Invoke("SetText", 0.15f);
        }
    }

    public void BuyCoins_3()
    {
        if (diamounds >= 60)
        {
            coins += 6600;
            diamounds -= 60;
            Invoke("SetText", 0.15f);
        }
    }

    public void BuyCoins_4()
    {
        if (diamounds >= 100)
        {
            coins += 11500;
            diamounds -= 100;
            Invoke("SetText", 0.15f);
        }
    }

    public void BuyCoins_5()
    {
        if (diamounds >= 250)
        {
            coins += 30000;
            diamounds -= 250;
            Invoke("SetText", 0.15f);
        }
    }

    private void SetText()
    {
        coinsText.text = coins.ToString();
        diamoundsText.text = diamounds.ToString();
        livesText.text = lives.ToString();
    }
}