// Based on InspectorButtonsTest by LotteMakesStuff
// https://gist.github.com/LotteMakesStuff/dd785ff49b2a5048bb60333a6a125187

using UnityEngine;
using UnityEditor;

namespace HitchLib
{

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  [CustomPropertyDrawer(typeof(MessageButtonAttribute))]
  public class MessageButtonDrawer : DecoratorDrawer
  {

    // START PropertyDrawer

    public override float GetHeight()
    {
      return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing*2;
    }

    public override void OnGUI(Rect position)
    {
      MessageButtonAttribute buttonAttribute = (MessageButtonAttribute)attribute;

      if (EditorApplication.isPlaying && !buttonAttribute.isActiveAtRuntime)
      {
        GUI.enabled = false;
      }
      if (!EditorApplication.isPlaying && !buttonAttribute.isActiveInEditor)
      {
        GUI.enabled = false;
      }

      position = new Rect(position.x, position.y, position.width, position.height -
            EditorGUIUtility.standardVerticalSpacing);

      if (GUI.Button(position, buttonAttribute.buttonLabel))
      {
        Selection.activeGameObject.BroadcastMessage(buttonAttribute.methodName);
      }

      GUI.enabled = true;
    }

    // END PropertyDrawer

  }

}
