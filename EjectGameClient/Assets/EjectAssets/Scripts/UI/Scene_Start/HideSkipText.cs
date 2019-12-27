using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HideSkipText : MonoBehaviour 
{
	#region public field

    public Text skipText;

	#endregion


	#region private field



	#endregion


	#region monobehavior callbacks

	void Start () {
		
	}
	
	void OnEnable ()
	{
	    if (skipText.gameObject.activeSelf)
	    {
	        skipText.DOFade(0f, 0.5f).OnComplete(() =>
	        {
	            skipText.gameObject.SetActive(false);
	        });
        }
	}

	#endregion

	#region custom methods



	#endregion


	#region custom coroutines



	#endregion
}
