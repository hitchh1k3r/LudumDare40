using UnityEngine;

public class NameManager : HitchLib.Singleton // MonoBehaviour
{

  // Referances:
  public TextAsset maleNames;
  public TextAsset femaleNames;

  // State:
  private string[] names;

  // Static Instance:
  private static NameManager instance;

  void Awake()
  {
    string[] males = maleNames.text.Split(new[]{'\n'},
          System.StringSplitOptions.RemoveEmptyEntries);
    string[] females = femaleNames.text.Split(new[]{'\n'},
          System.StringSplitOptions.RemoveEmptyEntries);
    names = new string[males.Length + females.Length];
    int m = 0;
    int f = 0;
    for(int i = 0; i < names.Length; ++i)
    {
      if(((float)m / males.Length) < ((float)f / females.Length))
      {
        names[i] = males[m];
        ++m;
      }
      else
      {
        names[i] = females[f];
        ++f;
      }
    }
  }

  public static string GetName()
  {
    // size * (normal * normal) = lower numbers are exponentially more likely!
    int weightedIndex = (int)(instance.names.Length * (Random.Range(0.0f, 1.0f) *
          Random.Range(0.0f, 1.0f)));
    return instance.names[weightedIndex] + " " + (char)('A' + Random.Range(0, ('Z'-'A'))) + ".";
  }

  // Interface HitchLib.Singleton < < < < < < < < < < < < < < < < < < < < < < < < < < < < < < < < <
  protected override void SetInstance(GameObject gameObject)
  {
    instance = gameObject.GetComponent<NameManager>();
  }
  // Interface HitchLib.Singleton > > > > > > > > > > > > > > > > > > > > > > > > > > > > > > > > >

}
