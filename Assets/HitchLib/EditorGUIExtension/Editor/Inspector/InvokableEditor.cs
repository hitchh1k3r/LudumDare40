using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace HitchLib
{

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  [CanEditMultipleObjects]
  [CustomEditor(typeof(MonoBehaviour), true)]
  public class InvokableEditor : Editor
  {

    // START Editor

    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();

      System.Type type = target.GetType();

      foreach(var method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public |
            BindingFlags.Instance))
      {
        var attributes = method.GetCustomAttributes(typeof(InvokableAttribute), true);
        if(attributes.Length > 0)
        {
          if(GUILayout.Button(method.Name))
          {
            method.Invoke(target, new object [0]);
          }
        }
      }
    }

    // END Editor

  }

}
