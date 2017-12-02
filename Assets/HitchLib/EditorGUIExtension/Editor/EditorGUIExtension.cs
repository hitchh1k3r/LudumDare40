using UnityEngine;
using UnityEditor;

namespace HitchLib
{

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  public static class EditorGUIExtension
  {

    // START Public Utilities

    public static void DrawScriptBox(Editor editor)
    {
      SerializedProperty prop = editor.serializedObject.GetIterator();
      prop.NextVisible(true);
      GUI.enabled = false;
      EditorGUILayout.PropertyField(prop);
      GUI.enabled = true;
    }

    // END Public Utilities

    // HitchLib // EditorGUIExtension //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
    public class ReorderableList
    {

      // State:
      public readonly UnityEditorInternal.ReorderableList reorderableList;

      // START Constructor

      public ReorderableList(SerializedProperty property)
      {
        if(!property.isArray)
        {
          throw new UnityException("ReorderableList needs an array property!");
        }

        reorderableList = new UnityEditorInternal.ReorderableList(property.serializedObject,
              property, true, true, true, true);

        reorderableList.drawHeaderCallback =
              (Rect rect) =>
              {
                EditorGUI.LabelField(rect, property.displayName);
              };
        reorderableList.drawElementCallback =
              (UnityEngine.Rect rect, int index, bool isActive, bool isFocused) =>
              {
                SerializedProperty element =
                      reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                rect.height -= 5;

                EditorGUI.BeginChangeCheck();
                EditorGUI.PropertyField(rect, element, new GUIContent(element.displayName));
                if (EditorGUI.EndChangeCheck())
                {
                  property.serializedObject.ApplyModifiedProperties();
                }
              };
        reorderableList.onChangedCallback =
              (UnityEditorInternal.ReorderableList list) =>
              {
                property.serializedObject.ApplyModifiedProperties();
              };
      }

      // END Constructor
      // START Public Utilities

      public void Draw(bool addRemove = true)
      {
        reorderableList.displayAdd = addRemove;
        reorderableList.displayRemove = addRemove;
        reorderableList.DoLayoutList();
      }

      public void Draw(Rect position, bool addRemove = true)
      {
        reorderableList.displayAdd = addRemove;
        reorderableList.displayRemove = addRemove;
        reorderableList.DoList(position);
      }

      // END Public Utilities

    }

  }

}
