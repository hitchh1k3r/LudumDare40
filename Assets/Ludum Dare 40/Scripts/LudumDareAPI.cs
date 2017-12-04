using UnityEngine;
using System;
using System.Collections;

public class LudumDareAPI : HitchLib.Singleton // MonoBehaviour
{

  // State:
  private int ludumDareID = -1;
  private Texture2D userAvatar;
  private string username;
  private Coroutine apiRequest;

  // Static Instance:
  private static LudumDareAPI instance;

  // Static Utilities:

  public static void SetUser(string username, Action onFinished)
  {
    if(instance.apiRequest != null)
    {
      instance.StopCoroutine(instance.apiRequest);
    }
    instance.apiRequest = instance.StartCoroutine(instance.DoLDLookup(username, onFinished));
  }

  public static bool Ready()
  {
    return instance.ludumDareID >= 0;
  }

  public static int GetUserID()
  {
    return instance.ludumDareID;
  }

  public static Texture2D GetAvatar()
  {
    return instance.userAvatar;
  }

  public static string GetUsername()
  {
    return instance.username;
  }

  // Private Utilities:

  private IEnumerator DoLDLookup(string username, Action onFinished)
  {
    WWWForm form = new WWWForm();
    form.AddField("username", username);
    WWW req = new WWW("https://hitchh1k3rsguide.com/api/ld.php", form);
    yield return req;
    UserDataJSON userdata = JsonUtility.FromJson<UserDataJSON>(req.text);
    if(userdata != null && userdata.status == 200)
    {
      if(userdata.node[0].name != "Users")
      {
        this.username = userdata.node[0].name;
        ludumDareID = userdata.node[0].id;
        form = new WWWForm();
        form.AddField("img", userdata.node[0].meta.avatar + ".256x256.png");
        req = new WWW("https://hitchh1k3rsguide.com/api/ld_img.php", form);
        yield return req;
        if(!string.IsNullOrEmpty(req.text))
        {
          userAvatar = req.texture;
          if(userAvatar != null)
          {
            userAvatar.filterMode = FilterMode.Point;
            userAvatar.wrapMode = TextureWrapMode.Clamp;
          }
        }
      }
    }
    onFinished();
  }

  // JSON Objects:

  [Serializable]
  private class UserDataJSON
  {
    public int status;
    public int caller_id;
    public UserDataNode[] node;
  }

  [Serializable]
  private class UserDataNode
  {
    public int id;
    public string slug;
    public string name;
    public UserMeta meta;
  }

  [Serializable]
  private class UserMeta
  {
    public string avatar;
  }

  // Interface Singleton:

  protected override void SetInstance(GameObject gameObject)
  {
    instance = gameObject.GetComponent<LudumDareAPI>();
  }

}
