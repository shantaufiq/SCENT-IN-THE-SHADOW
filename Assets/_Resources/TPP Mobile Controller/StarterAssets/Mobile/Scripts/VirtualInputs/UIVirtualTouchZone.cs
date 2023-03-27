using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIVirtualTouchZone : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
  [System.Serializable]
  public class Event : UnityEvent<Vector2> { }

  [Header("Rect References")]
  public RectTransform containerRect;
  public RectTransform handleRect;
  public RectTransform disableRect;

  [Header("Settings")]
  public bool clampToMagnitude;
  public float magnitudeMultiplier = 1f;
  public bool invertXOutputValue;
  public bool invertYOutputValue;

  //Stored Pointer Values
  public Vector2 pointerDownPosition;
  public Vector2 pointerPosition;
  public Vector2 dragPosition;
  public Vector2 joystickVec;
  public Vector2 currentPointerPosition;
  public bool isPointerUp;
  public bool isPointerDown;

  [Header("Output")]
  public Event touchZoneOutputEvent;

  void Start()
  {
    SetupHandle();
  }

  private void SetupHandle()
  {
    if (handleRect)
    {

      if (disableRect)
      {
        SetObjectActiveState(handleRect.gameObject, disableRect.gameObject, false);
      }
      else
      {
        SetObjectActiveState(handleRect.gameObject, false);
      }
    }
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera, out pointerDownPosition);

    pointerPosition = eventData.position;
    isPointerDown = true;

    if (handleRect)
    {

      if (disableRect)
      {
        SetObjectActiveState(handleRect.gameObject, disableRect.gameObject, true);
      }
      else
      {
        SetObjectActiveState(handleRect.gameObject, true);
      }
      UpdateHandleRectPosition(pointerDownPosition);
    }
  }

  public void OnDrag(PointerEventData eventData)
  {

    dragPosition = eventData.position;
    joystickVec = (dragPosition - pointerPosition).normalized;

    isPointerDown = false;


    RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera, out currentPointerPosition);

    Vector2 positionDelta = GetDeltaBetweenPositions(pointerDownPosition, currentPointerPosition);

    Vector2 clampedPosition = ClampValuesToMagnitude(positionDelta);

    Vector2 outputPosition = ApplyInversionFilter(clampedPosition);

    OutputPointerEventValue(outputPosition * magnitudeMultiplier);
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    pointerDownPosition = Vector2.zero;
    currentPointerPosition = Vector2.zero;

    OutputPointerEventValue(Vector2.zero);

    isPointerDown = false;


    if (handleRect)
    {
      if (disableRect)
      {
        SetObjectActiveState(handleRect.gameObject, disableRect.gameObject, false);
      }
      else
      {
        SetObjectActiveState(handleRect.gameObject, false);
      }

      UpdateHandleRectPosition(Vector2.zero);
    }
  }

  void OutputPointerEventValue(Vector2 pointerPosition)
  {
    this.pointerPosition = pointerPosition;
    touchZoneOutputEvent.Invoke(pointerPosition);
  }

  void UpdateHandleRectPosition(Vector2 newPosition)
  {
    handleRect.anchoredPosition = newPosition;
  }

  void SetObjectActiveState(GameObject targetObject, bool newState)
  {
    targetObject.SetActive(newState);
  }

  void SetObjectActiveState(GameObject targetObject, GameObject disableObject, bool newState)
  {
    targetObject.SetActive(newState);
    disableObject.SetActive(!newState);
    isPointerUp = !newState;
  }

  Vector2 GetDeltaBetweenPositions(Vector2 firstPosition, Vector2 secondPosition)
  {
    return secondPosition - firstPosition;
  }

  Vector2 ClampValuesToMagnitude(Vector2 position)
  {
    return Vector2.ClampMagnitude(position, 1);
  }

  Vector2 ApplyInversionFilter(Vector2 position)
  {
    if (invertXOutputValue)
    {
      position.x = InvertValue(position.x);
    }

    if (invertYOutputValue)
    {
      position.y = InvertValue(position.y);
    }

    return position;
  }

  float InvertValue(float value)
  {
    return -value;
  }

}
