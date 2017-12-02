Shader "Custom/3D/Walls Back"
{
  Properties
  {
    [PerRendererData] _LightTex ("Light Texture", 2D) = "white" {}
    [PerRendererData] _DarkTex ("Dark Texture", 2D) = "white" {}
    [PerRendererData] _Tiling ("Texture Tiling", Vector) = (1, 1, 0, 0)
    [PerRendererData] _Light ("Light Settings", Vector) = (1, 0.5, 0, 0)
  }

  SubShader
  {
    Tags
    {
      "Queue"="Geometry+1000"
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
        #include "WhoGoesTexturing.cginc"

        #pragma vertex BasicVert
        #pragma fragment frag
        #pragma target 2.0
        #pragma shader_feature _ NORTH_WALL SOUTH_WALL EAST_WALL WEST_WALL

        fixed4 _Tiling;

        fixed4 frag(v2f i) : SV_Target
        {
          fixed4 c = tex2D(_LightTex, ((i.texcoord * _Tiling.xy) + _Tiling.zw) % 1);
          if(c.a < 0.5)
          {
            discard;
          }
          if(_Light[2] > 0.5)
          {
            return fixed4(0, 0, 0, 1);
          }

          c.rgb *= (i.light * 0.25f) + 0.125f;
          c.a = _Light[1];

          return c;
        }
      ENDCG
    }
  }
}
