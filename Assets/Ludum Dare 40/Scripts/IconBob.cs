using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconBob : MonoBehaviour
{

  public float bobAmount = 0.25f;
  public float bobSpeed = 0.75f;

  private Vector3 basePosition;
  private float bobTimer;

  void OnEnable()
  {
    basePosition = transform.localPosition;
    bobTimer = 0;
  }

  void Update()
  {
    bobTimer += Time.deltaTime * bobSpeed;
    if(bobTimer > 2)
    {
      bobTimer = 0;
    }

    float t;
    if(bobTimer < 1)
    {
      t = HitchLib.Easing.EASE_CUBIC_IN_OUT(bobTimer);
    }
    else
    {
      t = 1 - HitchLib.Easing.EASE_CUBIC_IN_OUT(bobTimer-1);
    }
    transform.localPosition = basePosition + t * bobAmount * Vector3.up;
  }

}
