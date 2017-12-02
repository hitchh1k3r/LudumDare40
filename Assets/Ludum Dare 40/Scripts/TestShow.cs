using UnityEngine;
using System.Collections;

public class TestShow : MonoBehaviour
{

  IEnumerator Start()
  {
    yield return new WaitForSeconds(1);
    GameStateManager.IsMenu = true;
    yield return HitchLib.Tweening.EasyUIShow(GetComponent<CanvasGroup>(), positionStart:
      new Vector2(0, -400));
    while(string.IsNullOrEmpty(LudumDareAPI.GetUsername()))
    {
      yield return null;
    }
    yield return new WaitForSeconds(5.0f);
    yield return HitchLib.Tweening.EasyUIHide(GetComponent<CanvasGroup>(), positionEnd:
      new Vector2(0, -400));
    GameStateManager.IsMenu = false;
  }

}
