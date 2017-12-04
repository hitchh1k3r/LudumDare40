using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Request : MonoBehaviour
{

  // Referances:
  public CanvasGroup group;
  public TextMeshProUGUI request;
  public GameObject ignoreButton;

  // State:
  private bool interactable;

  public void HitButton(bool isAccept)
  {
    if(interactable)
    {
      interactable = false;
      if(isAccept)
      {
        FriendData friend = GameStateManager.FriendRequests[0];
        friend.friendedOnYear = GameStateManager.State.currentYear;
        GameStateManager.Friend.Add(friend);
        GameStateManager.FriendToBuy.Add(friend);
        GameStateManager.FriendQueue.Add(friend);
        ++GameStateManager.Collector.numberFriendRequestsAccepted;
      }
      StartCoroutine(HitchLib.Tweening.EasyUIHide(group, 0.25f, callbackEnding: () => {
              gameObject.SetActive(false);
              if(GameStateManager.FriendRequests.Count > 0)
              {
                Show(GameStateManager.FriendRequests[0]);
              }
            }));
      GameStateManager.FriendRequests.RemoveAt(0);
    }
  }

  // Utilities:
  public void Show(FriendData friend)
  {
    interactable = false;
    request.color = HitchLib.Colors.FromEnum(friend.color);
    request.text = "<b>" + friend.name + "</b> <color=#777777>would like\n" +
          "to be your friend!</color>\n";
    if(GameStateManager.State.currentYear > 1 && GameStateManager.FriendCount > 0)
    {
      request.text += "<color=#aaaaaa><size=17.5>(<i>Friend of <b>" +
            GameStateManager.Friend[Random.Range(0, GameStateManager.FriendCount)].name +
            "</b></i>)</size></color>";
    }
    ignoreButton.SetActive(GameStateManager.State.currentYear > 1);
    gameObject.SetActive(true);
    StartCoroutine(HitchLib.Tweening.EasyUIShow(group, 0.25f, callbackEnding: () => {
            interactable = true;
          }));
  }

}
