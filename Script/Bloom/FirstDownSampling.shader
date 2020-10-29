

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief シェーダ。ブルーム。
*/


Shader "Fee/Bloom/FirstDownSampling"
{
	Properties
	{
		_MainTex("_MainTex",2D) = "white"{}
		threshold("Threshold",Float) = 0
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

			/** threshold
			*/
			float threshold;

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
				//ダウンサンプリング。
				half3 t_color_a = tex2D(_MainTex,a_v2f.uv + float2( _MainTex_TexelSize.x, _MainTex_TexelSize.y)).rgb;
				half3 t_color_b = tex2D(_MainTex,a_v2f.uv + float2( _MainTex_TexelSize.x,-_MainTex_TexelSize.y)).rgb;
				half3 t_color_c = tex2D(_MainTex,a_v2f.uv + float2(-_MainTex_TexelSize.x, _MainTex_TexelSize.y)).rgb;
				half3 t_color_d = tex2D(_MainTex,a_v2f.uv + float2(-_MainTex_TexelSize.x,-_MainTex_TexelSize.y)).rgb;
				half3 t_color = (t_color_a + t_color_b + t_color_c + t_color_d) * 0.25;

				fixed4 t_ret;

				//輝度の高いものだけを描画。
				{
					half t_luminance = Luminance(t_color.rgb);
					if(t_luminance < threshold){
						t_ret = fixed4(0.0,0.0,0.0,1.0);
					}else{
						t_ret = fixed4(t_color,1.0);
					}
				}

				return t_ret;
			}
			ENDCG
		}
	}
}

