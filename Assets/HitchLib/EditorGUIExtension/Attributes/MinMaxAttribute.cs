// Based on MinMaxAttribute by LotteMakesStuff
// https://gist.github.com/LotteMakesStuff/0de9be35044bab97cbe79b9ced695585

using UnityEngine;

namespace HitchLib
{

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  public class MinMaxAttribute : PropertyAttribute
  {

    public float minLimit;
    public float maxLimit;

    public MinMaxAttribute(float minLimit = 0, float maxLimit = 1)
    {
      this.minLimit = minLimit;
      this.maxLimit = maxLimit;
    }

  }

}
