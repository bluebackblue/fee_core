/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief シェーダ。アルファ。
*/


Shader "Fee/PerformanceCounter/Sprite"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		Cull Off
		ZWrite Off
		ZTest Always

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
				fixed4 color : COLOR;
			};

			/** v2f
			*/
			struct v2f
			{
				float4 pos : SV_POSITION;
				fixed4 color : COLOR;
			};

			/** vert
			*/
			v2f vert(appdata a_appdata)
			{
				v2f t_ret;
				{
					t_ret.pos = UnityObjectToClipPos(a_appdata.vertex);
					t_ret.color = a_appdata.color;
				}
				return t_ret;
			}
			
			/** frag
			*/
			fixed4 frag(v2f a_v2f) : SV_Target
			{
				return a_v2f.color;
			}
			ENDCG
		}
	}
}

