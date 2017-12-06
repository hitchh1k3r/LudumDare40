using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PlayerController : MonoBehaviour
{

  // Configuration:
  public float moveSpeed = 1.5f;
  public float spinSpeed = 640.0f;
  public string startingPlane = "PowerPlant";
  public FourDirectionWalk animator;
  public WalkingAnimation animationNormal;
  public WalkingAnimation animationHolding;
  public Transform positionInteraction;

  // Cache:
  private Transform cam;
  private float offsetYInteraction;

  // State:
  private NavigationPlane navPlane;
  private Quaternion targetRotation = Quaternion.identity;

  // Lazy Accessor:
  private static PlayerController instance;
  public static PlayerController Get {
    get { return instance; }
  }

  // Messages:

  void Awake()
  {
    instance = this;
    offsetYInteraction = positionInteraction.localPosition.y;
  }

  void Start()
  {
    cam = Camera.main.transform;
    navPlane = NavigationPlane.FindPlane(startingPlane);
  }

  void Update()
  {
    float speed = 0;
    if(!GameStateManager.IsMenu && GameStateManager.HasFocus)
    {
      Vector3 oldPos = transform.localPosition;
      Quaternion cameraForward = Quaternion.Euler(0, cam.eulerAngles.y, 0);
      Vector3 moveVector = cameraForward * new Vector3(Input.GetAxis("Horizontal"), 0,
            Input.GetAxis("Vertical"));
      if(moveVector.sqrMagnitude > 0.01f)
      {
        speed = moveVector.magnitude;
        if(speed > 1)
        {
          moveVector /= speed;
          speed = 1;
        }
        float accSpeed = moveSpeed;
        if(GameStateManager.State.currentGameUpgrade > 3)
        {
          accSpeed *= 1.5f;
        }
        Vector3 newPos = transform.localPosition + Time.deltaTime * accSpeed * moveVector;
        NavigationPlane.ValidMove(ref newPos, ref navPlane);
        transform.localPosition = newPos;
        targetRotation = Quaternion.LookRotation(moveVector);
      }
      transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation,
            spinSpeed * Time.deltaTime);
    }
    animator.speed = 10.0f * speed;
    Vector3 pos = positionInteraction.localPosition;
    pos.y = offsetYInteraction * (1.0f / cam.up.y);
    positionInteraction.localPosition = pos;
  }

  // Utilities:

  public void SetHolding(bool isHolding)
  {
    if(isHolding)
    {
      animator.animation = animationHolding;
    }
    else
    {
      animator.animation = animationNormal;
    }
  }

  public void Teleport(string plane, Vector3 pos, Vector3? rotation)
  {
    Vector3 rot = rotation ?? transform.eulerAngles;
    pos.y = 0;
    navPlane = NavigationPlane.FindPlane(plane);
    NavigationPlane.ValidMove(ref pos, ref navPlane);
    transform.position = pos;
    transform.eulerAngles = rot;
    targetRotation = transform.rotation;
  }

}
