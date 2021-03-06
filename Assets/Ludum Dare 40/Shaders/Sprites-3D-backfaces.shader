Shader "Custom/3D Sprite Backface"
{
  Properties
  {
    [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
    _Color ("Tint", Color) = (1,1,1,1)
    [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
    [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
    [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
    [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
  }

  SubShader
  {
    Tags
    {
      "Queue"="Geometry+500"
      "IgnoreProjector"="True"
      "PreviewType"="Plane"
      "CanUseSpriteAtlas"="True"
    }

    Lighting Off
    ZTest LEqual
    ZWrite Off
    Cull Front
    Blend SrcAlpha OneMinusSrcAlpha

    Pass
    {
      CGPROGRAM
        #pragma vertex SpriteVert
        #pragma fragment frag
        #pragma target 2.0
        #pragma multi_compile _ PIXESNAP_ON
        #include "UnitySprites.cginc"

        fixed4 frag(v2f IN) : SV_Target
        {
          fixed4 c = SampleSpriteTexture(IN.texcoord);
          if(c.a < 0.5)
          {
            discard;
          }
          c *= IN.color;
          c.rgb *= c.a;
          c.xyz *= 0.5;

          return fixed4(c.xyz, 0.5);
        }
      ENDCG
    }
  }
}
