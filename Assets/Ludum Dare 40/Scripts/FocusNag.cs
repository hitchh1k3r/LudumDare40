using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FocusNag : MonoBehaviour
{

  // Cache:
  private CanvasGroup group;

  // State:
  private bool focusTracker;
  private Coroutine animation;

  // Messages:

  void Awake()
  {
    group = GetComponent<CanvasGroup>();
  }

  void Update()
  {
    if(GameStateManager.HasFocus != focusTracker)
    {
      focusTracker = GameStateManager.HasFocus;
      if(animation != null)
      {
        StopCoroutine(animation);
      }
      if(focusTracker)
      {
        animation = StartCoroutine(HitchLib.Tweening.EasyUIHide(group, 0.25f));
      }
      else
      {
        animation = StartCoroutine(HitchLib.Tweening.EasyUIShow(group, 0.25f));
      }
    }
  }

}
