using UnityEngine;
using System.Collections;

public class Interactor : MonoBehaviour
{

  // Configuration:
  public float range;
  public Sprite grabIndicator;

}

public interface IInteracatble
{

  public InteractionType CanInteract(Interactor actor);
  public void Interact(InteractionType type, Interactor actor);

}


