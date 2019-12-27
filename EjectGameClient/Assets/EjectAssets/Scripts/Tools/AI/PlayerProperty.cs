using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    private readonly float DISTANCE_OFFET = 0.001f;
    private bool isInitPos = false;
    private Animator anim;
    protected readonly int m_attackId = Animator.StringToHash("Attack");
    protected readonly int m_ultraAttackId = Animator.StringToHash("UltraAttack");
    private readonly int m_dotValueId = Animator.StringToHash("DotValue");
    private readonly int m_cosValueId = Animator.StringToHash("CosValue");

    public string username;
    public bool isReceivePos = false;
    public float animDotValue;
    public float animCosValue;
    public float targetRotationY;
    public Vector3 targetPosition = Vector3.zero;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
	void Update ()
	{
	    if (isReceivePos && !isInitPos)
	    {
	        transform.position = targetPosition;
	        isInitPos = true;
	    }

	    if (Vector3.Distance(targetPosition, transform.position) > DISTANCE_OFFET)
	    {
            transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);
	        //transform.Translate((targetPosition - transform.position) * 12 * Time.deltaTime);
	    }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, targetRotationY, 0)), 10f * Time.deltaTime);

	    anim.SetFloat(m_dotValueId, animDotValue);
	    anim.SetFloat(m_cosValueId, animCosValue);
    }

    public void SetAttackState(bool normalAttack = false, bool utraAttack = false)
    {
        if (normalAttack)
        {
            anim.Play("Ghost Attack");
        }

        if (utraAttack)
        {
            anim.Play("Ghost Ultra Attack");
        }
    }
}
