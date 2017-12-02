// Based on StatsBarAttribute by LotteMakesStuff
// https://gist.github.com/LotteMakesStuff/b8853a16de3e680dc1c326fe6f5ebd7e

using UnityEngine;

namespace HitchLib
{

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  public class StatBarAttribute : PropertyAttribute
  {

    public object valueMax;
    public ColorEnum color;

    public StatBarAttribute(object valueMax = null, ColorEnum color = ColorEnum.DARK_RED)
    {
      this.valueMax = valueMax;
      this.color = color;
    }

  }

}
