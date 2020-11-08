

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief シェーダ。半透明。
*/


Shader "Fee/Render2D/Alpha"
{
	Properties
	{
		_MainTex			("_MainTex",2D)			= "white"{}
	}
	SubShader
	{
		Tags
		{
			"RenderType" = "Transparent"
			"Queue" = "Transparent"
		}
		Pass
		{
			Cull Off
			ZWrite Off
			ZTest Always
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			/** appdata
			*/
			struct appdata
			{
				float4 vertex		: POSITION;
				fixed4 color		: COLOR;
				float2 uv			: TEXCOORD0;
			};

			/** v2f
			*/
			struct v2f
			{
				float4 vertex		: SV_POSITION;
				fixed4 color		: COLOR;
				float2 uv			: TEXCOORD0;
			};

			/** _MainTex
			*/
			sampler2D _MainTex;
			
			/** vert
			*/
			v2f vert(appdata a_appdata)
			{
				v2f t_ret;
				{
					t_ret.vertex = UnityObjectToClipPos(a_appdata.vertex);
					t_ret.color = a_appdata.color;
					t_ret.uv = a_appdata.uv;
				}
				return t_ret;
			}
			
			/** frag
			*/
			fixed4 frag(v2f a_v2f) : SV_Target
			{
				return tex2D(_MainTex,a_v2f.uv) * a_v2f.color;
			}
			ENDCG
		}
	}
}

