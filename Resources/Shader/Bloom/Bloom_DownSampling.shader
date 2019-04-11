/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief シェーダ。ブルーム。
*/


Shader "Fee/Bloom/DownSampling"
{
    Properties
	{
        _MainTex ("Texture", 2D) = "white" {}
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
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			/** _MainTex
			*/
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _MainTex_TexelSize;

			/** vert
			*/
			v2f vert(appdata a_appdata)
			{
				v2f t_ret;
				{
					t_ret.pos = UnityObjectToClipPos(a_appdata.vertex);
					t_ret.uv = TRANSFORM_TEX(a_appdata.uv,_MainTex);
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

				return fixed4(t_color,1.0);
			}
			ENDCG
		}
    }
}

