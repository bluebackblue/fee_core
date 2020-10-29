

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief シェーダ。ブルーム。
*/


Shader "Fee/Bloom/AddUpSampling"
{
	Properties
	{
		_MainTex("_MainTex",2D) = "white"{}
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

			Blend One One

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
				float4 pos			: SV_POSITION;
				float2 uv			: TEXCOORD0;
			};

			/** _MainTex
			*/
			sampler2D _MainTex;
			float4 _MainTex_TexelSize;

			/** vert
			*/
			v2f vert(appdata a_appdata)
			{
				v2f t_ret;
				{
					t_ret.pos = UnityObjectToClipPos(a_appdata.vertex);
					t_ret.uv = a_appdata.uv;
				}
				return t_ret;
			}

			/** frag
			*/
			fixed4 frag(v2f a_v2f) : SV_Target
			{
				//アップサンプリング。
				half3 t_color_a = tex2D(_MainTex,a_v2f.uv + float2( _MainTex_TexelSize.x*0.5, _MainTex_TexelSize.y*0.5)).rgb;
				half3 t_color_b = tex2D(_MainTex,a_v2f.uv + float2( _MainTex_TexelSize.x*0.5,-_MainTex_TexelSize.y*0.5)).rgb;
				half3 t_color_c = tex2D(_MainTex,a_v2f.uv + float2(-_MainTex_TexelSize.x*0.5, _MainTex_TexelSize.y*0.5)).rgb;
				half3 t_color_d = tex2D(_MainTex,a_v2f.uv + float2(-_MainTex_TexelSize.x*0.5,-_MainTex_TexelSize.y*0.5)).rgb;
				half3 t_color = (t_color_a + t_color_b + t_color_c + t_color_d) * 0.25;

				//加算。
				return fixed4(t_color,1.0);
			}
			ENDCG
		}
	}
}

