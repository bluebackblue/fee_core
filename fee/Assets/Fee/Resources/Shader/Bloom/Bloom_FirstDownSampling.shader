/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief シェーダ。ブルーム。
*/


Shader "Bloom/FirstDownSampling"
{
    Properties
	{
        _MainTex ("Texture", 2D) = "white" {}
		threshold ("Threshold", Float) = 0
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
			float threshold;

			/** vert
			*/
			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			/** frag
			*/
			fixed4 frag(v2f i) : SV_Target
			{
				//ダウンサンプリング。
				half3 t_color_a = tex2D(_MainTex,i.uv + float2( _MainTex_TexelSize.x, _MainTex_TexelSize.y)).rgb;
				half3 t_color_b = tex2D(_MainTex,i.uv + float2( _MainTex_TexelSize.x,-_MainTex_TexelSize.y)).rgb;
				half3 t_color_c = tex2D(_MainTex,i.uv + float2(-_MainTex_TexelSize.x, _MainTex_TexelSize.y)).rgb;
				half3 t_color_d = tex2D(_MainTex,i.uv + float2(-_MainTex_TexelSize.x,-_MainTex_TexelSize.y)).rgb;
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

