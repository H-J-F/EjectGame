using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueChangedText : MonoBehaviour
{
    private Text numText;

	void Start ()
	{
	    numText = GetComponent<Text>();
	}

    public void OnValuedChangedText(float number)
    {
        numText.text = number.ToString();
    }
}
