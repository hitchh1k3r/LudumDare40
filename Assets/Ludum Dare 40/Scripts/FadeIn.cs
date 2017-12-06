using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{

  // Referances:
  public CanvasGroup group;

  IEnumerator Start()
  {
    if(!GameStateManager.LoadGame())
    {
      InterfaceManager.ShowLogin();
    }
#if UNITY_WEBGL
    yield return new WaitForSeconds(1.0f);
#endif
    yield return HitchLib.Tweening.EasyUIHide(group);
    Destroy(gameObject);
  }

}
