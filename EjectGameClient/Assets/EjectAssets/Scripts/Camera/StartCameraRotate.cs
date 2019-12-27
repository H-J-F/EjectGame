using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCameraRotate : MonoBehaviour 
{
    #region public field

    [Header("Windows Settings")]
    public float rotateSpeed = 10; // 旋转速度
    public float rotateDegree = 2f; // 旋转角度

    [Header("Mobile Settings")]
    public float sensitivity = 15f; // 敏感度
    public float maxTurnSpeed = 20f; // 最大水平移动速度
    public float maxTilt = 20f; // 最大倾斜角

    #endregion


    #region private field

    // 电脑端的字段
    private Vector2 range; // 一个二维向量
    private Quaternion mStart;  //  四元数
    private Vector2 mRot = Vector2.zero;//旋转

    // 移动端的字段
    private Vector3 m_tagetTransform; 
    private Vector3 m_mobileOrientation; // 手机陀螺仪变化值

    #endregion


    #region monobehavior callbacks

    void Start () {
        range = new Vector2(rotateDegree * Screen.width / Screen.height, rotateDegree); // 定义一个二维向量
        mStart = transform.localRotation;  // 获取当前组件的本地旋转四元数
    }
	
	void Update ()
	{
	    if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
	    {
	        TransformTargetOnMobile();
	    }
	    else
	    {
	        TransformTargetOnComputer();
        }
	}

    #endregion

    #region custom methods

    private void TransformTargetOnComputer()
    {
        Vector3 pos = Input.mousePosition;   //  获取鼠标位置  
        float halfWidth = Screen.width * 0.5f;   // 相对原点x  
        float halfHeight = Screen.height * 0.5f; // 相对原点y  
        float x = Mathf.Clamp((pos.x - halfWidth) / halfWidth, -1f, 1f);  // 返回一个【-1,1】的x值  
        float y = Mathf.Clamp((pos.y - halfHeight) / halfHeight, -1f, 1f); // 返回一个【-1,1】的x值  
        //基于浮点数t返回a到b之间的插值，t限制在0～1之间。当t = 0返回from，当t = 1 返回to。当t = 0.5 返回from和to的平均值  
        // Quaternion.Euler(new Vector3（）)  返回一个旋转角度，绕z轴旋转z度，绕x轴旋转x度，绕y轴旋转y度（像这样的顺序）。   
        mRot = Vector2.Lerp(mRot, new Vector2(y, x), Time.deltaTime * rotateSpeed);  // 插值运算  
        transform.localRotation = mStart * Quaternion.Euler(-mRot.x * range.x, mRot.y * range.y, 0f);
    }

    private void TransformTargetOnMobile()
    {
        m_mobileOrientation = Input.acceleration;
        Vector3 orientation = Quaternion.Euler(-45f, 0, 0) * m_mobileOrientation;

        m_tagetTransform.y = Mathf.Lerp(m_tagetTransform.y, -orientation.x * maxTurnSpeed, 3f * Time.deltaTime);
        m_tagetTransform.x = Mathf.Lerp(m_tagetTransform.x, -orientation.z * maxTilt, 3f * Time.deltaTime);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(m_tagetTransform), Time.deltaTime * sensitivity);
    }

    #endregion


    #region custom coroutines



    #endregion
}
