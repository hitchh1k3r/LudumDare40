using UnityEngine;
using System.Collections;

public class Interactor : MonoBehaviour, IInventory
{

  // Configuration:
  public float range;
  public GameObject prefabInventory;
  public LayerMask layerInteration;
  public Transform itemMount;
  public PlayerController player;

  // Cache:
  private GameObject indicatorInventory;
  private IInteracatble targetInventory;
  private InteractionAttachment attachInventory;
  private bool showingInventory;

  // State:
  private Coroutine animateInventory;
  private Transform itemHeld;

  // Messages:

  void Awake()
  {
    indicatorInventory = Instantiate<GameObject>(prefabInventory);
    attachInventory = indicatorInventory.GetComponent<InteractionAttachment>();
    attachInventory.enabled = false;
    indicatorInventory.SetActive(false);
  }

  void Update()
  {
    float distanceInventory = float.PositiveInfinity;
    Plane forwardPlane = new Plane(transform.forward, transform.position);
    IInteracatble nearestInventory = null;
    Collider[] objects = Physics.OverlapSphere(transform.position, range, layerInteration);
    foreach(Collider obj in objects)
    {
      IInteracatble[] interacables = obj.GetComponents<IInteracatble>();
      foreach(IInteracatble inter in interacables)
      {
        InteractionType valid = inter.CanInteract(this);
        if(valid != InteractionType.None && forwardPlane.GetSide(obj.transform.position))
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
                  attachInventory.enabled = false;
                  attachInventory.target = null;
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
          Vector3 startPos = indicatorInventory.transform.position;
          Transform target = targetInventory.InteractIconPosition(InteractionType.Inventory, this);
          attachInventory.enabled = false;
          attachInventory.target = target;
          animateInventory = StartCoroutine(HitchLib.Tweening.Action((t) => {
                  indicatorInventory.transform.position =
                        Vector3.Lerp(startPos, target.position, t);
                }, 0.25f, HitchLib.Easing.EASE_CUBIC_IN_OUT, callbackEnding: () => {
                    attachInventory.enabled = true;
                }));
        }
        else
        {
          if(animateInventory != null)
          {
            StopCoroutine(animateInventory);
          }
          indicatorInventory.SetActive(true);
          attachInventory.enabled = true;
          attachInventory.target = targetInventory.InteractIconPosition(InteractionType.Inventory,
                this);
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

  // Interface IInventory:

  public void AddItem(Transform item)
  {
    player.SetHolding(true);
    itemHeld = item;
    itemHeld.SetParent(itemMount, false);
    itemHeld.localPosition = Vector3.zero;
  }

  public void RemoveItem(Transform item)
  {
    player.SetHolding(false);
    itemHeld = null;
  }

  // Utilities:

  public Transform GetItem()
  {
    return itemHeld;
  }

}

public interface IInteracatble
{

  InteractionType CanInteract(Interactor actor);
  Transform InteractIconPosition(InteractionType type, Interactor actor);
  void Interact(InteractionType type, Interactor actor);

}

public interface IInventory
{

  void AddItem(Transform item);
  void RemoveItem(Transform item);

}

[System.Flags]
public enum InteractionType
{
  None      = 0,
  Inventory = 1
}
