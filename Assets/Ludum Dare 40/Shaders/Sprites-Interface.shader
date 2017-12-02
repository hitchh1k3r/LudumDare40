Shader "Custom/Interface Icon"
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
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
      "DisableBatching"="True"
		}

    Cull Back
    Lighting Off
    ZTest Off
    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
      #pragma multi_compile _ PIXELSNAP_ON
			#include "UnitySprites.cginc"

      v2f vert(appdata_t IN)
      {
        v2f OUT;

        float3 camUp = normalize(UNITY_MATRIX_IT_MV[1].xyz);
        float3 camRight = normalize(UNITY_MATRIX_IT_MV[0].xyz);
        float4 vertex = mul(UNITY_MATRIX_M, float4(IN.vertex.xyz, 0));
        if(camUp.y == 0)
        {
          camUp.y = 0.0000001;
        }

        vertex.y *= 1 / camUp.y;
        vertex.xz = vertex.x * camRight.xz;
        vertex += float4(mul(UNITY_MATRIX_M, float4(0, 0, 0, 1)).xyz, 1);

        OUT.vertex = mul(UNITY_MATRIX_VP, vertex);
        OUT.texcoord = IN.texcoord;
        OUT.color = IN.color * _Color * _RendererColor;

#ifdef PIXELSNAP_ON
        OUT.vertex = UnityPixelSnap(OUT.vertex);
#endif

        return OUT;
      }

      fixed4 frag(v2f IN) : SV_Target
      {
        fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
        return c;
      }
		ENDCG
		}
	}
}
