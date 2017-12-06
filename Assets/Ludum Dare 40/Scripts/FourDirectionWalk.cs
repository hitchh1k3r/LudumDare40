using UnityEngine;

public class FourDirectionWalk : MonoBehaviour
{

  // Refernaces:
  public SpriteRenderer sprite;
  public WalkingAnimation animation;

  // Public State:
  public float speed;

  // Cache:
  Transform cam;

  // State:
  private int frame;
  private float animationTimer;

  // Messages:

  void Awake()
  {
    cam = Camera.main.transform;
  }

  void Update()
  {
    float angle = transform.rotation.eulerAngles.y - cam.rotation.eulerAngles.y;
    if(angle < 0)
    {
      angle = 360 - (-angle % 360.0f);
    }
    else
    {
      angle %= 360.0f;
    }
    int face = 0;
    if(angle > 315)
    {
      face = 0;
    }
    else if(angle > 225)
    {
      face = 3;
    }
    else if(angle > 135)
    {
      face = 2;
    }
    else if(angle > 45)
    {
      face = 1;
    }

    if(speed > 0.01f)
    {
      animationTimer += speed * Time.deltaTime;
      if(animationTimer > 1)
      {
        animationTimer = 0;
        ++frame;
        if(frame >= 2)
        {
          frame = 0;
        }
      }
    }

    sprite.sprite = animation.GetSprite(face, speed, frame);
  }

}
