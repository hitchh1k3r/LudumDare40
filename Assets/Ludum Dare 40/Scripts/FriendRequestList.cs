using UnityEngine;

public class FriendRequestList : MonoBehaviour
{

  // Referances:
  public GameObject request;

  // Configuration:
  public int requests = 25;

  void Awake()
  {
    int i;
    for(i = 0; i < requests; ++i)
    {
      GameObject go = Instantiate<GameObject>(request, transform);
      RectTransform t = (RectTransform)go.transform;
      t.anchoredPosition = new Vector2(0, (-55 * i) - 5);
    }
    RectTransform trans = (RectTransform)transform;
    trans.sizeDelta = new Vector2(trans.sizeDelta.x, (55 * i) + 5);
  }

}
