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

  public static void SetUser(string username)
  {
    if(instance.apiRequest != null)
    {
      instance.StopCoroutine(instance.apiRequest);
    }
    instance.apiRequest = instance.StartCoroutine(instance.DoLDLookup(username));
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

  private IEnumerator DoLDLookup(string username)
  {
    WWW req = new WWW("https://api.ldjam.com/vx/node/walk/1/users/" + username);
    yield return req;
    UserNodeJSON usernode = JsonUtility.FromJson<UserNodeJSON>(req.text);
    print(usernode);
    if(usernode.status == 200)
    {
      ludumDareID = usernode.node;
      req = new WWW("https://api.ldjam.com/vx/node/get/" + ludumDareID);
      yield return req;
      UserDataJSON userdata = JsonUtility.FromJson<UserDataJSON>(req.text);
      if(userdata.status == 200)
      {
        this.username = userdata.node[0].name;
        req = new WWW("https://static.jam.vg" + userdata.node[0].meta.avatar + ".256x256.png");
        yield return req;
        userAvatar = req.texture;
        userAvatar.filterMode = FilterMode.Point;
        userAvatar.wrapMode = TextureWrapMode.Clamp;
      }
    }
  }

  // JSON Objects:

  [Serializable]
  private class UserNodeJSON
  {
    public int status;
    public int caller_id;
    public int root;
    public int node;
  }

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
