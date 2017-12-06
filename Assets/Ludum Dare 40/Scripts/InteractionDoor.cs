using UnityEngine;

public class InteractionDoor : MonoBehaviour, IInteracatble
{

  // Configuration:
  public Transform interactionPoint;
  public string tpName;
  public Transform teleportTo;
  public DoorSwish.DoorLocation location;

  // Interface IInteracatble:

  public InteractionType CanInteract(Interactor actor)
  {
    if(GameStateManager.State.currentYear != 2 || GameStateManager.FriendRequests.Count <= 0)
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
    return interactionPoint;
  }

  public void Interact(InteractionType type, Interactor actor)
  {
    DoorSwish.StartSwish(location);
    CameraController.SetAngle(-30, 40);
    actor.GetComponent<PlayerController>().Teleport(tpName, teleportTo.position, Vector3.zero);
    if(location == DoorSwish.DoorLocation.Store)
    {
      PresentMover.On();
      GameStateManager.TryAddingCachiers();
      StoreReferances.instance.storeTimerOn = true;
    }
    else
    {
      PresentMover.Off();
    }
  }

#if UNITY_EDITOR
  void OnDrawGizmosSelected()
  {
    if(interactionPoint != null)
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(interactionPoint.position, 0.1f);
    }
  }
#endif

}
