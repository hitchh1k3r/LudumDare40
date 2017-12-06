using UnityEngine;
using System.Collections.Generic;

public class FriendList : MonoBehaviour
{

  // Referances:
  public GameObject prefabFriend;

  // Cache:
  private int lastFrientCount;
  private int lastChildCount;

  void Update()
  {
    if(lastFrientCount != GameStateManager.FriendCount)
    {
      lastFrientCount = GameStateManager.FriendCount;
      BuildList();
    }
    if(lastChildCount != transform.childCount)
    {
      lastChildCount = transform.childCount;
      int i;
      for(i = 0; i < lastChildCount; ++i)
      {
        RectTransform t = (RectTransform)transform.GetChild(i).transform;
        t.anchoredPosition = new Vector2(0, (-55 * i) - 5);
      }
      RectTransform trans = (RectTransform)transform;
      trans.sizeDelta = new Vector2(trans.sizeDelta.x, (55 * i) + 5);
    }
  }

  private void BuildList()
  {
    lastChildCount = 0;
    for(int i = 0; i < transform.childCount; ++i)
    {
      Destroy(transform.GetChild(i).gameObject);
    }
    List<FriendData> friends = GameStateManager.Friend;
    for(int i = 0; i < friends.Count; ++i)
    {
      FriendListing friend = Instantiate<GameObject>(prefabFriend, transform).
            GetComponent<FriendListing>();
      friend.friend = friends[i];
      friend.gameObject.SetActive(true);
    }
  }

}
