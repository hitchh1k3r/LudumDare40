using UnityEngine;

[CreateAssetMenu(fileName = "player_walk.asset", menuName = "Custom/Walk Animation")]
public class WalkingAnimation : ScriptableObject
{

  public Sprite downStill;
  public Sprite downFrame1;
  public Sprite downFrame2;

  public Sprite leftStill;
  public Sprite leftFrame1;
  public Sprite leftFrame2;

  public Sprite rightStill;
  public Sprite rightFrame1;
  public Sprite rightFrame2;

  public Sprite upStill;
  public Sprite upFrame1;
  public Sprite upFrame2;

  // Utilities:

  public Sprite GetSprite(int face, float speed, int frame)
  {
    if(face == 0)
    { // UP
      if(speed > 0.01f)
      {
        if(frame == 0)
          return upFrame1;
        else
          return upFrame2;
      }
      return upStill;
    }
    else if(face == 1)
    { // RIGHT
      if(speed > 0.01f)
      {
        if(frame == 0)
          return rightFrame1;
        else
          return rightFrame2;
      }
      return rightStill;
    }
    else if(face == 2)
    { // DOWN
      if(speed > 0.01f)
      {
        if(frame == 0)
          return downFrame1;
        else
          return downFrame2;
      }
      return downStill;
    }
    else
    { // LEFT
      if(speed > 0.01f)
      {
        if(frame == 0)
          return leftFrame1;
        else
          return leftFrame2;
      }
      return leftStill;
    }
    return downStill;
  }

}
