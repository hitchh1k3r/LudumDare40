using UnityEngine;

public class InteractionPickup : MonoBehaviour, IInteracatble
{

  // Configuration:
  public Transform interactionPoint;

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
