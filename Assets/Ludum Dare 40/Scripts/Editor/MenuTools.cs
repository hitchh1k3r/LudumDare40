using UnityEngine;
using UnityEditor;

public class MenuTools
{

  [MenuItem("Tools/Randomize Colors")]
  static void RandomizeColors()
  {
    foreach(GameObject go in Selection.gameObjects)
    {
      SpriteRenderer psr = go.GetComponent<SpriteRenderer>();
      SpriteRenderer rsr = go.transform.GetChild(0).GetComponent<SpriteRenderer>();
      float h = Random.Range(0.0f, 1.0f);
      psr.color = Color.HSVToRGB(h, Random.Range(0.8f, 1.0f),
            Random.Range(0.9f, 1.0f));
      rsr.color = Color.HSVToRGB((h + 0.5f) % 1.0f, Random.Range(0.2f, 0.5f),
            Random.Range(0.9f, 1.0f));
    }
  }

}
