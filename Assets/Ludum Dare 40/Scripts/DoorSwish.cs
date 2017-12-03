using UnityEngine;
using System.Collections;

public class DoorSwish : MonoBehaviour
{

  // Referances:
  public DoorLocation location;
  public Transform door1;
  public Transform door2;

  // Cache:
  private float startAngle1;
  private float startAngle2;

  // Instance:
  public static DoorSwish storeInstance;
  public static DoorSwish homeInstance;

  // Messages:

  void Awake()
  {
    startAngle1 = door1.eulerAngles.y;
    if(location == DoorLocation.Store)
    {
      startAngle2 = door2.eulerAngles.y;
      storeInstance = this;
    }
    else if(location == DoorLocation.Home)
    {
      homeInstance = this;
    }
    StartSwish(location);
  }

  public static void StartSwish(DoorLocation location)
  {
    if(location == DoorLocation.Store)
    {
      storeInstance.StartCoroutine(storeInstance.DoSwish());
    }
    else if(location == DoorLocation.Home)
    {
      homeInstance.StartCoroutine(homeInstance.DoSwish());
    }
  }

  private IEnumerator DoSwish()
  {
    if(location == DoorLocation.Store)
    {
      StartCoroutine(HitchLib.Tweening.TransRotate(door1, Quaternion.Euler(0, startAngle1, 0),
            Quaternion.Euler(0, 180, 0), 5.0f, HitchLib.Easing.EASE_ELASTIC_OUT));
      yield return HitchLib.Tweening.TransRotate(door2, Quaternion.Euler(0, startAngle2, 0),
            Quaternion.Euler(0, 180, 0), 5.5f, HitchLib.Easing.EASE_ELASTIC_OUT);
    }
    else
    {
      yield return HitchLib.Tweening.TransRotate(door1, Quaternion.Euler(0, startAngle1, 0),
            Quaternion.Euler(0, 180, 0), 5.0f, HitchLib.Easing.EASE_ELASTIC_OUT);
    }
  }

  public enum DoorLocation
  {
    Store,
    Home
  }

}
