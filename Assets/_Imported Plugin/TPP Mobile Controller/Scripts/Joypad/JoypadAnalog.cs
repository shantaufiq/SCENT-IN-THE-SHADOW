using UnityEngine;

class JoypadAnalog : MonoBehaviour
{
    public UIVirtualTouchZone virtualTouchZone;
    public RectTransform analogControl;
    public RectTransform parentTransform;
    public JoypadTransform joypadAnalog;

    public GameObject[] possitionFocus;

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

                if ((virtualTouchZone.pointerPosition.x <= 0.5f || virtualTouchZone.pointerPosition.x >= -0.5f) && virtualTouchZone.pointerPosition.y >= 0.5f)
                {
                    // Debug.Log($"{Time.time} Maju kedepan. handle poss: {virtualTouchZone.pointerPosition}");
                    possitionFocus[0].SetActive(true);
                }
                else
                {
                    possitionFocus[0].SetActive(false);
                }

                if ((virtualTouchZone.pointerPosition.x <= 0.5f || virtualTouchZone.pointerPosition.x >= -0.5f) && virtualTouchZone.pointerPosition.y <= -0.5f)
                {
                    // Debug.Log($"{Time.time} Mundur kebelakang. handle poss: {virtualTouchZone.pointerPosition}");
                    possitionFocus[2].SetActive(true);
                }
                else
                {
                    possitionFocus[2].SetActive(false);
                }

                if ((virtualTouchZone.pointerPosition.y <= 0.5f || virtualTouchZone.pointerPosition.y >= -0.5f) && virtualTouchZone.pointerPosition.x >= 0.5f)
                {
                    // Debug.Log($"{Time.time} hadap kanan. handle poss: {virtualTouchZone.pointerPosition}");
                    possitionFocus[1].SetActive(true);
                }
                else
                {
                    possitionFocus[1].SetActive(false);
                }

                if ((virtualTouchZone.pointerPosition.y <= 0.5f || virtualTouchZone.pointerPosition.y >= -0.5f) && virtualTouchZone.pointerPosition.x <= -0.5f)
                {
                    // Debug.Log($"{Time.time} hadap kiri. handle poss: {virtualTouchZone.pointerPosition}");
                    possitionFocus[3].SetActive(true);
                }
                else
                {
                    possitionFocus[3].SetActive(false);
                }
            }
        }

    }
}