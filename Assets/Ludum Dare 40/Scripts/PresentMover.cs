using UnityEngine;
using System.Collections.Generic;

public class PresentMover : MonoBehaviour, IInteracatble, IInventory
{

  // Referances:
  public GameObject prefabPresent;
  public Transform positionSpawn;
  public Transform positionInteraction;

  // Configuration:
  public Vector3 moveSpeed = new Vector3(1.0f, 0, 0);
  public float despawnX = -14.75f;
  public float spawnMin = 1.0f;
  public float spawnMax = 4.0f;

  // State:
  private float spawnTimer;
  private readonly List<Transform> presents = new List<Transform>();

  // Lazy:
  private static PresentMover instance;

  // Messages:

  void Awake()
  {
    instance = this;
    enabled = false;
  }

  void Update()
  {
    Vector3 interactionPos = positionInteraction.position;
    interactionPos.x = PlayerController.Get.transform.position.x;
    positionInteraction.position = interactionPos;
    if(StoreReferances.instance.storeTimerLeft > 0 ||
          (GameStateManager.State.currentYear == 1 && GameStateManager.FriendToBuy.Count > 0))
    {
      spawnTimer -= Time.deltaTime;
      if(spawnTimer < 0)
      {
        spawnTimer = Random.Range(spawnMin, spawnMax);
        GameObject go = Instantiate<GameObject>(prefabPresent, transform);
        HitchLib.ColorEnum[] presentColors = GameStateManager.PresentColors;
        Present present = go.GetComponent<Present>();
        present.price = Random.Range(0, 1 + ((int)GameStateManager.State.currentYear / 2)) + 1;
        present.color = presentColors[Random.Range(0, presentColors.Length)];
        go.GetComponent<InteractionPickup>().mount = this;
        go.transform.position = positionSpawn.position;
        presents.Add(go.transform);
        go.SetActive(true);
      }
    }

    if(StoreReferances.instance.storeTimerLeft > -20)
    {
      Vector3 movement = Time.deltaTime * moveSpeed;
      List<Transform> removeUs = new List<Transform>();
      foreach(Transform present in presents)
      {
        present.position = present.position + movement;
        if(present.position.x > despawnX)
        {
          if(present.GetComponent<Present>() != null)
          {
            Destroy(present.gameObject);
            removeUs.Add(present);
          }
          else
          {
            present.position = positionSpawn.position;
          }
        }
      }
      foreach(Transform r in removeUs)
      {
        presents.Remove(r);
      }
    }
  }

  // Utilities:

  public static void On()
  {
    instance.enabled = true;
    GameStateManager.GeneratePresentColors();
  }

  public static void Off()
  {
    instance.enabled = false;
    foreach(Transform present in instance.presents)
    {
      Destroy(present.gameObject);
    }
    instance.presents.Clear();
  }

  // Interface IInteracatble:

  public InteractionType CanInteract(Interactor actor)
  {
    if(actor.GetItem() != null)
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
    item.SetParent(transform);
    Vector3 pos = item.position;
    pos.y = positionSpawn.position.y;
    pos.z = positionSpawn.position.z;
    item.position = pos;
    InteractionPickup pickup = item.GetComponent<InteractionPickup>();
    if(pickup != null)
    {
      pickup.mount = this;
    }
    presents.Add(item);
  }

  public void RemoveItem(Transform item)
  {
    presents.Remove(item);
  }

}
