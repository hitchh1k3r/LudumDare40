// Based on HighlightAttribute by LotteMakesStuff
// https://gist.github.com/LotteMakesStuff/2d3c6dc7a913ed118601db95735574de

using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

namespace HitchLib
{

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  [CustomPropertyDrawer(typeof(HighlightAttribute))]
  public class HighlightDrawer : PropertyDrawer
  {

    // START PropertyDrawer

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
      return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      HighlightAttribute highlightAttribute = (HighlightAttribute)attribute;

      bool doHighlight = true;

      if(!string.IsNullOrEmpty(highlightAttribute.validateMethod))
      {
        Type t = property.serializedObject.targetObject.GetType();
        MethodInfo m = t.GetMethod(highlightAttribute.validateMethod, BindingFlags.Instance |
              BindingFlags.NonPublic | BindingFlags.Public);

        if(m != null)
        {
          if(highlightAttribute.value != null)
          {
            doHighlight = (bool)m.Invoke(property.serializedObject.targetObject,
                  new object[] { highlightAttribute.value });
          }
          else
          {
            doHighlight = (bool)m.Invoke(property.serializedObject.targetObject, new object[] {});
          }
        }
        else
        {
          Debug.LogError("Invalid Validate function: " + highlightAttribute.validateMethod,
                property.serializedObject.targetObject);
        }
      }

      if(doHighlight)
      {
        Color colorHighlight = Colors.FromEnum(highlightAttribute.color);

        float padding = EditorGUIUtility.standardVerticalSpacing;
        Rect positionHighlight = new Rect(position.x - padding, position.y - padding,
          position.width + (padding * 2), position.height + (padding * 2));

        EditorGUI.DrawRect(positionHighlight, colorHighlight);

        Color colorRestore = GUI.contentColor;

        GUI.contentColor = Color.black;
        EditorGUI.PropertyField(position, property, label);

        GUI.contentColor = colorRestore;
      }
      else
      {
        EditorGUI.PropertyField(position, property, label);
      }
    }

    // END PropertyDrawer

  }

}
