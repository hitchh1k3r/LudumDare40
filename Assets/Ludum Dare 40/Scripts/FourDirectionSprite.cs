using UnityEngine;

public class FourDirectionSprite : MonoBehaviour
{

  // Refernaces:
  public SpriteRenderer sprite;
  public Sprite down;
  public Sprite left;
  public Sprite right;
  public Sprite up;

  // Cache:
  Transform cam;

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

    if(face == 0)
    {
      sprite.sprite = up;
    }
    else if(face == 1)
    {
      sprite.sprite = right;
    }
    else if(face == 2)
    {
      sprite.sprite = down;
    }
    else if(face == 3)
    {
      sprite.sprite = left;
    }
  }

}
