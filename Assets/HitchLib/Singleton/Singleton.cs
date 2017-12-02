using UnityEngine;

namespace HitchLib
{

  public abstract class Singleton : MonoBehaviour
  {

    // Messages:

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CreateInstances()
    {
      foreach(GameObject prefab in Resources.LoadAll<GameObject>("Singletons/"))
      {
        if(prefab != null)
        {
          GameObject instance = Instantiate(prefab);
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

    // Singleton (interface):

    protected abstract void SetInstance(GameObject gameObject);

  }

}
