Shader "Custom/3D/Walls Two Sided"
{
  Properties
  {
    [PerRendererData] _LightTex ("Light Texture", 2D) = "white" {}
    [PerRendererData] _DarkTex ("Dark Texture", 2D) = "white" {}
    [PerRendererData] _Tiling ("Texture Tiling", Vector) = (1, 1, 0, 0)
    [PerRendererData] _Light ("Light Settings", Vector) = (1, 0, 0, 0)
  }

  SubShader
  {
    Tags
    {
      "Queue"="Geometry+100"
      "IgnoreProjector"="True"
      "PreviewType"="Plane"
      "CanUseSpriteAtlas"="True"
    }

    Lighting Off
    ZTest LEqual
    ZWrite On

    Pass
    {
      Cull Back
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
          c.rgb *= i.light;

          return c;
        }
      ENDCG
    }

    Pass
    {
      Cull Front
      CGPROGRAM
        #include "WhoGoesTexturing.cginc"

        #pragma vertex vert
        #pragma fragment frag
        #pragma target 2.0
        #pragma shader_feature _ NORTH_WALL SOUTH_WALL EAST_WALL WEST_WALL

        fixed4 _Tiling;

        v2f vert(a2v i)
        {
          v2f o;

          o.vertex = UnityObjectToClipPos(i.vertex);
          o.texcoord = i.texcoord;
          o.light = GetWorldLight(mul(unity_ObjectToWorld, -i.normal));

          return o;
        }

        fixed4 frag(v2f i) : SV_Target
        {
          fixed4 c = tex2D(_LightTex, ((i.texcoord * _Tiling.xy) + _Tiling.zw) % 1);
          if(c.a < 0.5)
          {
            discard;
          }
          c.rgb *= i.light;

          return c;
        }
      ENDCG
    }  }
}
