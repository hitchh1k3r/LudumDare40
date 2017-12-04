using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleButton : MonoBehaviour
{

  // Referances:
  public Request request;
  public bool isAccept;

  // Messages:

  void OnMouseDown()
  {
    Debug.Log("HIT");
    request.HitButton(isAccept);
  }
}
