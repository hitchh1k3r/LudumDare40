// Based on CustomInspectorCreator by LotteMakesStuff
// https://gist.github.com/LotteMakesStuff/cb63e4e25e5dfdda19a95380e9c03436

using UnityEngine;
using UnityEditor;
using System.IO;

namespace HitchLib
{

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  public static class CustomInspectorCreator
  {

    // START Menu Items

    [MenuItem("Assets/Create/Custom Inspector", priority = 81)]
    static void CreateInsptorEditorClass()
    {
      foreach(Object script in Selection.objects)
      {
        BuildEditorFile(script);
      }

      AssetDatabase.Refresh();
    }

    [MenuItem("Assets/Create/Custom Inspector", priority = 81, validate = true)]
    static bool ValidateCreateInsptorEditorClass()
    {
      foreach(Object script in Selection.objects)
      {
        string path = AssetDatabase.GetAssetPath(script);

        if(script.GetType() != typeof(MonoScript))
          return false;
        if(!path.EndsWith(".cs"))
          return false;
        if(path.Contains("Editor"))
          return false;
      }

      return true;
    }

    // END Menu Items
    // START Private Utilities

    private static void BuildEditorFile(Object obj)
    {
      MonoScript monoScript = obj as MonoScript;
      if(monoScript == null)
      {
        Debug.LogError("Cannot generate a custom inspector, Selected script was not a MonoBehavior");
        return;
      }

      string assetPath = AssetDatabase.GetAssetPath(obj);
      string filename = Path.GetFileNameWithoutExtension(assetPath);
      string scriptNamespace = monoScript.GetClass().Namespace;

      string script = (scriptNamespace == null ? string.Format(templateBasic, filename) :
            string.Format(templateNamespace, filename, scriptNamespace));

      string editorFolder = Path.GetDirectoryName(assetPath) + "/Editor";

      if(!Directory.Exists(editorFolder))
      {
        Directory.CreateDirectory(editorFolder);
      }

      if(File.Exists(editorFolder + "/" + filename + "Inspector.cs"))
      {
        Debug.LogError(filename + "Inspector.cs already exists.");
        return;
      }

      File.WriteAllText(editorFolder + "/" + filename + "Inspector.cs", script);
    }

    // END Private Utilities
    // START Templates

    static readonly string templateBasic = @"using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof({0}))]
/* [CanEditMultipleObjects] */
public class {0}Inspector : Editor
{{

  // START Messages

  void OnEnable()
  {{
    // serializedObject.FindProperty();
  }}

  // END Messages
  // START Editor

  public override void OnInspectorGUI()
  {{
    serializedObject.Update();

    DrawDefaultInspector();
    // EditorGUILayout.PropertyField();

    serializedObject.ApplyModifiedProperties();
  }}

  // END Editor

}}
";

    static readonly string templateNamespace = @"using UnityEngine;
using UnityEditor;
using System.Collections;

namespace {1}
{{

  // HitchLib //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//
  [CustomEditor(typeof({0}))]
  /* [CanEditMultipleObjects] */
  public class {0}Inspector : Editor
  {{

    // START Messages

    void OnEnable()
    {{
      // serializedObject.FindProperty();
    }}

    // END Messages
    // START Editor

    public override void OnInspectorGUI()
    {{
      serializedObject.Update();

      DrawDefaultInspector();
      // EditorGUILayout.PropertyField();

      serializedObject.ApplyModifiedProperties();
    }}

    // END Editor

  }}

}}
";

  // END Templates

  }

}
