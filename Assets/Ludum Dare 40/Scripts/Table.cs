using UnityEngine;

public class Table : MonoBehaviour, IInteracatble, IInventory
{

  // Referances:
  public Transform itemMount;
  public Transform positionInteraction;

  // State:
  private Transform itemHeld;

  // Interface IInteracatble:

  public InteractionType CanInteract(Interactor actor)
  {
    if(itemHeld == null && actor.GetItem() != null)
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
    actor.RemoveItem(item);
    AddItem(item);
  }

  // Interface IInventory:

  public void AddItem(Transform item)
  {
    itemHeld = item;
    itemHeld.SetParent(itemMount, false);
    itemHeld.localPosition = Vector3.zero;
    InteractionPickup pickup = itemHeld.GetComponent<InteractionPickup>();
    if(pickup != null)
    {
      pickup.mount = this;
    }
  }

  public void RemoveItem(Transform item)
  {
    itemHeld = null;
  }

}
