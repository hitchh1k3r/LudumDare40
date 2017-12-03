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
    return InteractionType.Inventory;
  }

  public Transform InteractIconPosition(InteractionType type, Interactor actor)
  {
    return interactionPoint;
  }

  public void Interact(InteractionType type, Interactor actor)
  {
    DoorSwish.StartSwish(location);
    CameraController.SetAngle(-30, 40);
    actor.GetComponent<PlayerController>().Teleport(tpName, teleportTo.position);
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
