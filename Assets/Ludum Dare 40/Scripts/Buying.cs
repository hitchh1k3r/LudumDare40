using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Buying : MonoBehaviour
{

  // Referances:
  public GameObject up1;
  public TextMeshProUGUI up1Text;
  public GameObject up2;
  public TextMeshProUGUI up2Text;
  public GameObject lavaLamp;
  public CanvasGroup endingFade;
  public CanvasGroup endingText;
  public CanvasGroup highscoreFade;
  public TextMeshProUGUI highscoreText1;
  public TextMeshProUGUI highscoreText2;
  public TextMeshProUGUI highscoreText3;
  public TextMeshProUGUI highscoreText4;
  public TextMeshProUGUI highscoreYear;
  public TextMeshProUGUI highscoreMoney;

  // Cache:
  private Upgrades last1 = Upgrades.NONE;
  private Upgrades last2 = Upgrades.NONE;
  private Interactor playerActor;

  // Static:
  public static Upgrades upgrade1 = Upgrades.LAVA_LAMP;
  public static Upgrades upgrade2 = Upgrades.BELT_SPEED;

  public void HitButton(int which)
  {
    Upgrades up = upgrade1;
    if(which == 2)
    {
      up = upgrade2;
    }

    if(GameStateManager.State.currentMoney >= GetPrice(up))
    {
      GameStateManager.Collector.moneySpent += GetPrice(up);
      GameStateManager.State.currentMoney -= GetPrice(up);
      ApplyUpgrade(up);
      if(which == 2)
      {
        upgrade2 = Upgrades.NONE;
      }
      else
      {
        upgrade1 = Upgrades.NONE;
      }
    }
  }

  // Messages:

  void Update()
  {
    if(last1 != upgrade1)
    {
      last1 = upgrade1;
      if(upgrade1 != Upgrades.NONE)
      {
        up1.SetActive(true);
        up1Text.text = "<size=15><color=#f00><align=right>$" + GetPrice(upgrade1) +
                "</align></color></size>\n" +
              "<color=#000>" + GetName(upgrade1) + "</color>\n" +
              GetDescription(upgrade1);
      }
      else
      {
        up1.SetActive(false);
      }
    }
    if(last2 != upgrade2)
    {
      last2 = upgrade2;
      if(upgrade2 != Upgrades.NONE)
      {
        up2.SetActive(true);
        up2Text.text = "<size=15><color=#f00><align=right>$" + GetPrice(upgrade2) +
                "</align></color></size>\n" +
              "<color=#000>" + GetName(upgrade2) + "</color>\n" +
              GetDescription(upgrade2);
      }
      else
      {
        up2.SetActive(false);
      }
    }
    if(playerActor == null)
    {
      playerActor = PlayerController.Get.GetComponent<Interactor>();
    }
    if(playerActor.GetItem() == null)
    {
      upgrade1 = Upgrades.LAVA_LAMP;
    }
    else
    {
      upgrade1 = Upgrades.NONE;
    }
  }

  // Utilities:

  private int GetPrice(Upgrades upgrade)
  {
    switch(upgrade)
    {
      case Upgrades.LAVA_LAMP:
        return 5;
      case Upgrades.BELT_SPEED:
        return 10;
      case Upgrades.FRIEND_INFO:
        return 5;
      case Upgrades.FASTER_PLAYER:
        return 7;
      case Upgrades.MORE_FRIENDS:
        return 20;
      case Upgrades.LONGER_TURN:
        return 10;
      case Upgrades.EXTRA_SWAP:
        return 5;
      case Upgrades.EXTRA_CHECKOUT:
        return 15;
      case Upgrades.FRIEND_MONEY:
        return 5;
      case Upgrades.ENDGAME:
        return 100;
    }
    return 0;
  }

  private string GetName(Upgrades upgrade)
  {
    switch(upgrade)
    {
      case Upgrades.LAVA_LAMP:
        return "Lava Lamp";
      case Upgrades.BELT_SPEED:
        return "Faster Conveyor";
      case Upgrades.FRIEND_INFO:
        return "Friend Investigator";
      case Upgrades.FASTER_PLAYER:
        return "New Shoes";
      case Upgrades.MORE_FRIENDS:
        return "Personal Ad";
      case Upgrades.LONGER_TURN:
        return "Open Late";
      case Upgrades.EXTRA_SWAP:
        return "Expanding";
      case Upgrades.EXTRA_CHECKOUT:
        return "Now Hiring";
      case Upgrades.FRIEND_MONEY:
        return "FriendSpace Premium";
      case Upgrades.ENDGAME:
        return "Lava Lamp Factory";
    }
    return "Unknown Upgrade";
  }

  private string GetDescription(Upgrades upgrade)
  {
    switch(upgrade)
    {
      case Upgrades.LAVA_LAMP:
        return "buy the perfect gift, any friend would be happy to get this from you";
      case Upgrades.BELT_SPEED:
        return "buy a better belt for the present store, presents will cycle faster";
      case Upgrades.FRIEND_INFO:
        return "hire an private investigator to learn a bit more about a friend";
      case Upgrades.FASTER_PLAYER:
        return "these shoes will allow you to run around the present store a bit faster";
      case Upgrades.MORE_FRIENDS:
        return "start a \"personal ad\" campaing to get more friend requests";
      case Upgrades.LONGER_TURN:
        return "the present store is open late, you will have longer each year to buy presents";
      case Upgrades.EXTRA_SWAP:
        return "the present store is gowing, now you'll have an extra table to place presents on before buying";
      case Upgrades.EXTRA_CHECKOUT:
        return "the present store is hiring more cachiers, allowing you to buy for two friends at once";
      case Upgrades.FRIEND_MONEY:
        return "enhance your FriendSpace account, allowing you too see how your friends feel about you";
      case Upgrades.ENDGAME:
        return "build a lava lamp factory and never run out of gifts for your friends again";
    }
    return "something went wrong, you should not be seeing this";
  }

  private void ApplyUpgrade(Upgrades upgrade)
  {
    switch(upgrade)
    {
      case Upgrades.LAVA_LAMP:
        {
          ++GameStateManager.Collector.numberLavaLampsPurchased;
          GameObject go = Instantiate<GameObject>(lavaLamp);
          playerActor.AddItem(go.transform);
        } break;
      case Upgrades.BELT_SPEED:
        {
          ++GameStateManager.State.currentGameUpgrade;
        } break;
      case Upgrades.FASTER_PLAYER:
        {
          ++GameStateManager.State.currentGameUpgrade;
        } break;
      case Upgrades.MORE_FRIENDS:
        {
          ++GameStateManager.State.currentGameUpgrade;
        } break;
      case Upgrades.LONGER_TURN:
        {
          ++GameStateManager.State.currentGameUpgrade;
        } break;
      case Upgrades.EXTRA_SWAP:
        {
          ++GameStateManager.State.currentGameUpgrade;
        } break;
      case Upgrades.EXTRA_CHECKOUT:
        {
          ++GameStateManager.State.currentGameUpgrade;
        } break;
      case Upgrades.FRIEND_MONEY:
        {
          ++GameStateManager.State.currentGameUpgrade;
        } break;
      case Upgrades.ENDGAME:
        {
          GameStateManager.State.currentGameUpgrade = 11;
          GameStateManager.IsMenu = true;
          StartCoroutine(Ending());
        } break;
    }
    GameStateManager.ApplyUpgrades();
  }

  private HighscoreData highscoreData;

  private string ToPrecent(float prec)
  {
    return (prec * 100).ToString("0.0");
  }

  private string HighStringCount(ScoreEntry[] scores, string suffix)
  {
    string build = "";
    foreach(ScoreEntry score in scores)
    {
      build += score.user + " :: " + score.integer + " " + suffix + "\n";
    }
    for(int i = scores.Length; i < 3; ++i)
    {
      build += "\n";
    }
    return build;
  }

  private string HighStringFriendCount(ScoreEntry[] scores, string suffix)
  {
    string build = "";
    foreach(ScoreEntry score in scores)
    {
      build += score.user + " :: " + score.str + " (" + score.integer + " " + suffix + ")\n";
    }
    for(int i = scores.Length; i < 3; ++i)
    {
      build += "\n";
    }
    return build;
  }

  private string HighStringFriendPrecent(ScoreEntry[] scores)
  {
    string build = "";
    foreach(ScoreEntry score in scores)
    {
      build += score.user + " :: " + score.str + " (" + ToPrecent(score.real)+ "%)\n";
    }
    for(int i = scores.Length; i < 3; ++i)
    {
      build += "\n";
    }
    return build;
  }

  private string HighStringMoney(ScoreEntry[] scores)
  {
    string build = "";
    foreach(ScoreEntry score in scores)
    {
      build += score.user + " :: $" + score.integer + "\n";
    }
    for(int i = scores.Length; i < 3; ++i)
    {
      build += "\n";
    }
    return build;
  }

  private string HighStringMoneyCount(ScoreEntry[] moneyScores, ScoreEntry[] countScores, string suffix)
  {
    string build = "";
    int i;
    for(i = 0; i < moneyScores.Length && i < countScores.Length; ++i)
    {
      build += countScores[i].user + " :: " + countScores[i].integer + " " + suffix + " ($" + moneyScores[i].integer + ")\n";
    }
    for(; i < 3; ++i)
    {
      build += "\n";
    }
    return build;
  }

  private IEnumerator Ending()
  {
    Coroutine sendAndGet = StartCoroutine(SendAndGetScores());
    endingFade.gameObject.SetActive(true);
    StartCoroutine(HitchLib.Tweening.EasyUIShow(endingFade, 2.5f,
          easingFade: HitchLib.Easing.EASE_QUAD_OUT));
    yield return new WaitForSeconds(2.0f);
    yield return HitchLib.Tweening.EasyUIShow(endingText, 2.0f);
    float timer = 0;
    while(!(Input.GetButtonDown("Cancel") || Input.GetButtonDown("Submit") ||
          Input.GetButtonDown("Grab")) && timer < 7.5f)
    {
      yield return null;
      timer += Time.deltaTime;
    }
    yield return HitchLib.Tweening.EasyUIHide(endingText, 2.0f);
    yield return sendAndGet;


    highscoreYear.text = GameStateManager.State.currentYear + " years";
    highscoreMoney.text = "$" + GameStateManager.State.currentMoney;
    highscoreText1.text = GameStateManager.Collector.numberFriendRequestsAccepted + " requests\n" +
          GameStateManager.Collector.maxConcurrentFriends + " friends\n" +
          GameStateManager.Collector.longestFriendshipName + "(" +
                GameStateManager.Collector.longestFriendshipYears + " years)\n" +
          GameStateManager.Collector.numberFriendsLost + " friends\n" +
          GameStateManager.Collector.happiestFriendName + " (" +
                ToPrecent(GameStateManager.Collector.happiestFriendScore) + "%)";

    highscoreText2.text = GameStateManager.Collector.presentsSold + " presents ($" +
                GameStateManager.Collector.moneyEarned + ")\n" +
          "$" + GameStateManager.Collector.moneySpent + "\n" +
          GameStateManager.Collector.numberGiftsPurchased + " presents ($" +
                GameStateManager.Collector.moneyGiftedAway + ")\n" +
          GameStateManager.Collector.numberLavaLampsPurchased + " lava lamps\n" +
          GameStateManager.Collector.angriestFriendName + " (" +
                ToPrecent(GameStateManager.Collector.angriestFriendScore) + "%)";

    try
    {
      highscoreText3.text = HighStringCount(highscoreData.win_years, "years") + "\n" +
            HighStringCount(highscoreData.num_req, "requests") + "\n" +
            HighStringCount(highscoreData.friend_max, "friends") + "\n" +
            HighStringFriendCount(highscoreData.friend_years, "years") + "\n" +
            HighStringFriendPrecent(highscoreData.friend_happy) + "\n" +
            HighStringCount(highscoreData.num_lost, "friends");

      highscoreText4.text = HighStringCount(highscoreData.years, "years") + "\n" +
            HighStringMoney(highscoreData.money_spending) + "\n" +
            HighStringMoneyCount(highscoreData.money_selling, highscoreData.num_sold, "presents") + "\n" +
            HighStringMoneyCount(highscoreData.money_gifted, highscoreData.num_gifts, "presents") + "\n" +
            HighStringCount(highscoreData.num_lamps, "lava lamps") + "\n" +
            HighStringFriendPrecent(highscoreData.friend_angry_min) + "\n" +
            HighStringFriendPrecent(highscoreData.friend_angry_max);
    }
    catch (System.Exception ignore)
    {
      highscoreText3.text = "";
      highscoreText4.text = "";
    }

    yield return HitchLib.Tweening.EasyUIShow(highscoreFade, 2.0f);
#if !UNITY_WEBGL
    while(!(Input.GetButtonDown("Cancel") || Input.GetButtonDown("Submit") ||
          Input.GetButtonDown("Grab")))
    {
      yield return null;
    }
    Application.Quit();
#endif
  }

  private IEnumerator SendAndGetScores()
  {
    yield return GameStateManager.SendHighscore();
    WWW req = new WWW("https://hitchh1k3rsguide.com/api/ld40_highscore.php");
    yield return req;
    highscoreData = JsonUtility.FromJson<HighscoreData>(req.text);
  }

  [System.Serializable]
  public struct HighscoreData
  {
    public ScoreEntry[] years; // Max Years Played For
    public ScoreEntry[] win_years; // Minimum Years Beaten In
    public ScoreEntry[] money; // Maximum Money Had At One Time
    public ScoreEntry[] friend_years; // Longest Friendship
    public ScoreEntry[] friend_happy; // Maximum Happiness Score
    public ScoreEntry[] friend_angry_min; // Lowest Angriness Score
    public ScoreEntry[] friend_angry_max; // Maximum Angriness Score
    public ScoreEntry[] friend_max; // Maximum Concurrent Friendships
    public ScoreEntry[] money_spending; // Maximum Money Spent
    public ScoreEntry[] money_selling; // Maximum Money From Selling Gifts
    public ScoreEntry[] money_gifted; // Maximum Money Spent On Gifts
    public ScoreEntry[] num_sold; // Maximum Number Of Gifts Sold
    public ScoreEntry[] num_gifts; // Maximum Number Of Gifts Given
    public ScoreEntry[] num_lamps; // Maximum Number Of Lava Lamps Purchased
    public ScoreEntry[] num_req; // Maximum Number Friend Requests Accepted
    public ScoreEntry[] num_lost; // Maximum Number Of Friends Lost
  }

  [System.Serializable]
  public struct ScoreEntry
  {
    public string user;
    public string str;
    public int integer;
    public float real;
  }

}

public enum Upgrades
{
  NONE,
  BELT_SPEED,
  FRIEND_MONEY,
  FRIEND_INFO,
  FASTER_PLAYER,
  LONGER_TURN,
  MORE_FRIENDS,
  EXTRA_SWAP,
  EXTRA_CHECKOUT,
  LAVA_LAMP,
  ENDGAME
}
