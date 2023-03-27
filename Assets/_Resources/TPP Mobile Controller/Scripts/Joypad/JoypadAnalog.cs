using UnityEngine;

class JoypadAnalog : MonoBehaviour
{
  public UIVirtualTouchZone virtualTouchZone;
  public RectTransform analogControl;
  public RectTransform parentTransform;
  public JoypadTransform joypadAnalog;

  private float joystickRadius;

  void Start()
  {
    joystickRadius = parentTransform.sizeDelta.y / 8;
  }

  private void Update()
  {
    float joystickDist = Vector2.Distance(virtualTouchZone.dragPosition, virtualTouchZone.pointerPosition);

    if (virtualTouchZone.isPointerUp)
    {
      joypadAnalog.ResetGO();
      analogControl.localPosition = joypadAnalog.thisGORect.localPosition;
    }
    else
    {
      if (virtualTouchZone.isPointerDown)
      {
        analogControl.localPosition = Vector3.zero;
      }
      else
      {
        analogControl.position =
          virtualTouchZone.pointerPosition + virtualTouchZone.joystickVec *
          joystickDist;
      }
    }

  }
}