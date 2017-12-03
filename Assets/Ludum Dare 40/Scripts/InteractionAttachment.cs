using UnityEngine;

public class InteractionAttachment : MonoBehaviour
{

  // Public State:
  public Transform target;

  // Messages:

  void Update()
  {
    if(target != null)
    {
      transform.position = target.position;
    }
  }

}
