using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class FriendListing : MonoBehaviour
{

  // Referances:
  public FriendData friend;
  public TextMeshProUGUI friendName;
  public RectTransform happyBar;
  public RectTransform happyBarChange;
  public TextMeshProUGUI statLine;

  // Messages:
  void OnEnable()
  {
    friendName.color = HitchLib.Colors.FromEnum(friend.color);
    friendName.text = friend.name;
    friendName.autoSizeTextContainer = false;
    statLine.text = "Friend For " + (GameStateManager.State.currentYear - friend.friendedOnYear) +
          " Years";
    float totalPrecent = Mathf.Max(friend.happyPrecent, friend.happyPrecentLastYear);
    happyBar.localScale = new Vector3(totalPrecent, 1, 1);
    if(totalPrecent != 0)
    {
      float changePrecent = (totalPrecent - Mathf.Min(friend.happyPrecent,
            friend.happyPrecentLastYear)) / totalPrecent;
      happyBarChange.localScale = new Vector3(changePrecent, 1, 1);
    }
    if(friend.happyPrecent < friend.happyPrecentLastYear)
    {
      happyBarChange.GetComponent<Graphic>().color = Color.red;
    }
    else
    {
      happyBarChange.GetComponent<Graphic>().color = Color.green;
    }
  }

}
