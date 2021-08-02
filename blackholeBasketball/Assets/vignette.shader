Shader "Custom/Vignette" {
	Properties {
		_VignettePower ("VignettePower", Range(0.0,6.0)) = 5.5
		_Color1 ("Color1",Color) = (0,0,0,0)
		_Color2 ("Color2",Color) = (0,0,0,0)
	}
	SubShader 
	{
		Pass
		{
		
		CGPROGRAM
		#pragma vertex vert_img
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest
		#include "UnityCG.cginc"

		uniform float _VignettePower;
		fixed4 _Color1;
		fixed4 _Color2;

		struct v2f
		{
			float2 texcoord	: TEXCOORD0;
		};
		
		float4 frag(v2f_img i) : COLOR
		{
		float2 dist = (i.uv - 0.5f) * 1.25f;
		dist.x = 1 - dot(dist, dist)  * _VignettePower;
		return lerp(_Color1,_Color2,dist.x);
		
		}

		ENDCG
		} 
	}
} 