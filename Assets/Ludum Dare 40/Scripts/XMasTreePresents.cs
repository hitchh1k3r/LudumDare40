using UnityEngine;
using System;
using System.Collections.Generic;

public class XMasTreePresents : MonoBehaviour
{

  // Referances:
  public PresentSprite[] presents;
  public NavigationPlane plane;

  // State:
  private int lastCount = -1;

  // Messages:

  void Update()
  {
    if(Input.GetButtonDown("Submit"))
    {
      PresentData present = new PresentData();
      present.color = HitchLib.ColorEnum.DARK_TURQUOISE;
      GameStateManager.Presents.Add(present);
    }
    List<PresentData> presentData = GameStateManager.Presents;
    int count = presentData.Count;
    if(lastCount != count)
    {
      lastCount = count;
      int ring = 0;
      if(count >= 66)
      {
        ring = 6;
      }
      else if(count >= 50)
      {
        ring = 5;
      }
      else if(count >= 39)
      {
        ring = 4;
      }
      else if(count >= 28)
      {
        ring = 3;
      }
      else if(count >= 16)
      {
        ring = 2;
      }
      else if(count >= 9)
      {
        ring = 1;
      }
      for(int i = 1; i <= 7; ++i)
      {
        plane.exceptions[i].navigable = (i < (7 - ring));
      }
      for(int i = 0; i < presents.Length; ++i)
      {
        presents[i].go.SetActive(count > i);
        if(count > i)
        {
          Color color = HitchLib.Colors.FromEnum(presentData[i].color);
          float H, S, V;
          Color.RGBToHSV(color, out H, out S, out V);
          presents[i].box.color = color;
          presents[i].ribbon.color = Color.HSVToRGB((H + 0.5f) % 1.0f, S * 0.5f, (V * 0.25f) + 0.75f);
        }
      }
    }
  }

  // Tools:

  [ContextMenu("Find Presents")]
  void FindPresents()
  {
    int c = transform.GetChildCount();
    presents = new PresentSprite[c+1];
    for(int i = 1; i < c; ++i)
    {
      PresentSprite present;
      present.go = transform.GetChild(i).gameObject;
      present.box = present.go.GetComponent<SpriteRenderer>();
      present.ribbon = present.go.transform.GetChild(0).GetComponent<SpriteRenderer>();
      presents[i-1] = present;
    }
  }

}

[Serializable]
public struct PresentSprite
{

  public GameObject go;
  public SpriteRenderer box;
  public SpriteRenderer ribbon;

}
