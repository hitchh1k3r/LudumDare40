using UnityEngine;

public class GameStateManager : HitchLib.Singleton // MonoBehaviour
{

  // Public State:
  public bool isMenu = false;

  // Statis Accessors:

  public static bool IsMenu {
    get { return instance.isMenu; }
    set { instance.isMenu = value; }
  }

  // Static Instance:
  private static GameStateManager instance;

  // Interface Singleton:

  protected override void SetInstance(GameObject gameObject)
  {
    instance = gameObject.GetComponent<GameStateManager>();
  }

}
