using UnityEngine;

public class NameManager : HitchLib.Singleton // MonoBehaviour
{

  // Referances:
  public TextAsset maleNames;
  public TextAsset femaleNames;

  // State:
  private string[] female;
  private string[] male;
  private string[] names;
  private string myFirstName;
  private string myLastInitial;
  private string myLastName;

  // Static Instance:
  private static NameManager instance;

  void Awake()
  {
    male = maleNames.text.Split(new[]{'\n'},
          System.StringSplitOptions.RemoveEmptyEntries);
    female = femaleNames.text.Split(new[]{'\n'},
          System.StringSplitOptions.RemoveEmptyEntries);
    names = new string[male.Length + female.Length];
    int m = 0;
    int f = 0;
    for(int i = 0; i < names.Length; ++i)
    {
      if(((float)m / male.Length) < ((float)f / female.Length))
      {
        names[i] = male[m];
        ++m;
      }
      else
      {
        names[i] = female[f];
        ++f;
      }
    }
  }

  public static void SetMyName(string first, string last)
  {
    instance.myFirstName = first;
    instance.myLastName = last;
    instance.myLastInitial = (last[0] + ".").ToUpper();
  }

  public static string GetMyName()
  {
    return instance.myFirstName + " " + instance.myLastInitial;
  }

  public static string GetName(bool isFemale)
  {
    if(isFemale)
    {
      int weightedIndex = (int)(instance.female.Length * (Random.Range(0.0f, 1.0f) *
            Random.Range(0.0f, 1.0f)));
      return instance.female[weightedIndex] + " " + (char)('A' + Random.Range(0, ('Z'-'A'))) + ".";
    }
    else
    {
      int weightedIndex = (int)(instance.male.Length * (Random.Range(0.0f, 1.0f) *
            Random.Range(0.0f, 1.0f)));
      return instance.male[weightedIndex] + " " + (char)('A' + Random.Range(0, ('Z'-'A'))) + ".";
    }
  }

  // Interface HitchLib.Singleton < < < < < < < < < < < < < < < < < < < < < < < < < < < < < < < < <
  protected override void SetInstance(GameObject gameObject)
  {
    instance = gameObject.GetComponent<NameManager>();
  }
  // Interface HitchLib.Singleton > > > > > > > > > > > > > > > > > > > > > > > > > > > > > > > > >

}
