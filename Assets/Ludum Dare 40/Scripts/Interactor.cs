using UnityEngine;
using System.Collections;

public class Interactor : MonoBehaviour
{

  // Configuration:
  public float range;
  public GameObject prefabInventory;
  public LayerMask layerInteration;

  // Cache:
  private GameObject indicatorInventory;
  private IInteracatble targetInventory;
  private bool showingInventory;

  // State:
  private Coroutine animateInventory;

  // Messages:

  void Awake()
  {
    indicatorInventory = Instantiate<GameObject>(prefabInventory);
    indicatorInventory.SetActive(false);
  }

  void Update()
  {
    float distanceInventory = float.PositiveInfinity;
    IInteracatble nearestInventory = null;
    Collider[] objects = Physics.OverlapSphere(transform.position, range, layerInteration);
    foreach(Collider obj in objects)
    {
      IInteracatble[] interacables = obj.GetComponents<IInteracatble>();
      foreach(IInteracatble inter in interacables)
      {
        InteractionType valid = inter.CanInteract(this);
        if(valid != InteractionType.None)
        {
          if((valid & InteractionType.Inventory) == InteractionType.Inventory)
          {
            float dist = (obj.transform.position - transform.position).sqrMagnitude;
            if(dist < distanceInventory)
            {
              distanceInventory = dist;
              nearestInventory = inter;
            }
          }
        }
      }
    }

    if(nearestInventory != targetInventory)
    {
      targetInventory = nearestInventory;
      if(targetInventory == null)
      {
        if(animateInventory != null)
        {
          StopCoroutine(animateInventory);
        }
        showingInventory = false;
        animateInventory = StartCoroutine(HitchLib.Tweening.EasyPopOut(
              indicatorInventory.transform, 0.25f, callbackEnding: () => {
                  indicatorInventory.SetActive(false);
                }));
      }
      else
      {
        if(showingInventory)
        {
          if(animateInventory != null)
          {
            StopCoroutine(animateInventory);
          }
          animateInventory = StartCoroutine(HitchLib.Tweening.TransTranslate(
                indicatorInventory.transform, null, targetInventory.InteractIconPosition(
                InteractionType.Inventory, this), 0.25f, HitchLib.Easing.EASE_CUBIC_IN_OUT));
        }
        else
        {
          if(animateInventory != null)
          {
            StopCoroutine(animateInventory);
          }
          indicatorInventory.SetActive(true);
          indicatorInventory.transform.position = targetInventory.InteractIconPosition(
                InteractionType.Inventory, this);
          animateInventory = StartCoroutine(HitchLib.Tweening.EasyPopIn(
                indicatorInventory.transform, 0.5f, callbackEnding: () => {
                    showingInventory = true;
                  }));
        }
      }
    }

    if(Input.GetButtonDown("Grab") && targetInventory != null)
    {
      targetInventory.Interact(InteractionType.Inventory, this);
    }
  }

}

public interface IInteracatble
{

  InteractionType CanInteract(Interactor actor);
  Vector3 InteractIconPosition(InteractionType type, Interactor actor);
  void Interact(InteractionType type, Interactor actor);

}

[System.Flags]
public enum InteractionType
{
  None      = 0,
  Inventory = 1
}
