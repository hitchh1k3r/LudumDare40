using UnityEngine;

public class InteractionTest : MonoBehaviour, IInteracatble
{

  // State:
  private bool didShow;

  // Interface IInteracatble:

  public InteractionType CanInteract(Interactor actor)
  {
    return InteractionType.Inventory;
  }

  public Vector3 InteractIconPosition(InteractionType type, Interactor actor)
  {
    return transform.position + new Vector3(0, 2, 0);
  }

  public void Interact(InteractionType type, Interactor actor)
  {
    if(!didShow)
    {
      didShow = true;
      InterfaceManager.ShowFriendBay();
    }
  }

}
