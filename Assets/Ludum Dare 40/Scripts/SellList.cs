using UnityEngine;
using System.Collections.Generic;

public class SellList : MonoBehaviour
{

  // Referances:
  public GameObject prefabSale;

  // Cache:
  private int lastChildCount;

  void OnEnable()
  {
    lastChildCount = 0;
    for(int i = 0; i < transform.childCount; ++i)
    {
      Destroy(transform.GetChild(i).gameObject);
    }
    List<PresentData> presents = GameStateManager.Presents;
    for(int i = 0; i < presents.Count; ++i)
    {
      PresentSelling sale = Instantiate<GameObject>(prefabSale, transform).
            GetComponent<PresentSelling>();
      sale.present = presents[i];
      sale.gameObject.SetActive(true);
    }
  }

  void Update()
  {
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

}
