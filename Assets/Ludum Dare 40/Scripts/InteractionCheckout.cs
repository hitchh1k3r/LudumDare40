using UnityEngine;
using TMPro;

public class InteractionCheckout : MonoBehaviour, IInteracatble
{

  // Referances:
  public Transform positionInteraction;
  public FourDirectionWalk person;
  public FourDirectionWalk shirt;
  public WalkingAnimation male;
  public WalkingAnimation female;
  public WalkingAnimation male_shirt;
  public WalkingAnimation female_shirt;
  public TextMeshPro name;
  public int index;

  public static InteractionCheckout[] instances = new InteractionCheckout[3];

  // State:
  [System.NonSerialized]
  public FriendData friend = null;

  // Messages:

  void Awake()
  {
    instances[index] = this;
  }

  // Interface IInteracatble:

  public InteractionType CanInteract(Interactor actor)
  {
    if(actor.GetItem() != null && friend != null)
    {
      return InteractionType.Inventory;
    }
    else
    {
      return InteractionType.None;
    }
  }

  public Transform InteractIconPosition(InteractionType type, Interactor actor)
  {
    return positionInteraction;
  }

  public void Interact(InteractionType type, Interactor actor)
  {
    Transform item = actor.GetItem();
    Present present = item.GetComponent<Present>();
    if(present == null || GameStateManager.State.currentMoney >= present.price)
    {
      if(present != null)
      {
        GameStateManager.Collector.moneyGiftedAway += present.price;
        ++GameStateManager.Collector.numberGiftsPurchased;
        GameStateManager.Collector.moneySpent += present.price;
        GameStateManager.State.currentMoney -= present.price;
      }
      actor.RemoveItem(item);
      friend.GivePresent(item);
      Destroy(item.gameObject);

      if(GameStateManager.FriendQueue.Count > 0)
      {
        SetFriend(GameStateManager.FriendQueue[Random.Range(0,
              GameStateManager.FriendQueue.Count)]);
      }
      else
      {
        SetFriend(null);
      }
    }
  }

  public void SetFriend(FriendData friend)
  {
    GameStateManager.FriendQueue.Remove(friend);
    this.friend = friend;
    if(friend != null)
    {
      if(friend.isFemale)
      {
        person.animation = female;
        shirt.animation = female_shirt;
      }
      else
      {
        person.animation = male;
        shirt.animation = male_shirt;
      }
      shirt.GetComponentInChildren<SpriteRenderer>().color = HitchLib.Colors.FromEnum(friend.color);
      name.text = friend.name;
      name.color = HitchLib.Colors.FromEnum(friend.color);
      person.gameObject.SetActive(true);
      shirt.gameObject.SetActive(true);
      name.gameObject.SetActive(true);
    }
    else
    {
      person.gameObject.SetActive(false);
      shirt.gameObject.SetActive(false);
      name.gameObject.SetActive(false);
    }
  }

}
