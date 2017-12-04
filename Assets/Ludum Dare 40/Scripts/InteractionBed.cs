using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class InteractionBed : MonoBehaviour, IInteracatble
{

  // Configuration:
  public Transform interactionPoint;
  public CanvasGroup fade;
  public CanvasGroup text;
  public TextMeshProUGUI yearText;

  // Interface IInteracatble:

  public InteractionType CanInteract(Interactor actor)
  {
    if(GameStateManager.State.currentYear > 1 || GameStateManager.State.currentMoney <= 0)
    {
      return InteractionType.Inventory;
    }
    else
    {
      return InteractionType.None;
    }
  }

  public Transform InteractIconPosition(InteractionType type, Interactor actor)
  {
    return interactionPoint;
  }

  public void Interact(InteractionType type, Interactor actor)
  {
    if(!GameStateManager.IsMenu)
    {
      // TODO (hitch) A SNOW EFFECT WOULD LOOK AWESOME!!!!
      GameStateManager.IsMenu = true;
      yearText.text = "Year " + (GameStateManager.State.currentYear + 1);
      text.alpha = 0;
      StartCoroutine(DoNewYear());
    }
  }

  private IEnumerator DoNewYear()
  {
    fade.gameObject.SetActive(true);
    StartCoroutine(HitchLib.Tweening.EasyUIShow(fade, 4.5f,
          easingFade: HitchLib.Easing.EASE_QUAD_OUT));
    yield return new WaitForSeconds(2.0f);
    yield return HitchLib.Tweening.EasyUIShow(text, 1.5f);
    yield return new WaitForSeconds(3.0f);
    ++GameStateManager.State.currentYear;
    GameStateManager.NewYear();
    yield return HitchLib.Tweening.EasyUIHide(fade, 2.0f);
    GameStateManager.IsMenu = false;
    fade.gameObject.SetActive(false);
  }

}
