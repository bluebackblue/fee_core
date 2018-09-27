/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief シェーダ。ブルーム。
*/


Shader "Bloom/LastAddUpSampling"
{
    Properties
	{
        _MainTex ("Texture", 2D) = "white" {}
		texture_original ("Texture Original", 2D) = "white" {}
		intensity ("Intensity", Float) = 0
    }
    SubShader
    {
		Tags { "RenderType" = "Opaque" }
        Cull Off
        ZTest Always
        ZWrite Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			
			/** appdata
			*/
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			/** v2f
			*/
			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			/** _MainTex
			*/
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _MainTex_TexelSize;

			/** texture_original
			*/
			sampler2D texture_original;

			/** intensity
			*/
			float intensity;

			/** vert
			*/
			v2f vert(appdata v)
			{
				v2f t_ret;
				{
					t_ret.vertex = UnityObjectToClipPos(v.vertex);
					t_ret.uv = TRANSFORM_TEX(v.uv, _MainTex);
				}
				return t_ret;
			}

			/** frag
			*/
			fixed4 frag(v2f i) : SV_Target
			{
				//オリジナル。
				half4 t_color_original = tex2D(texture_original,i.uv);

				//アップサンプリング。
				half3 t_color_a = tex2D(_MainTex,i.uv + float2( _MainTex_TexelSize.x*0.5, _MainTex_TexelSize.y*0.5)).rgb;
				half3 t_color_b = tex2D(_MainTex,i.uv + float2( _MainTex_TexelSize.x*0.5,-_MainTex_TexelSize.y*0.5)).rgb;
				half3 t_color_c = tex2D(_MainTex,i.uv + float2(-_MainTex_TexelSize.x*0.5, _MainTex_TexelSize.y*0.5)).rgb;
				half3 t_color_d = tex2D(_MainTex,i.uv + float2(-_MainTex_TexelSize.x*0.5,-_MainTex_TexelSize.y*0.5)).rgb;
				half3 t_color = (t_color_a + t_color_b + t_color_c + t_color_d) * 0.25;

				//加算。
				t_color_original.rgb += t_color * intensity;

				return t_color_original;
			}
			ENDCG
		}
    }
}

