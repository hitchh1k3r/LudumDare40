using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PresentSelling : MonoBehaviour, IPointerClickHandler
{

  // Referances:
  public PresentData present;
  public TextMeshProUGUI price;
  public TextMeshProUGUI tag;

  // Messages:
  void OnEnable()
  {
    price.text = "$" + present.price;
    tag.color = HitchLib.Colors.FromEnum(present.color);
    tag.text = "<color=#777777>A gift from:</color> <b>" + present.friend + "</b>";
    tag.autoSizeTextContainer = false;
  }

  // Interface IPointerClickHandler:

  public void OnPointerClick(PointerEventData eventData)
  {
    GameStateManager.Presents.Remove(present);
    ++GameStateManager.Collector.presentsSold;
    GameStateManager.Collector.moneyEarned += present.price;
    GameStateManager.State.currentMoney += present.price;
    Destroy(gameObject);
  }

}
