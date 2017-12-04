using UnityEngine;
using TMPro;

public class YearMoneyDisplay : MonoBehaviour
{

  // Referances:
  public TextMeshProUGUI text;

  void Update()
  {
    text.text = "Year " + GameStateManager.State.currentYear + "\n$" +
          GameStateManager.State.currentMoney;
  }

}
