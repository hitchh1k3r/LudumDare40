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

  InteractionType CanInteract(Interactor actor);
  void Interact(InteractionType type, Interactor actor);

}

[System.Flags]
public enum InteractionType
{
  GRAB
}
