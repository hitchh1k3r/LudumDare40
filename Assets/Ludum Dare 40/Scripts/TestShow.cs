using UnityEngine;
using System.Collections;

public class TestShow : MonoBehaviour
{

  IEnumerator Start()
  {
    while(string.IsNullOrEmpty(LudumDareAPI.GetUsername()))
    {
      yield return null;
    }
    InterfaceManager.HideMenu();
  }

}
