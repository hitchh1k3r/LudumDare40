// Based on InspectorButtonsTest by LotteMakesStuff
// https://gist.github.com/LotteMakesStuff/dd785ff49b2a5048bb60333a6a125187

using UnityEngine;
using System;

namespace HitchLib
{

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
  public class MessageButtonAttribute : PropertyAttribute
  {

    public string buttonLabel;
    public string methodName;

    public bool isActiveAtRuntime = true;
    public bool isActiveInEditor = true;

    public MessageButtonAttribute(string buttonLabel, string methodName, int order = 1)
    {
      this.buttonLabel = buttonLabel;
      this.methodName = methodName;
      this.order = order;
    }

  }

}
