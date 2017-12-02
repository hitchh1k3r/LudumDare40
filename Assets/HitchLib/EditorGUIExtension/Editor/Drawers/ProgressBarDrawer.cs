// Based on InspectorButtonsTest by LotteMakesStuff
// https://gist.github.com/LotteMakesStuff/dd785ff49b2a5048bb60333a6a125187

using UnityEngine;
using UnityEditor;

namespace HitchLib
{

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  [CustomPropertyDrawer(typeof(ProgressBarAttribute))]
  public class ProgressBarDrawer : PropertyDrawer
  {

    // START PropertyDrawer

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
      if(((ProgressBarAttribute)attribute).hideWhenZero && property.floatValue <= 0)
        return 0;

      return base.GetPropertyHeight(property, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      if(property.propertyType != SerializedPropertyType.Float)
      {
        GUI.Label(position, "ERROR: can only apply progress bar onto a float");
        return;
      }

      ProgressBarAttribute progressBar = (ProgressBarAttribute)attribute;
      if(progressBar.hideWhenZero && property.floatValue <= 0)
        return;

      var dynamicLabel = property.serializedObject.FindProperty(progressBar.label);

      EditorGUI.ProgressBar(position, property.floatValue/1f, dynamicLabel == null ? property.name :
            dynamicLabel.stringValue);
    }

    // END PropertyDrawer

  }

}


