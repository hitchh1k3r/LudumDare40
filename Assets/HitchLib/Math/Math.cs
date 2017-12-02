using UnityEngine;

namespace HitchLib
{

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  public static class Math
  {

    // Transformation Methods:

    public static float Map(float value, float inSideA, float inSideB, float outSideA = 0,
          float outSideB = 1, bool clamp = true)
    {
      // Map transforms a value from one range to another
      float t = (value - inSideA) / (inSideB - inSideA);
      if(clamp)
      {
        if(t < 0)
        {
          t = 0;
        }
        if(t > 1)
        {
          t = 1;
        }
      }
      return outSideA + (t * (outSideB - outSideA));
    }

    // Interpolation Methods:

    public static float HalfLifeInterp(float halfLife, float deltaTime)
    {
      // HalfLifeInterp returns the linear interpolation factor for half life decay.
      return 1 - Mathf.Pow(0.5f, deltaTime / halfLife);
    }

  }

}
