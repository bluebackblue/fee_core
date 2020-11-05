

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief デプス。
*/


Shader "_Fee/Depth/CameraDepthTexture"
{
	Properties
	{
		_MainTex					("_MainTex",2D)						= "white"{}
		blendrate				("blendrate",Range(0.0,1.0))			= 1.0
	}
	SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
		}
		Pass
		{
			Cull Off
			ZTest Always
			ZWrite Off

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			
			/** appdata
			*/
			struct appdata
			{
				float4 vertex		: POSITION;
				float2 uv			: TEXCOORD0;
			};

			/** v2f
			*/
			struct v2f
			{
				float4 vertex		: SV_POSITION;
				float2 uv			: TEXCOORD0;
			};

			/** _MainTex
			*/
			sampler2D _MainTex;

			/** _CameraDepthTexture
			*/
			sampler2D _CameraDepthTexture;

			/** blendrate
			*/
			float blendrate;

			/** vert
			*/
			v2f vert(appdata a_appdata)
			{
				v2f t_ret;
				{
					t_ret.vertex = UnityObjectToClipPos(a_appdata.vertex);
					t_ret.uv = a_appdata.uv;
				}
				return t_ret;
			}

			/** frag
			*/
			fixed4 frag(v2f a_v2f) : SV_Target
			{
				fixed3 t_color = tex2D(_MainTex,a_v2f.uv).rgb;

				if(blendrate > 0.0f){
					float t_depth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture,a_v2f.uv));
					t_depth = Linear01Depth(t_depth);
					t_color = t_color * (1.0f - blendrate) + fixed3(t_depth,t_depth,t_depth) * blendrate;
				}

				return fixed4(t_color,1.0f);
			}
			ENDCG
		}
	}
}

