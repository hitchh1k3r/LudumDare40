using UnityEngine;

public class SimpleAnimation : MonoBehaviour
{

  // Referances:
  public SpriteRenderer sprite;
  public Sprite[] frames;

  // Configuration:
  public float frameRate = 1.0f;

  // State:
  private float animationTimer;
  private int frame;

  // Messages:

  void Update()
  {
    animationTimer -= Time.deltaTime;
    if(animationTimer < 0)
    {
      ++frame;
      if(frame >= frames.Length)
      {
        frame = 0;
      }
      sprite.sprite = frames[frame];
      animationTimer = 1.0f / frameRate;
    }
  }

}
