using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buying : MonoBehaviour
{

  // Referances:
  public GameObject up1;
  public TextMeshProUGUI up1Text;
  public GameObject up2;
  public TextMeshProUGUI up2Text;

  // Cache:
  private Upgrades last1 = Upgrades.NONE;
  private Upgrades last2 = Upgrades.NONE;

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
    }  }

  // Utilities:

  private int GetPrice(Upgrades upgrade)
  {
    switch(upgrade)
    {
      case Upgrades.LAVA_LAMP:
        return 4;
      case Upgrades.BELT_SPEED:
        return 5;
      case Upgrades.FRIEND_INFO:
        return 5;
      case Upgrades.FASTER_PLAYER:
        return 5;
      case Upgrades.MORE_FRIENDS:
        return 5;
      case Upgrades.LONGER_TURN:
        return 10;
      case Upgrades.EXTRA_SWAP:
        return 10;
      case Upgrades.EXTRA_CHECKOUT:
        return 10;
      case Upgrades.FRIEND_MONEY:
        return 10;
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
        return "enhance your FriendSpace account, allowing you too see which friends gave you what presents";
    }
    return "something when wrong, you should not be seeing this";
  }

  private void ApplyUpgrade(Upgrades upgrade)
  {
    switch(upgrade)
    {
      case Upgrades.LAVA_LAMP:
        {
          ++GameStateManager.Collector.numberGiftsPurchased;
          ++GameStateManager.Collector.numberLavaLampsPurchased;
        } break;
      case Upgrades.BELT_SPEED:
        {
        } break;
      case Upgrades.FRIEND_INFO:
        {
        } break;
      case Upgrades.FASTER_PLAYER:
        {
        } break;
      case Upgrades.MORE_FRIENDS:
        {
        } break;
      case Upgrades.LONGER_TURN:
        {
        } break;
      case Upgrades.EXTRA_SWAP:
        {
        } break;
      case Upgrades.EXTRA_CHECKOUT:
        {
        } break;
      case Upgrades.FRIEND_MONEY:
        {
        } break;
    }
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
  LAVA_LAMP
}
