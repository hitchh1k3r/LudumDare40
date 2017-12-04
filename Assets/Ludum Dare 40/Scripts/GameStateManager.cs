using UnityEngine;
using System;
using System.Collections.Generic;

public class GameStateManager : HitchLib.Singleton // MonoBehaviour
{

  // Public State:
  public bool isMenu;
  public List<PresentData> presents;

  // Statis Accessors:

  public static bool IsMenu {
    get { return instance.isMenu; }
    set {
      instance.isMenu = value;
      if(value)
      {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
      }
      else
      {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
      }
    }
  }

  public static bool HasFocus {
    get { return instance.isMenu || Cursor.lockState == CursorLockMode.Locked; }
  }

  public static int PresentCount {
    get { return instance.presents.Count; }
  }

  public static List<PresentData> Presents {
    get { return instance.presents; }
  }

  // Static Instance:
  private static GameStateManager instance;

  // Messages:

  void Awake()
  {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update()
  {
    if(!isMenu)
    {
      Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
      if(Input.GetMouseButton(0) && screenRect.Contains(Input.mousePosition))
      {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
      }
      else if(Input.GetButton("Cancel"))
      {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
      }
    }
  }

  // Interface Singleton:

  protected override void SetInstance(GameObject gameObject)
  {
    instance = gameObject.GetComponent<GameStateManager>();
  }

}

[Serializable]
public struct PresentData
{

  public HitchLib.ColorEnum color;
  public int price;

}
