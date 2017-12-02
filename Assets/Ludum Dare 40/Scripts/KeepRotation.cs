using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRotation : MonoBehaviour
{

  // Configurartion:
  public Quaternion rotation = Quaternion.identity;

  // Messages:

  void LateUpdate()
  {
    transform.rotation = rotation;
  }

}
