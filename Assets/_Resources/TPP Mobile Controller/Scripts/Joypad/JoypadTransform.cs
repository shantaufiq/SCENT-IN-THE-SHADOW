using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JoypadTransform : MonoBehaviour
{
  public RectTransform thisGORect;
  public float maxArea;

  public void ResetGO()
  {
    thisGORect.localPosition = Vector3.zero;
  }

  public void Update()
  {
    var xPos = Math.Abs(transform.localPosition.x);
    var yPos = Math.Abs(transform.localPosition.y);

    var xRes = xPos <= maxArea ? transform.localPosition.x : transform.localPosition.x < 0 ? -(maxArea) : maxArea;
    var yRes = yPos <= maxArea ? transform.localPosition.y : transform.localPosition.y < 0 ? -(maxArea) : maxArea;

    transform.localPosition = new Vector2(xRes, yRes).normalized * maxArea;
  }
}
