// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

#ifndef WHO_GOES_TEXTURE_INCLUDED
#define WHO_GOES_TEXTURE_INCLUDED

  sampler2D _LightTex;
  sampler2D _DarkTex;
  fixed4    _Light;

  // Utilities:

  /*
  inline float4 UnityObjectToClipPos(in float3 pos)
  {
    return mul(UNITY_MATRIX_VP, mul(unity_ObjectToWorld, float4(pos, 1.0)));
  }
  */

  float GetWorldLight(float3 norm)
  {
    float light = 1;
    if((norm.x * norm.x) > (norm.z * norm.z))
    {
      if(norm.x > 0)
      {
        // +X
        light = 0.3;
      }
      else
      {
        // -X
        light = 1;
      }
    }
    else
    {
      if(norm.z > 0)
      {
        // +Z
        light = 0.6;
      }
      else
      {
        // -Z
        light = 0.6;
      }
    }
    return 1 - ((1 - light) * _Light[0]);
  }

  // Data:

  struct a2v
  {
    float4 vertex   : POSITION;
    float3 normal   : NORMAL;
    float2 texcoord : TEXCOORD0;
  };

  struct v2f
  {
    float4 vertex   : SV_POSITION;
    float  light    : TEXCOORD0;
    float2 texcoord : TEXCOORD1;
  };

  // Shaders:

  v2f BasicVert(a2v i)
  {
    v2f o;

    o.vertex = UnityObjectToClipPos(i.vertex);
    o.texcoord = i.texcoord;
    o.light = GetWorldLight(mul(unity_ObjectToWorld, i.normal));

    return o;
  }

  fixed4 BasicFrag(v2f i) : SV_Target
  {
    fixed4 c = tex2D(_LightTex, i.texcoord);
    c.rgb *= i.light;

    return c;
  }

#endif
