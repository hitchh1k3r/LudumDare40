using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HitchLib;

[CustomPropertyDrawer(typeof(NavigationPlane.Exception))]
public class NavPlaneExclusion : PropertyDrawer
{

  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
  {
    EditorGUI.BeginProperty(position, label, property);
    int indent = EditorGUI.indentLevel;
    EditorGUI.indentLevel = 0;
    float lWidth = EditorGUIUtility.labelWidth;
    EditorGUIUtility.labelWidth = 15.0f;

    float t = position.width / 5;
    Rect tRect   = new Rect(position.x, position.y, t - 5, position.height);
    Rect navRect = new Rect(position.x + position.width - 20, position.y, 20, position.height);

    float w = (position.width - 20 - t) / 3;
    Rect a3Rect   = new Rect(position.x +         t, position.y, w - 5, position.height);
    Rect b3Rect   = new Rect(position.x +     w + t, position.y, w - 5, position.height);
    Rect c3Rect   = new Rect(position.x + 2 * w + t, position.y, w - 5, position.height);

    w = (position.width - 20 - t) / 4;
    Rect a4Rect   = new Rect(position.x +         t, position.y, w - 5, position.height);
    Rect b4Rect   = new Rect(position.x +     w + t, position.y, w - 5, position.height);
    Rect c4Rect   = new Rect(position.x + 2 * w + t, position.y, w - 5, position.height);
    Rect d4Rect   = new Rect(position.x + 3 * w + t, position.y, w - 5, position.height);

    w = (position.width - 20 - t) / 5;
    Rect a5Rect   = new Rect(position.x +         t, position.y, w - 5, position.height);
    Rect b5Rect   = new Rect(position.x +     w + t, position.y, w - 5, position.height);
    Rect c5Rect   = new Rect(position.x + 2 * w + t, position.y, w - 5, position.height);
    Rect d5Rect   = new Rect(position.x + 3 * w + t, position.y, w - 5, position.height);
    Rect e5Rect   = new Rect(position.x + 4 * w + t, position.y, w - 5, position.height);

    EditorGUI.PropertyField(tRect, property.FindPropertyRelative("shape"), GUIContent.none);

    if(property.FindPropertyRelative("shape").enumValueIndex ==
          (int) NavigationPlane.ExceptionShape.RECT)
    {
      EditorGUI.PropertyField(a4Rect, property.FindPropertyRelative("xP"), new GUIContent("X"));
      EditorGUI.PropertyField(b4Rect, property.FindPropertyRelative("zP"), new GUIContent("Z"));
      EditorGUI.PropertyField(c4Rect, property.FindPropertyRelative("xS"), new GUIContent("W"));
      EditorGUI.PropertyField(d4Rect, property.FindPropertyRelative("zS"), new GUIContent("H"));
    }
    else if(property.FindPropertyRelative("shape").enumValueIndex ==
          (int) NavigationPlane.ExceptionShape.ROT_RECT)
    {
      EditorGUI.PropertyField(a5Rect, property.FindPropertyRelative("xP"),  new GUIContent("X"));
      EditorGUI.PropertyField(b5Rect, property.FindPropertyRelative("zP"),  new GUIContent("Z"));
      EditorGUI.PropertyField(c5Rect, property.FindPropertyRelative("xS"),  new GUIContent("W"));
      EditorGUI.PropertyField(d5Rect, property.FindPropertyRelative("zS"),  new GUIContent("H"));
      EditorGUI.PropertyField(e5Rect, property.FindPropertyRelative("rot"), new GUIContent("R"));
    }
    else if(property.FindPropertyRelative("shape").enumValueIndex ==
          (int) NavigationPlane.ExceptionShape.CIRCLE)
    {
      EditorGUI.PropertyField(a3Rect, property.FindPropertyRelative("xP"), new GUIContent("X"));
      EditorGUI.PropertyField(b3Rect, property.FindPropertyRelative("zP"), new GUIContent("Z"));
      EditorGUI.PropertyField(c3Rect, property.FindPropertyRelative("xS"), new GUIContent("R"));
    }

    EditorGUI.PropertyField(navRect, property.FindPropertyRelative("navigable"), GUIContent.none);

    EditorGUIUtility.labelWidth = lWidth;
    EditorGUI.indentLevel = indent;
    EditorGUI.EndProperty();
  }

}

[CustomEditor(typeof(NavigationPlane))]
public class NavPlaneEditor : Editor
{

  EditorGUIExtension.ReorderableList exceptionsList;
  EditorGUIExtension.ReorderableList connectionsLight;
  SerializedProperty name, xP, zP, xS, zS, navigable, exceptions, connections;

  void OnEnable()
  {
    name = serializedObject.FindProperty("name");
    xP = serializedObject.FindProperty("xP");
    zP = serializedObject.FindProperty("zP");
    xS = serializedObject.FindProperty("xS");
    zS = serializedObject.FindProperty("zS");
    navigable = serializedObject.FindProperty("navigable");
    exceptions = serializedObject.FindProperty("exceptions");
    connections = serializedObject.FindProperty("connections");
    exceptionsList = new EditorGUIExtension.ReorderableList(exceptions);
    connectionsLight = new EditorGUIExtension.ReorderableList(connections);
    connectionsLight.reorderableList.drawElementCallback =
      (Rect rect, int index, bool isActive, bool isFocused) =>
      {
        SerializedProperty element = connections.GetArrayElementAtIndex(index);
        rect.y += 2;
        rect.height -= 5;

        EditorGUI.BeginChangeCheck();
        int v = 0;
        Object cObj = element.objectReferenceValue;
        string[] names = new string[NavigationPlane.instances.Count];
        for(int i = 0; i < NavigationPlane.instances.Count; ++i)
        {
          if(cObj == NavigationPlane.instances[i])
          {
            v = i;
          }
          names[i] = NavigationPlane.instances[i].name;
        }
        v = EditorGUI.Popup(rect, v, names);
        if(EditorGUI.EndChangeCheck())
        {
          element.objectReferenceValue = NavigationPlane.instances[v];
          connections.serializedObject.ApplyModifiedProperties();
        }
      };
  }

  public override void OnInspectorGUI()
  {
    EditorGUIExtension.DrawScriptBox(this);
    serializedObject.Update();
    EditorGUILayout.PropertyField(name);
    EditorGUILayout.PropertyField(xP);
    EditorGUILayout.PropertyField(zP);
    EditorGUILayout.PropertyField(xS);
    EditorGUILayout.PropertyField(zS);
    EditorGUILayout.PropertyField(navigable);
    exceptionsList.Draw();
    connectionsLight.Draw();
    serializedObject.ApplyModifiedProperties();
  }

}
