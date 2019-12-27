using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDataController : MonoBehaviour
{
    public Text livesText;
    public Text coinsText;
    public Text diamondsText;

    void Start()
    {
        livesText.text = UserDataManager.instance.userData.livesNum.ToString();
        coinsText.text = UserDataManager.instance.userData.goldNum.ToString();
        diamondsText.text = UserDataManager.instance.userData.diamondNum.ToString();
    }
}