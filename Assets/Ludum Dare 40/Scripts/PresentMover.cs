using UnityEngine;
using System.Collections.Generic;

public class PresentMover : MonoBehaviour
{

  // Referances:
  public GameObject prefabPresent;

  // Configuration:
  public Vector3 moveSpeed = new Vector3(1.0f, 0, 0);
  public float despawnX = -14.75f;
  public float intervalSpawn = 3.0f;

  // State:
  private float spawnTimer;
  private readonly List<Transform> presents = new List<Transform>();

  // Messages:

  void Update()
  {
    spawnTimer += Time.deltaTime;
    if(spawnTimer > intervalSpawn)
    {
      spawnTimer = 0;
      GameObject go = Instantiate<GameObject>(prefabPresent, transform);
      go.transform.position = transform.position;
      presents.Add(go.transform);
    }

    Vector3 movement = Time.deltaTime * moveSpeed;
    List<Transform> removeUs = new List<Transform>();
    foreach(Transform present in presents)
    {
      present.position = present.position + movement;
      if(present.position.x > despawnX)
      {
        Destroy(present.gameObject);
        removeUs.Add(present);
      }
    }
    foreach(Transform r in removeUs)
    {
      presents.Remove(r);
    }

  }

}
