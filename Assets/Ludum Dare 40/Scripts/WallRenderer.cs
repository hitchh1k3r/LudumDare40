using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class WallRenderer : MonoBehaviour
{

  // Configuration:
  public Texture lightTexture;
  public float xOffset;
  public float yOffset;
  public float directionShading = 1;
  public float backWallOpacity = 0.5f;
  public bool obscureWall = false;

  // Cache:
  private Renderer renderer;
  private MaterialPropertyBlock block;

  // Messages:

  void Awake()
  {
    renderer = GetComponent<Renderer>();
  }

  public void Update()
  {
    if(block == null)
    {
      block = new MaterialPropertyBlock();
    }
    else
    {
#if !UNITY_EDITOR
      return;
#endif
    }
    block.Clear();
    block.SetVector("_Tiling", new Vector4(transform.lossyScale.x, transform.lossyScale.y, xOffset, yOffset));
    block.SetVector("_Light", new Vector4(directionShading, backWallOpacity, obscureWall ? 1 : 0, 0));
    if(lightTexture != null)
    {
      block.SetTexture("_Tex", lightTexture);
      renderer.enabled = true;
    }
    else
    {
      renderer.enabled = false;
    }
    renderer.SetPropertyBlock(block);
  }

}
