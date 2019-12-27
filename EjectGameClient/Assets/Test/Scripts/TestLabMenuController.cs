using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLabMenuController : MonoBehaviour
{
    public GameObject labMenu;

    public void OnBackClick()
    {
        labMenu.SetActive(false);
    }

    public void ShowLabMenu()
    {
        labMenu.SetActive(true);
    }
}