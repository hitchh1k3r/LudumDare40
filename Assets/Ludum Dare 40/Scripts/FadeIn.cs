using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{

  // Referances:
  public CanvasGroup group;

  IEnumerator Start()
  {
    yield return new WaitForSeconds(1.0f);
    yield return HitchLib.Tweening.EasyUIHide(group);
    Destroy(gameObject);
  }

}
