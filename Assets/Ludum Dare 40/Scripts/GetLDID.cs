using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class GetLDID : MonoBehaviour
{

  // Referances:
  public RawImage userImage;
  public TMP_InputField firstName;
  public TMP_InputField lastName;
  public TMP_InputField input;

  // State:
  private int ludumDareID = -1;
  private bool lookingUp = false;

  void Awake()
  {
    string[] name = NameManager.GetName().Split(' ');
    firstName.text = name[0];
    lastName.text = name[1];
  }

  public void SubmitName()
  {
    if(!lookingUp)
    {
      lookingUp = true;
      StartCoroutine(DoLDLookup(input.text));
    }
  }

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
        req = new WWW("https://static.jam.vg" + userdata.node[0].meta.avatar + ".256x256.png");
        yield return req;
        Texture tex = req.texture;
        tex.filterMode = FilterMode.Point;
        tex.wrapMode = TextureWrapMode.Clamp;
        userImage.texture = tex;
      }
    }
    lookingUp = false;
  }

  [Serializable]
  public class UserNodeJSON
  {
    public int status;
    public int caller_id;
    public int root;
    public int node;
  }

  [Serializable]
  public class UserDataJSON
  {
    public int status;
    public int caller_id;
    public UserDataNode[] node;
  }

  [Serializable]
  public class UserDataNode
  {
    public string slug;
    public string name;
    public UserMeta meta;
  }

  [Serializable]
  public class UserMeta
  {
    public string avatar;
  }

}
