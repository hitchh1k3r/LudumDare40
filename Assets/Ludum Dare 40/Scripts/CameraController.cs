using UnityEngine;

public class CameraController : MonoBehaviour
{

  // Referances:
  public Vector3 targetOffset = new Vector3(0, 1, 0);
  public Transform target;

  // Configuration:
  public float camSpeed = 4.0f;
  public float pitchSpeed = 1;
  public float yawSpeed = 1;
  public float pitch = 40;
  public float yaw = 45;
  public float roll = 0;
  [HitchLib.MinMaxAttribute(0, 90)]
  public Vector2 pitchLimits = new Vector2(10, 80);

  // Static Instance:
  private static CameraController instance;

  // Messages:

  void Awake()
  {
    instance = this;
  }

  void Update()
  {
    if(!GameStateManager.IsMenu && GameStateManager.HasFocus)
    {
      yaw += yawSpeed * Input.GetAxisRaw("Mouse X");
      pitch += pitchSpeed * Input.GetAxisRaw("Mouse Y");
      if(pitch < pitchLimits.x)
      {
        pitch = pitchLimits.x;
      }
      if(pitch > pitchLimits.y)
      {
        pitch = pitchLimits.y;
      }
      transform.eulerAngles = new Vector3(pitch, yaw, roll);
    }
    if((target.position - transform.position).sqrMagnitude > 500.0f)
    {
      transform.position = target.position + targetOffset;
    }
    else
    {
      transform.position = Vector3.Lerp(transform.position, target.position + targetOffset,
            HitchLib.Math.HalfLifeInterp(1.0f / camSpeed, Time.deltaTime));
    }
  }

  // Utilities:

  public static void SetAngle(float yaw, float pitch)
  {
    instance.pitch = pitch;
    instance.yaw = yaw;
  }

}
