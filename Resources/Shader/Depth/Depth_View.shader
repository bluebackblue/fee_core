/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief シェーダ。深度表示。
*/


Shader "Fee/Depth/DepthView"
{
    Properties
	{
        _MainTex ("Texture", 2D) = "white" {}
		[MaterialToggle] camera_depth_flag ("Camera Depth Flag", Int) = 0
		texture_depth ("Texture Depth", 2D) = "white" {}
		rate_blend ("rate_blend", Float) = 1
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

			/** _CameraDepthTexture
			*/
			sampler2D _CameraDepthTexture;

			/** camera_depth_flag
			*/
			int camera_depth_flag;

			/** texture_depth
			*/
			sampler2D texture_depth;

			/** rate_blend
			*/
			float rate_blend;

			/** vert
			*/
			v2f vert(appdata a_appdata)
			{
				v2f t_ret;
				{
					t_ret.vertex = UnityObjectToClipPos(a_appdata.vertex);
					t_ret.uv = TRANSFORM_TEX(a_appdata.uv,_MainTex);
				}
				return t_ret;
			}

			/** frag
			*/
			fixed4 frag(v2f a_v2f) : SV_Target
			{
				fixed3 t_color = tex2D(_MainTex,a_v2f.uv).rgb;

				if(rate_blend > 0.0f){
					float t_depth;
					if(camera_depth_flag > 0){
						t_depth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture,a_v2f.uv));
						t_depth = Linear01Depth(t_depth);
					}else{
						t_depth = UNITY_SAMPLE_DEPTH(tex2D(texture_depth,a_v2f.uv));
						t_depth = Linear01Depth(t_depth);
					}
					t_color = t_color * (1.0f - rate_blend) + fixed3(t_depth,t_depth,t_depth) * rate_blend;
				}

				return fixed4(t_color,1.0f);
			}
			ENDCG
		}
    }
}

