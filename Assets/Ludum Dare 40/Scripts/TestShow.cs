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
  }

}
