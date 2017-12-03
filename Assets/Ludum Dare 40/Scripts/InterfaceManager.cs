using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{

  // Referances:
  public CanvasGroup login;
  public CanvasGroup friendBay;

  // Public State:
  public CurrentMenu menu;

  // Static Instance:
  private static InterfaceManager instance;

  // Messages:

  void Awake()
  {
    login.gameObject.SetActive(false);
    friendBay.gameObject.SetActive(false);
    instance = this;
    if(menu == CurrentMenu.Login)
    {
      GameStateManager.IsMenu = true;
      login.gameObject.SetActive(true);
    }
    else if(menu == CurrentMenu.FriendBay)
    {
      GameStateManager.IsMenu = true;
      friendBay.gameObject.SetActive(true);
    }
  }

  // Public Utilities:

  public static void HideMenu()
  {
    if(instance.menu == CurrentMenu.Login)
    {
      instance.StartCoroutine(HitchLib.Tweening.EasyUIHide(instance.login, callbackEnding:
            () => {
                GameStateManager.IsMenu = false;
                instance.login.gameObject.SetActive(false);
                instance.menu = CurrentMenu.None;
              }));
    }
    else if(instance.menu == CurrentMenu.FriendBay)
    {
      instance.StartCoroutine(HitchLib.Tweening.EasyUIHide(instance.friendBay, callbackEnding:
            () => {
                GameStateManager.IsMenu = false;
                instance.friendBay.gameObject.SetActive(false);
                instance.menu = CurrentMenu.None;
              }));
    }
  }

  public static void ShowFriendBay()
  {
    GameStateManager.IsMenu = true;
    instance.friendBay.gameObject.SetActive(true);
    instance.menu = CurrentMenu.FriendBay;
    instance.StartCoroutine(HitchLib.Tweening.EasyUIShow(instance.friendBay, positionStart:
      new Vector2(0, -400)));
  }

  public static void ShowLogin()
  {
    GameStateManager.IsMenu = true;
    instance.login.gameObject.SetActive(true);
    instance.menu = CurrentMenu.Login;
    instance.StartCoroutine(HitchLib.Tweening.EasyUIShow(instance.login, positionStart:
      new Vector2(0, -400)));
  }

  public enum CurrentMenu
  {
    None,
    Login,
    FriendBay
  }

}
