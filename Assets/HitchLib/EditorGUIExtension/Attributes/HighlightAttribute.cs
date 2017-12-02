// Based on HighlightAttribute by LotteMakesStuff
// https://gist.github.com/LotteMakesStuff/2d3c6dc7a913ed118601db95735574de

using UnityEngine;

namespace HitchLib
{

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  public class HighlightAttribute : PropertyAttribute
  {

    public ColorEnum color;
    public string validateMethod;
    public object value;

    public HighlightAttribute(ColorEnum color = ColorEnum.YELLOW, string validateMethod = null,
          object value = null)
    {
      this.color = color;
      this.validateMethod = validateMethod;
      this.value = value;
    }

  }

}
