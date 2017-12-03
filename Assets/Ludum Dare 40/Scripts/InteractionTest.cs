using UnityEngine;

public class InteractionTest : MonoBehaviour, IInteracatble
{

  // Configuration:
  public Transform interactionPoint;

  // State:
  private bool didShow;

  // Interface IInteracatble:

  public InteractionType CanInteract(Interactor actor)
  {
    return InteractionType.Inventory;
  }

  public Vector3 InteractIconPosition(InteractionType type, Interactor actor)
  {
    return interactionPoint.position;
  }

  public void Interact(InteractionType type, Interactor actor)
  {
    if(!didShow)
    {
      didShow = true;
      InterfaceManager.ShowFriendBay();
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
