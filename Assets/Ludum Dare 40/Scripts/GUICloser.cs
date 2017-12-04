using UnityEngine;

public class GUICloser : MonoBehaviour
{

  // Referances:
  public GameObject closePrompt;
  public Request request;

  // Messages:

  void OnEnable()
  {
    if(GameStateManager.FriendRequests.Count > 0)
    {
      request.Show(GameStateManager.FriendRequests[0]);
    }
  }

  void Update()
  {
    if(!closePrompt.activeSelf && GameStateManager.State.currentMoney > 0)
    {
      closePrompt.SetActive(true);
      StartCoroutine(HitchLib.Tweening.EasyUIShow(closePrompt.GetComponent<CanvasGroup>()));
    }
    if((GameStateManager.State.currentYear > 1 || closePrompt.activeSelf) &&
          Input.GetButtonDown("Cancel"))
    {
      InterfaceManager.HideMenu();
    }
  }

}
