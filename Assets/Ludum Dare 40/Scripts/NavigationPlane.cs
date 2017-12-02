using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NavigationPlane : MonoBehaviour
{

  // State:
  public string name;
  public float xP, zP, xS, zS;
  public bool navigable = true;
  public Exception[] exceptions;
  public NavigationPlane[] connections;

  // Cache:
  public static readonly List<NavigationPlane> instances = new List<NavigationPlane>();

  // Messages:

  void OnEnable()
  {
    instances.Add(this);
  }

  void OnDisable()
  {
    instances.Remove(this);
  }

#if UNITY_EDITOR
  void OnDrawGizmosSelected()
  {
    Gizmos.color = navigable ? new Color(1.0f, 1.0f, 0, 0.5f) : new Color(1.0f, 0, 0, 0.5f);
    Gizmos.DrawCube(new Vector3(xP, 0, zP), new Vector3(xS, 0.1f, zS));
    Gizmos.color = Color.black;
    Gizmos.DrawWireCube(new Vector3(xP, 0, zP), new Vector3(xS, 0.1f, zS));
    Gizmos.color = new Color(0.0f, 1.0f, 1.0f);
    Vector3 a = new Vector3(xP, 0.055f, zP);
    foreach(NavigationPlane connection in connections)
    {
      if(connection != null)
      {
        Vector3 b = new Vector3(connection.xP, 0.055f, connection.zP);
        Vector3 arrow = 0.4f * (Quaternion.Euler(0, -45, 0) * (a - b).normalized);
        Gizmos.DrawLine(a, b);
        Gizmos.DrawLine(b, b + arrow);
        Gizmos.DrawLine(b, b + (Quaternion.Euler(0, 90, 0) * arrow));
      }
    }
    foreach(Exception exception in exceptions)
    {
      if(exception.shape == ExceptionShape.CIRCLE)
      {
        Gizmos.color = exception.navigable ? Color.green : Color.red;
        Gizmos.DrawMesh(PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Cylinder),
          new Vector3(exception.xP, 0, exception.zP), Quaternion.identity,
          new Vector3(exception.xS * 2, 0.0525f, exception.xS * 2));
      }
      else if(exception.shape == ExceptionShape.RECT)
      {
        Gizmos.color = exception.navigable ? Color.green : Color.red;
        Gizmos.DrawCube(new Vector3(exception.xP, 0, exception.zP),
              new Vector3(exception.xS, 0.105f, exception.zS));
      }
      else if(exception.shape == ExceptionShape.ROT_RECT)
      {
        Matrix4x4 push = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(new Vector3(exception.xP, 0, exception.zP),
              Quaternion.EulerRotation(0, exception.rot, 0), Vector3.one) *
              Matrix4x4.Translate(new Vector3(-exception.xP, 0, -exception.zP)) * Gizmos.matrix;

        Gizmos.color = exception.navigable ? Color.green : Color.red;
        Gizmos.DrawCube(new Vector3(exception.xP, 0, exception.zP),
              new Vector3(exception.xS, 0.105f, exception.zS));
        Gizmos.matrix = push;
      }
    }
    UnityEditor.Handles.Label(new Vector3(xP, 0.5f, zP), name, GUI.skin.button);
  }
#endif

  // Utilities:

  public static NavigationPlane FindPlane(string name)
  {
    foreach(NavigationPlane plane in instances)
    {
      if(plane.name == name)
      {
        return plane;
      }
    }
    return null;
  }

  public static bool ValidMove(ref Vector3 pos, ref NavigationPlane plane)
  {
    if(plane.navigable && plane.Contains(pos.x, pos.z))
    {
      return true;
    }

    NavigationPlane nPlane = null;
    if(plane.navigable)
    {
      List<NavigationPlane> visitedPlanes = new List<NavigationPlane>();
      nPlane = plane.SearchPlanes(pos.x, pos.z, ref visitedPlanes, 3);
    }
    if(nPlane == null)
    {
      nPlane = plane;
      Vector3 nPos = plane.NearestPoint(pos);
      float nDist = (nPos - pos).sqrMagnitude;
      if(!plane.navigable)
      {
        nDist = float.PositiveInfinity;
      }

      foreach(NavigationPlane tPlane in plane.connections)
      {
        if(tPlane.navigable)
        {
          Vector3 tPos = tPlane.NearestPoint(pos);
          float tDist = (tPos - pos).sqrMagnitude;
          if(tDist < nDist)
          {
            nDist = tDist;
            nPos = tPos;
            nPlane = tPlane;
          }
        }
      }

      pos = nPos;
      plane = nPlane;

      return false;
    }
    else
    {
      plane = nPlane;
      return true;
    }
  }

  private Vector3 NearestPoint(Vector3 pos)
  {
    float xH = 0.5f * xS;
    float zH = 0.5f * zS;
    if(pos.x < xP - xH)
    {
      pos.x = xP - xH;
    }
    else if(pos.x > xP + xH)
    {
      pos.x = xP + xH;
    }
    if(pos.z < zP - zH)
    {
      pos.z = zP - zH;
    }
    else if(pos.z > zP + zH)
    {
      pos.z = zP + zH;
    }
    return pos;
  }

  private NavigationPlane SearchPlanes(float x, float z, ref List<NavigationPlane> visitedPlanes,
        int depth)
  {
    if(--depth < 0 || visitedPlanes.Contains(this) || !navigable)
    {
      return null;
    }

    if(Contains(x, z))
    {
      return this;
    }
    visitedPlanes.Add(this);

    NavigationPlane s;
    for(int i = 0; i < connections.Length; ++i)
    {
      s = connections[i].SearchPlanes(x, z, ref visitedPlanes, depth);
      if(s != null)
      {
        return s;
      }
    }

    return null;
  }

  private bool Contains(float x, float z)
  {
    bool contained = (Mathf.Abs(x - xP) < (0.5f * xS) && Mathf.Abs(z - zP) < (0.5f * zS));

    return contained;
  }

  private Vector3 NearestExceptionPoint(Vector3 pos)
  {
    float xH = 0.5f * xS;
    float zH = 0.5f * zS;
    if(pos.x < xP - xH)
    {
      pos.x = xP - xH;
    }
    else if(pos.x > xP + xH)
    {
      pos.x = xP + xH;
    }
    if(pos.z < zP - zH)
    {
      pos.z = zP - zH;
    }
    else if(pos.z > zP + zH)
    {
      pos.z = zP + zH;
    }
    return pos;
  }

  private bool ExceptionContains(Exception exception, float x, float z)
  {
    if(exception.shape == ExceptionShape.CIRCLE)
    {
      float deltaX = exception.xP - x;
      float deltaZ = exception.zP - z;
      return (deltaX * deltaX + deltaZ * deltaZ < exception.xS * exception.xS);
    }
    else if(exception.shape == ExceptionShape.RECT)
    {
      return (Mathf.Abs(x - exception.xP) < (0.5f * exception.xS) &&
            Mathf.Abs(z - exception.zP) < (0.5f * exception.zS));
    }
    else if(exception.shape == ExceptionShape.ROT_RECT)
    {
      float sin = Mathf.Sin(exception.rot);
      float cos = Mathf.Cos(exception.rot);
      x -= exception.xP;
      z -= exception.zP;
      float nX = cos * x - sin * z;
      float nZ = sin * x + cos * z;
      x = nX + exception.xP;
      z = nZ + exception.zP;
      return (Mathf.Abs(x - exception.xP) < (0.5f * exception.xS) &&
            Mathf.Abs(z - exception.zP) < (0.5f * exception.zS));
    }
    return false;
  }

  // NavigationPlane //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*/

  [System.Serializable]
  public struct Exception
  {
    public ExceptionShape shape;
    public float xP, zP, xS, zS, rot;
    public bool navigable;
  }

  // NavigationPlane //*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*//*/

  public enum ExceptionShape
  {
    RECT, CIRCLE, ROT_RECT
  }

}
