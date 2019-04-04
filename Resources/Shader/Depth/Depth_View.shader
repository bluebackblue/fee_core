/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief シェーダ。深度表示。
*/


Shader "Depth/DepthView"
{
    Properties
	{
        _MainTex ("Texture", 2D) = "white" {}
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

			/** texture_depth
			*/
			sampler2D texture_depth;

			/** rate_blend
			*/
			float rate_blend;

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
				fixed3 t_color = tex2D(_MainTex,i.uv).rgb;

				if(rate_blend > 0.0f){
					fixed t_depth = UNITY_SAMPLE_DEPTH(tex2D(texture_depth,i.uv));
					t_depth = Linear01Depth(t_depth);

					if(t_depth < 0.0f){
						t_color = fixed3(0.0f,1.0f,0.0f);
					}else if(t_depth == 0.0f){
						t_color = fixed3(1.0f,0.0f,0.0f);
					}else{
						t_color = t_color * (1.0f - rate_blend) + fixed3(t_depth,t_depth,t_depth) * rate_blend;
					}
				}

				return fixed4(t_color,1.0f);
			}
			ENDCG
		}
    }
}

