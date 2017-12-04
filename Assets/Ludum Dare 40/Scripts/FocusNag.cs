using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FocusNag : MonoBehaviour
{

  // Referance:
  public GameObject quitPrompt;
  public CanvasGroup fadeOut;

  // Cache:
  private CanvasGroup group;

  // State:
  private bool focusTracker;
  private Coroutine animation;
  private float resetTimer;

  // Messages:

  void Awake()
  {
    group = GetComponent<CanvasGroup>();
#if UNITY_WEBGL
    quitPrompt.SetActive(false);
#endif
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
    if(!focusTracker)
    {
      if(Input.GetButton("Reset"))
      {
        if(resetTimer < 1)
        {
          resetTimer += 0.2f * Time.deltaTime;
          if(resetTimer >= 1)
          {
            GameStateManager.ResetGame();
          }
        }
      }
      else
      {
        resetTimer = 0;
      }
      fadeOut.alpha = resetTimer;
#if !UNITY_WEBGL
      if(Input.GetButtonDown("Quit"))
      {
        Application.Quit();
      }
#endif
    }
  }

}
