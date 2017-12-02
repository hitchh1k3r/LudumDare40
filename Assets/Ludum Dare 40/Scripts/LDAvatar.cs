using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class LDAvatar : MonoBehaviour
{

  // Cache:
  private RawImage image;
  private int lastUserID = -1;

  // Messages:

  void Awake()
  {
    image = GetComponent<RawImage>();
    image.enabled = false;
  }

  void Update()
  {
    if(lastUserID != LudumDareAPI.GetUserID())
    {
      Texture2D tex = LudumDareAPI.GetAvatar();
      if(tex != null)
      {
        lastUserID = LudumDareAPI.GetUserID();
        image.texture = tex;
        image.enabled = true;
      }
    }
  }

}
