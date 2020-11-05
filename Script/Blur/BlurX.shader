

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ブラー。
*/


Shader "Fee/Blur/BlurX"
{
	Properties
	{
		_MainTex	("_MainTex",2D) = "white"{}
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
				half3 t_color = half3(0.0f,0.0f,0.0f);

				/*
				float[] t_table = new float[8];
				float t_total = 0.0f;
				float t_dispersion = 4.0f;
				for(int ii=0;ii<t_table.Length;ii++){
					t_table[ii] = Mathf.Exp(-0.5f * ((float)(ii*ii)) / t_dispersion);
					t_total += t_table[ii] * 2;
				}
				for(int ii=0;ii<t_table.Length;ii++){
					t_table[ii] /= t_total;
				}
				//total = 0.5f
				*/

				float t_weight_1 = 0.16637f;
				float t_weight_2 = 0.14677f;
				float t_weight_3 = 0.10087f;
				float t_weight_4 = 0.05399f;
				float t_weight_5 = 0.02250f;
				float t_weight_6 = 0.00730f;
				float t_weight_7 = 0.00184f;
				float t_weight_8 = 0.00036f;

				t_color += tex2D(_MainTex,a_v2f.uv + float2( _MainTex_TexelSize.x *  1,0.0f)).rgb * t_weight_1;
				t_color += tex2D(_MainTex,a_v2f.uv + float2(-_MainTex_TexelSize.x *  1,0.0f)).rgb * t_weight_1;
				t_color += tex2D(_MainTex,a_v2f.uv + float2( _MainTex_TexelSize.x *  3,0.0f)).rgb * t_weight_2;
				t_color += tex2D(_MainTex,a_v2f.uv + float2(-_MainTex_TexelSize.x *  3,0.0f)).rgb * t_weight_2;
				t_color += tex2D(_MainTex,a_v2f.uv + float2( _MainTex_TexelSize.x *  5,0.0f)).rgb * t_weight_3;
				t_color += tex2D(_MainTex,a_v2f.uv + float2(-_MainTex_TexelSize.x *  5,0.0f)).rgb * t_weight_3;
				t_color += tex2D(_MainTex,a_v2f.uv + float2( _MainTex_TexelSize.x *  7,0.0f)).rgb * t_weight_4;
				t_color += tex2D(_MainTex,a_v2f.uv + float2(-_MainTex_TexelSize.x *  7,0.0f)).rgb * t_weight_4;
				t_color += tex2D(_MainTex,a_v2f.uv + float2( _MainTex_TexelSize.x *  9,0.0f)).rgb * t_weight_5;
				t_color += tex2D(_MainTex,a_v2f.uv + float2(-_MainTex_TexelSize.x *  9,0.0f)).rgb * t_weight_5;
				t_color += tex2D(_MainTex,a_v2f.uv + float2( _MainTex_TexelSize.x * 11,0.0f)).rgb * t_weight_6;
				t_color += tex2D(_MainTex,a_v2f.uv + float2(-_MainTex_TexelSize.x * 11,0.0f)).rgb * t_weight_6;
				t_color += tex2D(_MainTex,a_v2f.uv + float2( _MainTex_TexelSize.x * 13,0.0f)).rgb * t_weight_7;
				t_color += tex2D(_MainTex,a_v2f.uv + float2(-_MainTex_TexelSize.x * 13,0.0f)).rgb * t_weight_7;
				t_color += tex2D(_MainTex,a_v2f.uv + float2( _MainTex_TexelSize.x * 15,0.0f)).rgb * t_weight_8;
				t_color += tex2D(_MainTex,a_v2f.uv + float2(-_MainTex_TexelSize.x * 15,0.0f)).rgb * t_weight_8;

				return fixed4(t_color,1.0f);
			}
			ENDCG
		}
	}
}

