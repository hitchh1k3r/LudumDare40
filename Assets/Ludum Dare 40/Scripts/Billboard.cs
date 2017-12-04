using UnityEngine;

public class Billboard : MonoBehaviour
{

  // Configuration:
  public bool lateralBillboard;

  // Messages:

  void Update()
  {
    if(lateralBillboard)
    {
      Vector3 e = Camera.main.transform.rotation.eulerAngles;
      e.x = 0;
      e.z = 0;
      transform.rotation = Quaternion.Euler(e);
    }
    else
    {
      transform.rotation = Camera.main.transform.rotation;
    }
  }

}
