using UnityEngine;

public class LavaLamp : MonoBehaviour
{

  // Configuration:
  public SpriteRenderer sprite;
  public float animationSpeed = 0.3333333f;
  public float sat = 0.5f;
  public float val = 1.0f;

  // State:
  private float animationTimer;

  // Messages:

  void Update()
  {
    animationTimer += Time.deltaTime * animationSpeed;
    if(animationTimer > 1)
    {
      animationTimer = 0;
    }
    sprite.color = Color.HSVToRGB(animationTimer, sat, val);
  }

}
