using UnityEngine;
using System.Collections.Generic;

namespace HitchLib
{

  public abstract class Singleton : MonoBehaviour
  {

    // Cache:
    private static List<GameObject> instances;

    // Messages:

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CreateInstances()
    {
      instances = new List<GameObject>();
      foreach(GameObject prefab in Resources.LoadAll<GameObject>("Singletons/"))
      {
        if(prefab != null)
        {
          GameObject instance = Instantiate(prefab);
          instances.Add(instance);
          Singleton singleton = instance.GetComponent<Singleton>();
          if(singleton != null)
          {
            singleton.SetInstance(instance);
          }
          instance.name = prefab.name;
          DontDestroyOnLoad(instance);
        }
      }
    }

    public static void ResetSingletons()
    {
      foreach(GameObject go in instances)
      {
        Destroy(go);
      }
      CreateInstances();
    }

    // Singleton (interface):

    protected abstract void SetInstance(GameObject gameObject);

  }

}
