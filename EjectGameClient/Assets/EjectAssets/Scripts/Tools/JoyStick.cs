using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    public float radius;
    public float minValueDistance;
    public float maxValueDistance;
    public bool isHorizontal = true;
    public bool isVertical = true;
    public Camera uiCamera;
    //public RectTransform checkRect;
    public RectTransform target;
    public RectTransform dragStick;
    public CircleButtonImage joystickImg;

    public bool PointerDown
    {
        get { return isPointerDown; }
    }

    public Vector3 Direction
    {
        get { return direction; }
    }

    private bool isPointerDown = false;
    private Vector3 rawVelocity = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private RectTransform joystick;


    void Awake()
    {
        joystick = GetComponent<RectTransform>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (IsSmallerThanMinValueDistance())
        {
            direction = Vector3.zero;
        }
        else
        {
            rawVelocity = Mathf.Clamp(target.anchoredPosition.magnitude / maxValueDistance, 0f, 1f) * target.anchoredPosition.normalized;
            direction = new Vector3(rawVelocity.x, 0, rawVelocity.y);
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        isPointerDown = true;
        Vector2 mouseDrag = eventData.position;
        Vector2 uguiPos = new Vector2();

        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(joystick, mouseDrag, uiCamera, out uguiPos);

        if (isRect)
        {
            dragStick.anchoredPosition = uguiPos;

            if (dragStick.anchoredPosition.magnitude <= radius)
            {
                target.anchoredPosition = dragStick.anchoredPosition;
            }
            else
            {
                target.anchoredPosition = dragStick.anchoredPosition.normalized * radius;
            }

            target.anchoredPosition = new Vector2(isHorizontal ? target.anchoredPosition.x : 0, isVertical ? target.anchoredPosition.y : 0);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isPointerDown = false;
        dragStick.anchoredPosition = Vector2.zero;
        target.anchoredPosition = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        Vector2 mousePos = eventData.position;
        
        bool isInCircle = joystickImg.IsRaycastLocationValid(mousePos, eventData.enterEventCamera);

        if (isInCircle)
        {
            Vector2 mouseUguiPos = new Vector2();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(joystick, mousePos, eventData.enterEventCamera, out mouseUguiPos);
            dragStick.anchoredPosition = mouseUguiPos;
            target.anchoredPosition = new Vector2(isHorizontal ? mouseUguiPos.x : 0, isVertical ? mouseUguiPos.y : 0);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
    }

    public bool IsSmallerThanMinValueDistance()
    {
        return target.anchoredPosition.magnitude < minValueDistance;
    }
}