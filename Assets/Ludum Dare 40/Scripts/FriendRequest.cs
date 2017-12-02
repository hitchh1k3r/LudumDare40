using UnityEngine;
using UnityEngine.UI;

public class FriendRequest : MonoBehaviour
{

  // Referances:
  public TMPro.TextMeshProUGUI requestText;

  // Messages:
  void OnEnable()
  {
    requestText.text = "<b>" + NameManager.GetName() + "</b> <color=#777>(<i>Friend of <b>" +
          NameManager.GetName() + "</b></i>)</color>";
    requestText.autoSizeTextContainer = false;
  }

}
