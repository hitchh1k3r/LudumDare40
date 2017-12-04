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
  public List<FriendData> pendingRequests;
  public List<FriendData> friendsToBuy;
  public List<FriendData> friendsToQueue;
  public List<FriendData> friendsBought;
  public HitchLib.ColorEnum[] colorList;
  public HitchLib.ColorEnum[] presentColors;

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

  public static HitchLib.ColorEnum[] PresentColors {
    get { return instance.presentColors; }
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

  public static List<FriendData> FriendRequests {
    get { return instance.pendingRequests; }
  }

  public static List<FriendData> FriendToBuy {
    get { return instance.friendsToBuy; }
  }

  public static List<FriendData> FriendBought {
    get { return instance.friendsBought; }
  }

  public static List<FriendData> FriendQueue {
    get { return instance.friendsToQueue; }
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

  public static void NewYear()
  {
    instance.pendingRequests.Clear();
    for(int i = 0; i < 2; ++i)
    {
      instance.pendingRequests.Add(GenerateFriend());
    }

    // FRIEND LOST!

    // GET PRESENTS FROM FRIENDS

    // NEW UPGRADES

    // CALCULATE SUPERLATIVES (IN THE COLLECTOR, FOR HS SYSTEM)

    PrepareFriends();
    SaveGame();
  }

  public static void PrepareFriends()
  {
    instance.friendsBought.Clear();
    instance.friendsToBuy.Clear();
    instance.friendsToQueue.Clear();
    foreach(FriendData friend in instance.friends)
    {
      if(friend.happyTarget < 0)
      {
        friend.happyTarget = 0;
      }
      if(friend.happyTarget > 1)
      {
        friend.happyTarget = 1;
      }
      friend.happyPrecentLastYear = friend.happyPrecent;
      friend.happyPrecent = friend.happyTarget;
      instance.friendsToBuy.Add(friend);
      instance.friendsToQueue.Add(friend);
    }
    TryAddingCachiers();
  }

  public static void TryAddingCachiers()
  {
    for(int i = 0; i < InteractionCheckout.instances.Length; ++i)
    {
      if(InteractionCheckout.instances[i].isActiveAndEnabled &&
            InteractionCheckout.instances[i].friend == null)
      {
        if(instance.friendsToQueue.Count > 0)
        {
          InteractionCheckout.instances[i].SetFriend(instance.friendsToQueue[
                UnityEngine.Random.Range(0, instance.friendsToQueue.Count)]);
        }
        else
        {
          InteractionCheckout.instances[i].SetFriend(null);
        }
      }
    }
  }

  public static void GeneratePresentColors()
  {
    List<HitchLib.ColorEnum> list = new List<HitchLib.ColorEnum>();
    foreach(FriendData friend in instance.friends)
    {
      if(!list.Contains(friend.color))
      {
        list.Add(friend.color);
      }
    }
    instance.presentColors = list.ToArray();
  }

  public static FriendData GenerateFriend()
  {
    FriendData friend = new FriendData();
    friend.isFemale = (UnityEngine.Random.Range(0, 2) == 0);
    friend.name = NameManager.GetName(friend.isFemale);
    friend.color = instance.colorList[UnityEngine.Random.Range(0,
          Math.Min(instance.state.currentYear, instance.colorList.Length))];
    friend.happyPrecent = 0.5f;
    friend.happyPrecentLastYear = 0.5f;
    friend.happyTarget = 0.5f;
    return friend;
  }

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
    data.friendRequests = pendingRequests.Count;
    PlayerPrefs.SetString("SaveData", JsonUtility.ToJson(data));
  }

  public static bool LoadGame()
  {
    return instance.DoLoadGame();
  }

  [HitchLib.Invokable]
  private bool DoLoadGame()
  {
    bool loaded = false;
    int fReq = 3;
    if(PlayerPrefs.HasKey("SaveData"))
    {
      SaveData data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("SaveData"));
      state = data.state;
      stats = data.stats;
      presents = data.presents;
      friends = data.friends;
      fReq = data.friendRequests;
      loaded = true;
    }
    pendingRequests.Clear();
    for(int i = 0; i < fReq; ++i)
    {
      pendingRequests.Add(GenerateFriend());
    }
    PrepareFriends();
    return loaded;
  }

}

[Serializable]
public class CurrentState
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
  public int friendRequests;

}

[Serializable]
public struct PresentData
{

  public HitchLib.ColorEnum color;
  public int price;
  public string friend;

}

[Serializable]
public class FriendData
{

  public string name;
  public bool isFemale;
  public HitchLib.ColorEnum color;
  public int friendedOnYear;
  public int moneySpentOnFriend;
  public int moneySpentOnPlayer;
  public int numberGiftsFromPlayer;
  public int numberGiftsToPlayer;
  public float happyPrecent;
  public float happyPrecentLastYear;
  public float happyTarget;

  public void GivePresent(Transform item)
  {
    GameStateManager.FriendToBuy.Remove(this);
    GameStateManager.FriendBought.Add(this);
    Present present = item.GetComponent<Present>();
    happyTarget += 0.1f;
    if(present == null || present.color == color)
    {
      happyTarget += 0.4f;
    }
  }

}

[Serializable]
public class StatCollector
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
