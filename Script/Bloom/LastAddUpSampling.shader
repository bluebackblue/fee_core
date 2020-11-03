

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ブルーム。
*/


Shader "Fee/Bloom/LastAddUpSampling"
{
	Properties
	{
		_MainTex("_MainTex",2D) = "white"{}
		texture_original("texture_original",2D) = "white"{}
		intensity("intensity",Float) = 0
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
			float4 _MainTex_TexelSize;

			/** texture_original
			*/
			sampler2D texture_original;

			/** intensity
			*/
			float intensity;

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
				//オリジナル。
				half4 t_color_original = tex2D(texture_original,a_v2f.uv);

				//アップサンプリング。
				half3 t_color_a = tex2D(_MainTex,a_v2f.uv + float2( _MainTex_TexelSize.x*0.5, _MainTex_TexelSize.y*0.5)).rgb;
				half3 t_color_b = tex2D(_MainTex,a_v2f.uv + float2( _MainTex_TexelSize.x*0.5,-_MainTex_TexelSize.y*0.5)).rgb;
				half3 t_color_c = tex2D(_MainTex,a_v2f.uv + float2(-_MainTex_TexelSize.x*0.5, _MainTex_TexelSize.y*0.5)).rgb;
				half3 t_color_d = tex2D(_MainTex,a_v2f.uv + float2(-_MainTex_TexelSize.x*0.5,-_MainTex_TexelSize.y*0.5)).rgb;
				half3 t_color = (t_color_a + t_color_b + t_color_c + t_color_d) * 0.25;

				//加算。
				t_color_original.rgb += t_color * intensity;

				return t_color_original;
			}
			ENDCG
		}
	}
}

