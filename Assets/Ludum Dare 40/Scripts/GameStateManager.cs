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
  public Upgrades[] gameUps;

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

  public static void ApplyUpgrades()
  {
    NavigationPlane store = NavigationPlane.FindPlane("Store_Main");

    if(State.currentGameUpgrade > 6)
    {
      StoreReferances.instance.belt.moveSpeed = new Vector3(2.0f, 0, 0);
      StoreReferances.instance.belt.spawnMin = 0.75f;
      StoreReferances.instance.belt.spawnMax = 0.75f;
    }
    else if(State.currentGameUpgrade > 1)
    {
      StoreReferances.instance.belt.moveSpeed = new Vector3(1.0f, 0, 0);
      StoreReferances.instance.belt.spawnMin = 1.0f;
      StoreReferances.instance.belt.spawnMax = 3.0f;
    }
    else
    {
      StoreReferances.instance.belt.moveSpeed = new Vector3(0.5f, 0, 0);
      StoreReferances.instance.belt.spawnMin = 3.0f;
      StoreReferances.instance.belt.spawnMax = 5.0f;
    }

    StoreReferances.instance.table1.SetActive(State.currentGameUpgrade > 2);
    store.exceptions[1].navigable = !(State.currentGameUpgrade > 2);

    StoreReferances.instance.table2.SetActive(State.currentGameUpgrade > 7);
    store.exceptions[2].navigable = !(State.currentGameUpgrade > 7);

    InteractionCheckout.instances[1].gameObject.SetActive(State.currentGameUpgrade > 4);
    store.exceptions[7].navigable = !(State.currentGameUpgrade > 4);
    store.exceptions[8].navigable = !(State.currentGameUpgrade > 4);
    store.exceptions[9].navigable = !(State.currentGameUpgrade > 4);

    InteractionCheckout.instances[2].gameObject.SetActive(State.currentGameUpgrade > 9);
    store.exceptions[10].navigable = !(State.currentGameUpgrade > 9);
    store.exceptions[11].navigable = !(State.currentGameUpgrade > 9);
    store.exceptions[12].navigable = !(State.currentGameUpgrade > 9);

    if(!StoreReferances.instance.storeTimerOn)
    {
      StoreReferances.instance.storeTimerLeft = (State.currentGameUpgrade > 5) ? 300 : 150;
    }
  }

  public static void SetUpgrades()
  {
    if(State.currentGameUpgrade < instance.gameUps.Length)
    {
      Buying.upgrade2 = instance.gameUps[State.currentGameUpgrade];
    }
    else
    {
      Buying.upgrade2 = Upgrades.NONE;
    }
  }

  public static void NewYear()
  {
    instance.pendingRequests.Clear();
    int numReq = (int)(((State.currentGameUpgrade > 8) ? 1.0f : 0.25f) * instance.friends.Count) +
          1;
    if(numReq > 100)
    {
      numReq = 100;
    }
    for(int i = 0; i < numReq; ++i)
    {
      instance.pendingRequests.Add(GenerateFriend());
    }

    StoreReferances.instance.storeTimerOn = false;
    SetUpgrades();
    ApplyUpgrades();

    foreach(FriendData friend in instance.friendsToBuy)
    {
      friend.happyTarget -= 0.25f;
    }

    List<FriendData> lostFriends = new List<FriendData>();
    foreach(FriendData friend in instance.friends)
    {
      if(friend.happyTarget < 0)
      {
        ++instance.stats.numberFriendsLost;
        lostFriends.Add(friend);
      }
      else
      {
        if(friend.happyTarget > 1)
        {
          friend.happyTarget = 1;
        }
        friend.happyPrecentLastYear = friend.happyPrecent;
        friend.happyPrecent = friend.happyTarget;
        friend.happyScore += friend.happyPrecent;
        friend.angryScore += (1 - friend.happyPrecent);
        instance.friendsToBuy.Add(friend);
        instance.friendsToQueue.Add(friend);
      }
    }
    foreach(FriendData friend in lostFriends)
    {
      instance.friends.Remove(friend);
    }

    foreach(FriendData friend in instance.friends)
    {
      PresentData present;
      present.color = friend.color;
      present.price = UnityEngine.Random.Range(2, 4);
      present.friend = friend.name;
      instance.presents.Add(present);
    }

    foreach(FriendData friend in instance.friends)
    {
      if(friend.happyScore > instance.stats.happiestFriendScore)
      {
        instance.stats.happiestFriendName = friend.name;
        instance.stats.happiestFriendScore = friend.happyScore;
      }
      if(friend.angryScore > instance.stats.angriestFriendScore)
      {
        instance.stats.angriestFriendName = friend.name;
        instance.stats.angriestFriendScore = friend.angryScore;
      }

      if((State.currentYear - friend.friendedOnYear) > instance.stats.longestFriendshipYears)
      {
        instance.stats.longestFriendshipName = friend.name;
        instance.stats.longestFriendshipYears = (State.currentYear - friend.friendedOnYear);
      }

      if(instance.friends.Count > instance.stats.maxConcurrentFriends)
      {
        instance.stats.maxConcurrentFriends = instance.friends.Count;
      }
    }

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
    friend.happyPrecent = 0.25f;
    friend.happyPrecentLastYear = 0.25f;
    friend.happyTarget = 0.25f;
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
    data.playerName = NameManager.GetMyName();
    data.ldName = LudumDareAPI.GetUsername();
    PlayerPrefs.SetString("SaveData", JsonUtility.ToJson(data));
    WWWForm form = new WWWForm();
    data.friends = null;
    data.presents = null;
    form.AddField("SaveData", JsonUtility.ToJson(data));
    WWW req = new WWW("https://hitchh1k3rsguide.com/api/ld40_highscore.php", form);
    StartCoroutine(req);
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
      if(!string.IsNullOrEmpty(data.playerName))
      {
        string[] nameBits = data.playerName.Split(new [] {' '}, 2);
        NameManager.SetMyName(nameBits[0], nameBits[1]);
      }
      if(!string.IsNullOrEmpty(data.ldName))
      {
        LudumDareAPI.SetUser(data.ldName, () => {});
      }
      loaded = true;
    }
    pendingRequests.Clear();
    for(int i = 0; i < fReq; ++i)
    {
      pendingRequests.Add(GenerateFriend());
    }
    PrepareFriends();
    SetUpgrades();
    ApplyUpgrades();
    return loaded;
  }

}

[Serializable]
public class CurrentState
{

  public int currentYear;
  public int currentMoney;
  public int currentGameUpgrade;

}

[Serializable]
public struct SaveData
{

  public CurrentState state;
  public StatCollector stats;
  public List<PresentData> presents;
  public List<FriendData> friends;
  public int friendRequests;
  public string playerName;
  public string ldName;

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
  public float happyScore;
  public float angryScore;

  public void GivePresent(Transform item)
  {
    GameStateManager.FriendToBuy.Remove(this);
    GameStateManager.FriendBought.Add(this);
    Present present = item.GetComponent<Present>();
    if(present != null)
    {
      if(present.color == color)
      { // CORRECT COLOR
        happyTarget += 0.15f;
      }
      else
      {
        happyTarget += 0.0f;
      }
    }
    else
    { // LAVA LAMP
      happyTarget += 0.3f;
    }
  }

}

[Serializable]
public class StatCollector
{

  public int longestFriendshipYears;
  public string longestFriendshipName;
  public float happiestFriendScore;
  public string happiestFriendName;
  public float angriestFriendScore;
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
