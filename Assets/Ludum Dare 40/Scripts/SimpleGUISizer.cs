using UnityEngine;
using UnityEngine.UI;

public class SimpleGUISizer : MonoBehaviour
{

  // Referances:
  public RectTransform left;
  public RectTransform right;

  // Cache:
  private RectTransform container;
  private Vector2 lastSize;

  // Messages:

  void Awake()
  {
    container = (RectTransform)transform;
  }

  void Update()
  {
    if(lastSize != container.sizeDelta)
    {
      lastSize = container.sizeDelta;

      float targetWidth = (container.sizeDelta.x / 2)  - 5;
      left.sizeDelta = new Vector2(targetWidth, left.sizeDelta.y);
      right.sizeDelta = new Vector2(targetWidth, right.sizeDelta.y);
    }
  }

}
