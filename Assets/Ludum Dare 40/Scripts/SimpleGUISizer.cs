using UnityEngine;
using UnityEngine.UI;

public class SimpleGUISizer : MonoBehaviour
{

  // Referances:
  public RectTransform left;
  public RectTransform gutter;
  public RectTransform right;

  // Configuration:
  public float minLeftSize = 212;
  public float minRightSize = 212;

  // Cache:
  private RectTransform container;
  private Vector2 lastSize;

  // State:
  private bool dragging;
  private float draggingOffset;
  private float positionDivider = 0.5f;

  // Messages:

  void Awake()
  {
    container = (RectTransform)transform;
  }

  void Update()
  {
    if(gutter != null)
    {
      if(Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(gutter,
            Input.mousePosition))
      {
        dragging = true;
        draggingOffset = (Input.mousePosition.x / Screen.width) - positionDivider;
      }
      if(dragging)
      {
        if(Input.GetMouseButton(0))
        {
          positionDivider = (Input.mousePosition.x / Screen.width) + draggingOffset;
          lastSize = Vector2.zero;
        }
        else
        {
          dragging = false;
        }
      }
    }
    if(lastSize != container.rect.size)
    {
      lastSize = container.rect.size;

      float targetLeft = (container.rect.size.x - 5) * positionDivider;
      float targetRight = (container.rect.size.x - 5) * (1 - positionDivider);
      if(targetLeft < minLeftSize)
      {
        targetLeft = minLeftSize;
        targetRight = container.rect.size.x - 5 - minLeftSize;
        positionDivider = minLeftSize / container.rect.size.x;
      }
      else if(targetRight < minRightSize)
      {
        targetLeft = container.rect.size.x - 5 - minRightSize;
        targetRight = minRightSize;
        positionDivider = 1 - (minRightSize / container.rect.size.x);
      }
      left.sizeDelta = new Vector2(targetLeft, left.sizeDelta.y);
      if(gutter != null)
      {
        gutter.anchoredPosition = new Vector2(((container.rect.size.x - 10) * positionDivider) + 5f,
              0);
      }
      right.sizeDelta = new Vector2(targetRight, right.sizeDelta.y);
    }
  }

}
