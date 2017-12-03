using UnityEngine;

public class ConveyorAnimation : MonoBehaviour
{

  // Referances:
  public WallRenderer[] sides;
  public WallRenderer topForward;
  public WallRenderer topBackwards;
  public Sprite frame1;
  public Sprite frame2;

  // Configuration:
  public float moveSpeed = 0.25f;

  // State:
  private float timer;
  private int frame;
  private float scroller;

  // Messages:

  void Update()
  {
    timer += Random.Range(0.75f, 1.25f) * Time.deltaTime;
    if(timer > 0.25f)
    {
      timer = 0;
      ++frame;
      if(frame >= 2)
      {
        frame = 0;
      }
      if(frame == 0)
      {
        foreach(WallRenderer side in sides)
        {
          side.spriteTexture = frame1;
        }
      }
      else
      {
        foreach(WallRenderer side in sides)
        {
          side.spriteTexture = frame2;
        }
      }
    }
    scroller += moveSpeed * Time.deltaTime;
    if(scroller > 0.25f)
    {
      scroller = 0;
    }
    topForward.xOffset = scroller;
    topBackwards.xOffset = 0.25f - scroller;
  }

}
