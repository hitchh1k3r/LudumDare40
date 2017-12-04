using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class GameStateManager : HitchLib.Singleton // MonoBehaviour
{

  // Public State:
  public bool isMenu;
  public CurrentState state;
  public StatCollector stats;
  public List<PresentData> presents;
  public List<FriendData> friends;

  // Static Accessors:

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

  public static CurrentState State {
    get { return instance.state; }
  }

  public static StatCollector Collector {
    get { return instance.stats; }
  }

  public static int PresentCount {
    get { return instance.presents.Count; }
  }

  public static List<PresentData> Presents {
    get { return instance.presents; }
  }

  public static int FriendCount {
    get { return instance.friends.Count; }
  }

  public static List<FriendData> Friend {
    get { return instance.friends; }
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
      if(Cursor.lockState == CursorLockMode.None && Input.GetButtonDown("Cancel"))
      {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
      }
      else
      {
        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
        if(Input.GetMouseButton(0) && screenRect.Contains(Input.mousePosition))
        {
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
        }
        else if(Input.GetButtonDown("Cancel"))
        {
          Cursor.lockState = CursorLockMode.None;
          Cursor.visible = true;
        }
      }
    }
  }

  // Interface Singleton:

  protected override void SetInstance(GameObject gameObject)
  {
    instance = gameObject.GetComponent<GameStateManager>();
  }

  // Utilities:

  public static void ResetGame()
  {
    instance.DoResetGame();
  }

  private void DoResetGame()
  {
    HitchLib.Singleton.ResetSingletons();
    PlayerPrefs.DeleteKey("SaveData");
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public static void SaveGame()
  {
    instance.DoSaveGame();
  }

  [HitchLib.Invokable]
  private void DoSaveGame()
  {
    SaveData data;
    data.state = state;
    data.stats = stats;
    data.presents = presents;
    data.friends = friends;
    PlayerPrefs.SetString("SaveData", JsonUtility.ToJson(data));
  }

  public static bool LoadGame()
  {
    return instance.DoLoadGame();
  }

  [HitchLib.Invokable]
  private bool DoLoadGame()
  {
    if(PlayerPrefs.HasKey("SaveData"))
    {
      SaveData data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("SaveData"));
      state = data.state;
      stats = data.stats;
      presents = data.presents;
      friends = data.friends;
      return true;
    }
    return false;
  }

}

[Serializable]
public struct CurrentState
{

  public int currentYear;
  public int currentMoney;

}

[Serializable]
public struct SaveData
{

  public CurrentState state;
  public StatCollector stats;
  public List<PresentData> presents;
  public List<FriendData> friends;

}

[Serializable]
public struct PresentData
{

  public HitchLib.ColorEnum color;
  public int price;

}

[Serializable]
public struct FriendData
{

  public string name;
  public bool isFemale;
  public HitchLib.ColorEnum color;
  public int friendedOnYear;
  public int moneySpentOnFriend;
  public int moneySpentOnPlayer;
  public int numberGiftsFromPlayer;
  public int numberGiftsToPlayer;

}

[Serializable]
public struct StatCollector
{

  public int longestFriendshipYears;
  public string longestFriendshipName;
  public int happiestFriendScore;
  public string happiestFriendName;
  public int angriestFriendScore;
  public string angriestFriendName;
  public int maxConcurrentFriends;
  public int moneySpent;
  public int moneyEarned;
  public int moneyGiftedAway;
  public int numberGiftsPurchased;
  public int numberLavaLampsPurchased;
  public int numberFriendRequestsAccepted;
  public int numberFriendsLost;

}
