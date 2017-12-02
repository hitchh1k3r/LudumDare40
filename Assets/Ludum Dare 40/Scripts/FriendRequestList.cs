using UnityEngine;

public class FriendRequestList : MonoBehaviour
{

  // Referances:
  public GameObject request;

  void Awake()
  {
    for(int i = 0; i < 25; ++i)
    {
      Instantiate(request, transform);
    }
  }

}
