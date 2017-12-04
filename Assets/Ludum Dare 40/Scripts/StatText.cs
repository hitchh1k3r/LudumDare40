using UnityEngine;
using TMPro;

public class StatText : MonoBehaviour
{

  // Referances:
  public TextMeshProUGUI text;

  void Update()
  {
    text.text = "Years Active:\n" +
          "<align=right>" + (GameStateManager.State.currentYear-1) + "</align>\n" +
          "\n" +
          "Friends:\n" +
          "<align=right>" + GameStateManager.FriendCount + "</align>\n" +
          "\n" +
          "Friend Requests:\n" +
          "<align=right>3</align>\n" +
          "\n" +
          "Money:\n" +
          "<align=right>$" + GameStateManager.State.currentMoney + "</align>\n" +
          "\n" +
          "Money Spent:\n" +
          "<align=right>$" + GameStateManager.Collector.moneySpent + "</align>\n" +
          "\n" +
          "Money Recieved:\n" +
          "<align=right>$" + GameStateManager.Collector.moneyEarned + "</align>\n" +
          "\n" +
          "Presents Owned:\n" +
          "<align=right>" + GameStateManager.PresentCount + "</align>\n" +
          "\n" +
          "Lava Lamps:\n" +
          "<align=right>" + GameStateManager.Collector.numberLavaLampsPurchased + "</align>";
  }

}
