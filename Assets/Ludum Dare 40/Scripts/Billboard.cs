using UnityEngine;

public class Billboard : MonoBehaviour
{

  // Messages:

  void Update()
  {
    transform.rotation = Camera.main.transform.rotation;
  }

}
