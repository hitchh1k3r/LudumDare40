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

  // State:
  private bool lastMenuState;

  // Messages:

  void Awake()
  {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update()
  {
    if(lastMenuState != GameStateManager.IsMenu)
    {
      lastMenuState = GameStateManager.IsMenu;
      if(GameStateManager.IsMenu)
      {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
      }
      else
      {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
      }
    }
    if(!GameStateManager.IsMenu)
    {
      Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
      if(Input.GetMouseButton(0) && screenRect.Contains(Input.mousePosition))
      {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
      }
      else if(Input.GetButton("Cancel"))
      {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
      }
      if(Cursor.lockState == CursorLockMode.Locked)
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
      transform.position = Vector3.Lerp(transform.position, target.position + targetOffset,
            HitchLib.Math.HalfLifeInterp(1.0f / camSpeed, Time.deltaTime));
    }
  }

}
