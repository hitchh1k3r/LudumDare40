// Based on StatsBarAttribute by LotteMakesStuff
// https://gist.github.com/LotteMakesStuff/b8853a16de3e680dc1c326fe6f5ebd7e

using UnityEngine;
using UnityEditor;

namespace HitchLib
{

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  [CustomPropertyDrawer(typeof(StatBarAttribute))]
  public class StatBarDrawer : PropertyDrawer
  {

    // START PropertyDrawer

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
      return (EditorGUIUtility.singleLineHeight * 2) + EditorGUIUtility.standardVerticalSpacing;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      StatBarAttribute statBar = (StatBarAttribute) attribute;
      float maxValue = -1;
      string typeError = "";
      if(statBar.valueMax != null)
      {
        if(statBar.valueMax is string)
        {
          SerializedProperty propertyMax = property.serializedObject.FindProperty(
                (string) statBar.valueMax);
          if(propertyMax == null)
          {
            typeError = "valueMax refernaces an invalid property!";
          }
          else if(propertyMax.propertyType == SerializedPropertyType.Float)
          {
            maxValue = propertyMax.floatValue;
            if(maxValue < 0)
            {
              typeError = "valueMax must be positive!";
            }
          }
          else if(propertyMax.propertyType == SerializedPropertyType.Integer)
          {
            maxValue = propertyMax.intValue;
            if(maxValue < 0)
            {
              typeError = "valueMax must be positive!";
            }
          }
          else
          {
            typeError = "valueMax must referance a number property!";
          }
        }
        else if(statBar.valueMax is float || statBar.valueMax is int || statBar.valueMax is double)
        {
          maxValue = (float)statBar.valueMax;
          if(maxValue < 0)
          {
            typeError = "valueMax must be positive!";
          }
        }
        else
        {
          typeError = "valueMax must be the name of a property or a number!";
        }
      }
      else
      {
        typeError = "unset";
      }

      float lineHight = EditorGUIUtility.singleLineHeight;
      float padding = EditorGUIUtility.standardVerticalSpacing;

      Rect positionBar = new Rect(position.position.x, position.position.y, position.size.x,
            lineHight);

      float precentFilled = 0;
      string labelBar = "";
      bool error = false;

      switch(property.propertyType)
      {
        case SerializedPropertyType.Integer:
        {
          if(maxValue < 0)
          {
            error = true;
            if(typeError == "unset")
            {
              labelBar = "you must provide a valueMax in the StatBarAttribute!";
            }
            else
            {
              labelBar = typeError;
            }
          }
          else
          {
            precentFilled = property.intValue / maxValue;
            labelBar = "[" + property.name + "] " + property.intValue + "/" + (int)maxValue;
          }
        } break;
        case SerializedPropertyType.Float:
        {
          if(maxValue < 0)
          {
            if(property.floatValue > 1)
            {
              error = true;
              if(typeError == "unset")
              {
                labelBar = "property value is over 1, and no max value has been specified!";
              }
              else
              {
                labelBar = typeError;
              }
              break;
            }

            precentFilled = property.floatValue / 1;
            labelBar = "[" + property.name + "] " + (int)property.floatValue + "/" + 1;
          }
          else
          {
            precentFilled = property.floatValue / maxValue;
            labelBar = "[" + property.name + "] " + (int)property.floatValue + "/" + (int)maxValue;
          }
        } break;
        default:
        {
          error = true;
          labelBar = "unsupported type for a stats bar!";
        } break;
      }

      if(error)
      {
        GUI.Label(positionBar, labelBar);
      }
      else
      {
        Color colorBar = Colors.FromEnum(statBar.color);
        Color colorLabel = Color.white;
        if(precentFilled < 0)
        {
          precentFilled = 0;
        }
        if(precentFilled > 1)
        {
          precentFilled = 1;
        }
        DrawBar(positionBar, precentFilled, labelBar, colorBar, colorLabel);
      }

      EditorGUI.PropertyField(new Rect(position.position.x, position.position.y+ lineHight  + padding, position.size.x, lineHight), property);
    }

    // END PropertyDrawer
    // START Private Utilities

    private void DrawBar(Rect position, float precentFilled, string label, Color colorBar, Color colorLabel)
    {
      if(Event.current.type != EventType.Repaint)
        return;

      Rect positionFill = new Rect(position.x, position.y, position.width * precentFilled,
            position.height);
      Rect positionLabel = new Rect(position.x, position.y-3, position.width, position.height);

      TextAnchor alignRestore = GUI.skin.label.alignment;
      GUI.skin.label.alignment = TextAnchor.UpperCenter;
      Color colorRestore = GUI.contentColor;
      GUI.contentColor = colorLabel;

      EditorGUI.DrawRect(position, new Color(0.1f, 0.1f, 0.1f));
      EditorGUI.DrawRect(positionFill, colorBar);
      EditorGUI.DropShadowLabel(positionLabel, label);

      GUI.contentColor = colorRestore;
      GUI.skin.label.alignment = alignRestore;
    }

    // END Private Utilities

  }

}