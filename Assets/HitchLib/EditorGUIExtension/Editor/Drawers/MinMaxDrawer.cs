// Based on MinMaxAttribute by LotteMakesStuff
// https://gist.github.com/LotteMakesStuff/0de9be35044bab97cbe79b9ced695585

using UnityEngine;
using UnityEditor;

namespace HitchLib
{

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  [CustomPropertyDrawer(typeof(MinMaxAttribute))]
  public class MinMaxDrawer : PropertyDrawer
  {

    // START PropertyDrawer

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      if(property.propertyType == SerializedPropertyType.Vector2)
      {
        MinMaxAttribute minMax = (MinMaxAttribute)attribute;

        float minValue = property.vector2Value.x;
        float maxValue = property.vector2Value.y;
        float minLimit = minMax.minLimit;
        float maxLimit = minMax.maxLimit;

        Rect positionLable = new Rect(position.x, position.y, EditorGUIUtility.labelWidth,
              position.height);
        position = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, position.width -
              EditorGUIUtility.labelWidth, position.height);
        Rect positionLeft = new Rect(position.x, position.y, 30, position.height);
        Rect positionMid = new Rect(position.x + 35, position.y, position.width - 70,
              position.height);
        Rect positionRight = new Rect(position.x + position.width - 30, position.y, 30,
              position.height);

        EditorGUI.LabelField(positionLable, label);
        minValue = EditorGUI.FloatField(positionLeft, minValue);
        EditorGUI.MinMaxSlider(positionMid, ref minValue, ref maxValue, minLimit, maxLimit);
        maxValue = EditorGUI.FloatField(positionRight, maxValue);

        if(minValue < minLimit)
        {
          minValue = minLimit;
        }
        if(minValue > maxLimit)
        {
          minValue = maxLimit;
        }
        if(maxValue < minLimit)
        {
          maxValue = minLimit;
        }
        if(maxValue > maxLimit)
        {
          maxValue = maxLimit;
        }

        property.vector2Value = new Vector2(minValue, maxValue);
      }
    }

    // END PropertyDrawer

  }

}
