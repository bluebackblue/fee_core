

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ミラー。シンプル。
*/


Shader "Fee/Mirror/Mirror"
{
	Properties
	{
		texture_mirror		("texture_mirror",2D)		= "white"{}
	}
	SubShader
	{
		Tags
		{
			"RenderType"="Opaque"
		}
		Pass
		{
			Cull Back
			ZTest LEqual
			ZWrite On

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			/** appdata
			*/
			struct appdata
			{
				float4		vertex			: POSITION;
				//float2		uv				: TEXCOORD0;
			};

			/** v2f
			*/
			struct v2f
			{
				float4		vertex			: SV_POSITION;
				//float2		uv				: TEXCOORD0;
				float4		refvertex		: TEXCOORD1;
			};

			/** texture_mirror
			*/
			sampler2D texture_mirror;

			/** vert
			*/
			v2f vert(appdata a_appdata)
			{
				v2f t_result;
				{
					t_result.vertex = UnityObjectToClipPos(a_appdata.vertex);
					//t_result.uv = a_appdata.uv;
					t_result.refvertex = ComputeScreenPos(t_result.vertex);
				}
				return t_result;
			}
			
			/** frag
			*/
			fixed4 frag(v2f a_v2f) : SV_Target
			{
				return tex2Dproj(texture_mirror,UNITY_PROJ_COORD(a_v2f.refvertex));
			}
			ENDCG
		}
	}
}

