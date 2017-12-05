using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class WallRenderer : MonoBehaviour
{

  // Configuration:
  public Texture lightTexture;
  public Sprite spriteTexture;
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
    block.Clear();
    if(lightTexture != null)
    {
      block.SetVector("_Tiling", new Vector4(transform.lossyScale.x, transform.lossyScale.y,
            xOffset, yOffset));
      block.SetVector("_Light", new Vector4(directionShading, backWallOpacity, obscureWall ? 1 : 0,
            0));
      block.SetTexture("_MainTex", lightTexture);
      renderer.enabled = true;
    }
    else if(spriteTexture != null)
    {
      block.SetVector("_Light", new Vector4(directionShading, backWallOpacity, obscureWall ? 1 : 0,
            0));
      block.SetTexture("_MainTex", spriteTexture.texture);
      Vector2 size = spriteTexture.texture.texelSize;
      Rect part = spriteTexture.rect;
      block.SetVector("_Tiling", new Vector4(size.x * part.width, size.y * part.height,
            (size.x * part.x) + xOffset, (size.y * part.y) + yOffset));
      renderer.enabled = true;
    }
    else
    {
      renderer.enabled = false;
    }
    renderer.SetPropertyBlock(block);
  }

}
