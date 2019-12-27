using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour 
{
	#region public field

    public float emissionVelocity = 13f;

    #endregion


    #region private field
    
    private Rigidbody rgbody;

	#endregion


	#region monobehavior callbacks

	void Start ()
	{
	    rgbody = GetComponent<Rigidbody>();
	    rgbody.velocity = transform.forward.normalized * emissionVelocity;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (rgbody.velocity.magnitude < emissionVelocity)
        {
            rgbody.velocity = rgbody.velocity.normalized * emissionVelocity;
        }
    }

    #endregion

    #region custom methods



    #endregion


    #region custom coroutines



    #endregion
}
