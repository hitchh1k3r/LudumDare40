using UnityEngine;

public class GUICloser : MonoBehaviour
{

  // Messages:

  void Update()
  {
    if(Input.GetButtonDown("Cancel"))
    {
      InterfaceManager.HideMenu();
    }
  }

}
